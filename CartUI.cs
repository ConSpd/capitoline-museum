using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CartUI : MonoBehaviour
{
    private bool isCartActive = false;
    private bool isFirstItem = true;
    private float sumOfItems = 0f;
    private TMP_Text sumValue;
    private List<Transform> itemChilds;

    private TMP_Text purchaseScreen;
    private GameObject inventoryNote;
    private GameObject sum;
    private GameObject cart; // Σε περίπτωση που buggάρει κάντο με Serialize
    private GameObject itemList;

    // Start is called before the first frame update
    void Start() {
        purchaseScreen = transform.GetChild(2).GetComponent<TMP_Text>();
        cart = transform.GetChild(0).gameObject;
        sum = transform.GetChild(0).GetChild(3).gameObject;
        inventoryNote = transform.GetChild(1).gameObject;
        itemList = transform.GetChild(0).GetChild(2).GetChild(0).gameObject;
        purchaseScreen.enabled = false;
        itemChilds = new List<Transform>();
        sumValue = sum.GetComponent<TMP_Text>();
    }

    public void ShowMessage(string itemName) {
        if(purchaseScreen.enabled) {
            DisableMessage();
            CancelInvoke();
        }
        purchaseScreen.SetText("Purchased "+itemName);
        purchaseScreen.enabled = true;
        Invoke("DisableMessage",3f);
    }

    private void DisableMessage() {
        purchaseScreen.enabled = false;
    }

    public void AddItemToUI(Exhibit item) {
        Transform textParent;
        if(isFirstItem) {
            textParent = itemList.transform.GetChild(0).GetChild(1); // textParent contains TMP_Text Component
            itemList.transform.GetChild(0).gameObject.SetActive(true); // Item gets set to active
            itemChilds.Add(itemList.transform.GetChild(0));
            isFirstItem = false;
        } else {
            itemChilds.Add(Instantiate(itemList.transform.GetChild(0),itemList.transform)); // Add a new Item object to list
            textParent = itemChilds[itemChilds.Count-1].GetChild(1); // Get the text object inside it
        }
        TMP_Text textObject = textParent.GetComponent<TMP_Text>();
        textObject.SetText(item.GetDescription().GetName());
        textObject.fontSize = 18;

        Image itemImage = itemList.transform.GetChild(0).GetChild(2).GetComponent<Image>();
        itemImage.sprite = item.GetDescription().GetImage();

        sumOfItems += item.GetDescription().GetPrice();
        sumValue.SetText("Total Price: "+sumOfItems+"€");
    }

    public void ShowCart() {
        cart.SetActive(true);
        isCartActive = true;
        inventoryNote.SetActive(false);
        sumValue.SetText("Total Price: "+sumOfItems+"€"); 
    }

    public void CloseCart() {
        cart.SetActive(false);
        isCartActive = false;
        inventoryNote.SetActive(true);
    }

    public bool IsCartActive() {
        return isCartActive;
    }

    public void RemoveItem(Exhibit itemToRemove) {
        if(itemChilds.Count == 1) {
            itemList.transform.GetChild(0).gameObject.SetActive(false);
            itemList.transform.GetChild(0).gameObject.name = "Item";
            isFirstItem = true;
            sumOfItems = 0f;
            sumValue.SetText("Total Price: "+sumOfItems+"€");
            itemChilds.RemoveAt(0);
            return;
        }
        string itemName;
        foreach (Transform item in itemChilds) {
            itemName = item.GetChild(1).GetComponent<TMP_Text>().text;
            if(itemName.Equals(itemToRemove.GetDescription().GetName())) {
                sumOfItems -= itemToRemove.GetDescription().GetPrice();
                sumValue.SetText("Total Price: "+sumOfItems+"€");
                itemChilds.Remove(item);
                Destroy(item.gameObject);
                break;
            }
        }
    }
}
