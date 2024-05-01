using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float activationDistance = 7f;

    private Exhibit closestExhibit;
    private Exhibit[] allExhibits;
    private float minDistance;
    private bool lockPosition = false;
    private Vector3 startingRotation;
    private Rigidbody playerRigidBody;

    private CartUI uiScreen;
    private ShoppingCart shoppingCart;
    private DescriptionUI descriptionUI;

    void Start()
    {
        //Application.targetFrameRate = 60;

        uiScreen = FindObjectOfType<CartUI>();
        allExhibits = FindObjectsOfType<Exhibit>();
        shoppingCart = FindObjectOfType<ShoppingCart>();
        descriptionUI = FindObjectOfType<DescriptionUI>();
        playerRigidBody = GetComponent<Rigidbody>();
        closestExhibit = null;
        Cursor.visible = false;
        Cursor.lockState =  CursorLockMode.Locked;
    }

    void Update()
    {
        CheckDistances();
        InputListener();
    }

    private void LateUpdate() {
        if(lockPosition) 
            transform.localEulerAngles = startingRotation;
    }

    // Finds the exhibit with the smallest distance off the player and if it's in the activateDistance
    // radius it prints the Description on the screen
    private void CheckDistances() {
        for(int i = 0; i<allExhibits.Length; i++) {
            Vector3 exhibitPosition = allExhibits[i].GetPosition();
            float distance = Vector3.Distance(exhibitPosition,transform.position);
            if(i == 0) 
                minDistance = distance;
            if(distance <= minDistance) {
                minDistance = distance;
                closestExhibit = allExhibits[i];
            } 
        }
        if((minDistance <= activationDistance) && !closestExhibit.IsActive()) {
            closestExhibit.PrintDescriptionOnScreen();
            ClearOtherWindows();
        } else if(minDistance > activationDistance) {
            if(closestExhibit.IsActive())
                closestExhibit.TurnWindowOff();
            closestExhibit = null;
        }

    }

    private void InputListener() {
        if(Input.GetKeyDown(KeyCode.P))
            PurchaseExhibit();

        if(Input.GetKeyDown(KeyCode.E)) {
            ShowItemDescription();
        }
            

        if(Input.GetKeyDown(KeyCode.F) && !uiScreen.IsCartActive()) {
            shoppingCart.ShowCart();
            DisablePlayerMovement();
            UnlockCursor();
        }
            
        if(Input.GetKeyDown(KeyCode.Q)) {
            if(uiScreen.IsCartActive())
                shoppingCart.CloseCart();
            if(descriptionUI.IsDescriptionActive())
                descriptionUI.CloseDescriptionUI();
            EnablePlayerMovement();
            LockCursor();
        }

    }

    private void ClearOtherWindows() {
        foreach(Exhibit exhibit in allExhibits)
            if(exhibit.GetInstanceID() != closestExhibit.GetInstanceID())
                exhibit.TurnWindowOff();
    }

    public void PurchaseExhibit() {
        if(closestExhibit != null)
            shoppingCart.AddItem(closestExhibit);
    }

    public void ShowItemDescription() {
        if(closestExhibit != null) {
            DisablePlayerMovement();
            descriptionUI.ShowDescriptionUI(closestExhibit);
        }
    }

    /* Player Lock Settings */

    public void DisablePlayerMovement() {
        startingRotation = transform.localEulerAngles;
        lockPosition = true;
        playerRigidBody.constraints = RigidbodyConstraints.FreezePosition;
    }

    public void DisablePlayerMovement(Vector3 position,Vector3 rotation) { // With preset position and rotation
        transform.position = position;
        startingRotation = rotation;
        playerRigidBody.constraints = RigidbodyConstraints.FreezePosition;
        lockPosition = true;
    }

    public void EnablePlayerMovement() {
        lockPosition = false;
        playerRigidBody.constraints = RigidbodyConstraints.None;
        playerRigidBody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    public void UnlockCursor() {
        Cursor.visible = true;
        Cursor.lockState =  CursorLockMode.Confined;
    }

    public void LockCursor() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}