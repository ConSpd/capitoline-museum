using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PurchaseUI : MonoBehaviour
{
    private TMP_Text totalSum;
    private TMP_Text numberOfItems;

    private Button confirmButton;
    private Button exitButton;

    private Transform purchaseWindow;
    private Transform buttonChild;

    private Player player;
    private ShoppingCart shoppingCart;
    private AgentBehaviour agent;

    void Start()
    {
        agent = FindObjectOfType<AgentBehaviour>();
        shoppingCart = FindObjectOfType<ShoppingCart>();

        purchaseWindow = transform.GetChild(0);
        player = FindObjectOfType<Player>();

        buttonChild = purchaseWindow.GetChild(3);
        confirmButton = buttonChild.GetComponent<Button>();
        confirmButton.onClick.AddListener(CloseCheckoutWindow);
        exitButton = purchaseWindow.GetChild(4).GetComponent<Button>();
        exitButton.onClick.AddListener(CancelCheckout);

        totalSum = purchaseWindow.GetChild(2).GetChild(1).GetComponent<TMP_Text>();
        numberOfItems = purchaseWindow.GetChild(2).GetChild(0).GetComponent<TMP_Text>();
    }

    public void OpenCheckoutWindow() {
        player.DisablePlayerMovement();
        player.UnlockCursor();
        purchaseWindow.gameObject.SetActive(true);

        numberOfItems.SetText("Total Amount ("+shoppingCart.GetItemAmount().ToString()+" items)");
        totalSum.SetText(shoppingCart.GetSumPrice().ToString()+"€");
    }

    public void CloseCheckoutWindow() {
        StartCoroutine(shoppingCart.RemoveAllItems());
        player.EnablePlayerMovement();
        player.LockCursor();
        purchaseWindow.gameObject.SetActive(false);
        agent.FinishCheckoutPhase();
    }

    public void CancelCheckout() {
        player.EnablePlayerMovement();
        player.LockCursor();
        purchaseWindow.gameObject.SetActive(false);
        agent.CancelCheckoutPhase();
    }
}
