using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ShoppingCart : MonoBehaviour
{

    public List<Exhibit> purchasedItems;
    private CartUI uiScreen;
    private AudioSource audioSource;
    [SerializeField] private AudioClip purchaseSound;
    [SerializeField] private AudioClip removeSound;
    [SerializeField] private AudioClip openCart;

    void Start()
    {
        uiScreen = FindObjectOfType<CartUI>();
        purchasedItems = new List<Exhibit>();
        audioSource = GetComponent<AudioSource>();
    }

    public void AddItem(Exhibit exhibit) {
        purchasedItems.Add(exhibit);
        uiScreen.ShowMessage(exhibit.GetDescription().GetName());
        audioSource.volume = 0.3f;
        audioSource.PlayOneShot(purchaseSound);
        uiScreen.AddItemToUI(exhibit);
    }

    public void ShowCart() {
        audioSource.volume = 1f;
        audioSource.PlayOneShot(openCart);
        uiScreen.ShowCart();
    }

    public void CloseCart() {
        uiScreen.CloseCart();
    }

    public bool IsCartActive() {
        return uiScreen.IsCartActive();
    }

    public void RemoveItem(string itemToRemove) {
        foreach(Exhibit item in purchasedItems) {
            if(item.GetDescription().GetName().Equals(itemToRemove)) {
                uiScreen.RemoveItem(item);
                purchasedItems.Remove(item);
                audioSource.volume = 0.6f;
                audioSource.PlayOneShot(removeSound);
                break;
            }
        }
    }

    public IEnumerator RemoveAllItems() {
        for(int i = 0;i<purchasedItems.Count;i++) {
            uiScreen.RemoveItem(purchasedItems[i]);
            yield return new WaitForSeconds(0.01f);
        }
        purchasedItems.Clear();
    }    

    public int GetItemAmount() {
        return purchasedItems.Count;
    }

    public float GetSumPrice() {
        float sum = 0f;
        foreach(Exhibit item in purchasedItems)
            sum += item.GetDescription().GetPrice();
        return sum;
    }
}
