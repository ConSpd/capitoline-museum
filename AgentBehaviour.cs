using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AgentBehaviour : MonoBehaviour
{
    private Vector3 agentPosition;
    private Vector3 playerPosition;
    private bool isChatting = false;
    private bool farewellMessage = false;
    private bool isCheckoutPhase = false;
    
    private TMP_Text textWindow;
    private AudioSource audioSource;

    private List<string> questions;
    private List<string> answers;
    private List<float> agentSpeakingDuration;
    private List<AudioClip> agentSpeech;

    private ChairBehaviour chair;
    private Player player;
    private Animator animator;
    private ShoppingCart shoppingCart;
    private PurchaseUI purchaseUI;
    
    [SerializeField] List<AudioClip> audioClips;



    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        chair = FindObjectOfType<ChairBehaviour>();
        shoppingCart = FindObjectOfType<ShoppingCart>();
        purchaseUI = FindObjectOfType<PurchaseUI>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        textWindow = transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>();
        agentPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isCheckoutPhase)
            if(!isChatting)
                CheckPlayerPosition();
            else
                QuestionPart();
    }

    private void CheckPlayerPosition() {
        playerPosition = player.transform.position;
        if(Vector3.Distance(playerPosition,agentPosition) <= 4f) {
            // When "Have a nice visit" is displayed game waits 4 seconds until TalkMessage appears
            if(farewellMessage)
                Invoke("ShowTalkMessage",4f);
            else
                ShowTalkMessage();
            if(Input.GetKeyDown(KeyCode.E)) {
                CancelInvoke();
                chair.MoveBack();
                StartConversation();
            }
        } else {
            textWindow.SetText("");
        }
    }

    private void StartConversation() {
        InitializeQuestions();
        audioSource.PlayOneShot(audioClips[0]);
        animator.SetTrigger("activateSpeaking");
        PlaceQuestions();
        isChatting = true;
        //Invoke("SetAgentWaiting",3f);
        animator.SetTrigger("activateWaiting");
    }

    private void QuestionPart() {
        int KeyPressed = GetKeyPressed();
        if(KeyPressed == 99) { // Player selected (X) Exit Conversation
            EndConversation();
            return;
        }
        if(KeyPressed == 88) { // Player selected (E) Continue
            audioSource.Stop();
            animator.ResetTrigger("activateSpeaking");
            animator.SetTrigger("activateWaiting");
            CancelInvoke();
            PlaceQuestions();
            return;
        }
        if((KeyPressed == 0) || (KeyPressed > questions.Count)) { // Player hit random key or index of answered question
            return;
        }
        audioSource.Stop();
        if(animator.GetBool("activateWaiting") == true)
            animator.SetBool("activateWaiting",false);
        if(answers[KeyPressed-1].Equals("Checkout")) {  
            Checkout();
            return;
        }
        AgentReply(KeyPressed);
    }

    private void AgentReply(int KeyPressed) {
        textWindow.SetText(answers[KeyPressed-1]+"\n\nPress (E) to Continue...");
        audioSource.PlayOneShot(agentSpeech[KeyPressed-1]);
        animator.SetTrigger("activateSpeaking");
        Invoke("SetAgentWaiting",agentSpeakingDuration[KeyPressed-1]);

        agentSpeakingDuration.RemoveAt(KeyPressed-1);
        agentSpeech.RemoveAt(KeyPressed-1);
        answers.RemoveAt(KeyPressed-1);
        questions.RemoveAt(KeyPressed-1);
    }

    private void SetAgentWaiting() {
        animator.SetTrigger("activateWaiting");
    }

    private void SetAgentWorking() {
        animator.SetTrigger("activateWorking");
        chair.MoveForward();
        isChatting = false;
    }

    private void ShowTalkMessage() {
        farewellMessage = false;
        textWindow.SetText("\n\n\n\n\n\n\nPress (E) to talk\nto the Receptionist.");
    }

    private void EndConversation() { 
        isChatting = false;
        textWindow.SetText("\n\n\n\n\nHave a nice visit.");
        audioSource.PlayOneShot(audioClips[1]);
        farewellMessage = true;
        animator.SetTrigger("activateWorking");
        chair.MoveForward();
    }

    private int GetKeyPressed() {
        int KeyPressed;
        if(Input.GetKeyDown(KeyCode.Alpha1))
            KeyPressed = 1;
        else if(Input.GetKeyDown(KeyCode.Alpha2))
            KeyPressed = 2;
        else if(Input.GetKeyDown(KeyCode.Alpha3))
            KeyPressed = 3;
        else if(Input.GetKeyDown(KeyCode.Alpha4))
            KeyPressed = 4;
        else if(Input.GetKeyDown(KeyCode.X))
            KeyPressed = 99;
        else if(Input.GetKeyDown(KeyCode.E))
            KeyPressed = 88;
        else
            KeyPressed = 0;
        return KeyPressed;
    }

    private void PlaceQuestions() {
        string textSum = "\n\n\n";
        if(!isChatting) // It activates when player initiates the conversation
            textSum += "Welcome to Capitoline Museum.\nHow can i help you ?\n";

        for(int i = 0;i<questions.Count;i++)
            textSum += "("+ (i+1) +") "+questions[i]+"\n";
        textSum += "\nPress (X) to Exit the Conversation";
        textWindow.SetText(textSum);
    }

    private void InitializeQuestions() {
        questions = new List<string>();
        answers = new List<string>();
        agentSpeech = new List<AudioClip>();
        agentSpeakingDuration = new List<float>();

        questions.Add("Tell me about the Museum");
        answers.Add("In ancient times, the Capitoline Hill \n"+
                    "was the geographical and political centre\n" +
                    "of Rome. Now, it’s home to the Capitoline\n"+
                    "Museum, which fittingly, tell the fascinating\n" +
                    "story of the Eternal City. Inside our Museum you\n" +
                    "will find exhibits that come from Ancient Rome,\n"+
                    "Egypt and Greece, all giants of ancient civilizations.\n");
        agentSpeech.Add(audioClips[4]);
        agentSpeakingDuration.Add(audioClips[4].length);

        questions.Add("How can i purchase exhibits?");
        answers.Add("\n\nGoing near any exhibit you have the option\n" +
                    "to get information about the item, age and price.\n" +
                    "Then you can add a copy of it to your cart.\n" +
                    "At the end of your visit you can return to \n" +
                    "the reception to purchase your copies.\n");
        agentSpeech.Add(audioClips[5]);
        agentSpeakingDuration.Add(audioClips[5].length);

        questions.Add("How do i get to the Cinema?");
        answers.Add("\n\nThe Cinema is located in Egypt room.\n" +
                    "Take a seat and enjoy a short documentary on\n" +
                    "Symmetry directed by Charles and Ray Eames.\n");
        agentSpeech.Add(audioClips[6]);
        agentSpeakingDuration.Add(audioClips[6].length);

        if(shoppingCart.GetItemAmount() > 0) {
            questions.Add("I would like to Checkout.");
            answers.Add("Checkout");
        }
    }

    private void Checkout() {
        isCheckoutPhase = true;
        textWindow.SetText("\n\n\n\nInsert your credentials\nin the following Window.");
        audioSource.PlayOneShot(audioClips[2]);
        Invoke("CheckoutPhase",3f);
    }
    private void CheckoutPhase() {
        purchaseUI.OpenCheckoutWindow();
    }

    public void FinishCheckoutPhase() {
        isCheckoutPhase = false;
        textWindow.SetText("\n\n\n\nCapitoline Museum thanks you\nfor your purchase.");
        audioSource.PlayOneShot(audioClips[3]);
        Invoke("SetAgentWorking",3f);
    }

    public void CancelCheckoutPhase() {
        isCheckoutPhase = false;
        textWindow.SetText("");
        SetAgentWorking();
    }
}
