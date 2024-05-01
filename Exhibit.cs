using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Exhibit : MonoBehaviour
{
    private TMP_Text descr;
    private Description exhibit;

    private void Start() {
        descr = GetComponent<TMP_Text>();
        exhibit = GetComponentInParent<Description>();
        descr.enabled = false;
    }

    public void PrintDescriptionOnScreen() {
        descr.enabled = true;
        descr.SetText("Name: "+exhibit.GetName()+"\n"
                        +"Description: "+exhibit.GetDescription()+"\n"
                        +"Year: "+exhibit.GetYear()+"\n"
                        +"Price: "+exhibit.GetPrice()+"€\n\n"
                        +"Press (P) to Add to Cart\n"
                        +"Press (E) to View Description");
            
    }

    public void TurnWindowOff() {
        descr.enabled = false;
    }

    public bool IsActive() {
        return descr.enabled;
    }

    public void PrintInfo() {
        print(exhibit.GetName());
    }

    public Description GetDescription() {
        return exhibit;
    }

    public Vector3 GetPosition() {
        return exhibit.GetPosition();
    }
}
