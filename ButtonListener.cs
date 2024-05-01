using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonListener : MonoBehaviour
{

    private Button button;
    private string itemName;
    private GameObject item;
    private ShoppingCart shoppingCart;

    // Start is called before the first frame update
    void Start()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(ButtonHandler);

        item = transform.parent.GetChild(1).gameObject;
        itemName = item.GetComponent<TMP_Text>().text;

        shoppingCart = FindObjectOfType<ShoppingCart>();

    }

    void ButtonHandler() {
        shoppingCart.RemoveItem(itemName);
    }
}
