using UnityEngine;
using TMPro;

public class ChairHandler : MonoBehaviour
{
    private Player player;
    private ShoppingCart shoppingCart;
    private Vector3 playerPosition;
    private Vector3 sittingPosition;
    private float distance;
    private TMP_Text sitOnChairMessage;
    private TMP_Text standUpMessage;
    public bool isSitting = false;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        shoppingCart = FindObjectOfType<ShoppingCart>();
        sittingPosition = new Vector3(transform.position.x,transform.position.y+0.5f,transform.position.z+0.5f);

        sitOnChairMessage = transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        sitOnChairMessage.enabled = false;
        standUpMessage = transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>();
        standUpMessage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = player.transform.position;
        distance = Vector3.Distance(playerPosition,transform.position);
        if((distance <= 3) && !isSitting) {
            sitOnChairMessage.enabled = true;
            if(Input.GetKeyDown(KeyCode.E))
                SitOnChair();
        }else
            sitOnChairMessage.enabled = false;

        if(isSitting && Input.GetKey(KeyCode.Q) && !shoppingCart.IsCartActive())
            StandOffChair();
    }

    private void SitOnChair() {
        isSitting = true;
        player.DisablePlayerMovement(sittingPosition,transform.localEulerAngles);
        sitOnChairMessage.enabled = false;
        standUpMessage.enabled = true;
    }

    private void StandOffChair() {
        isSitting = false;
        standUpMessage.enabled = false;
        player.EnablePlayerMovement();
    }
}
