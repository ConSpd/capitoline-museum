using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Description : MonoBehaviour
{
    [SerializeField] string exhibitName;
    [SerializeField] float price;
    [SerializeField] string description;
    [SerializeField] string longDescription;
    [SerializeField] string year;
    [SerializeField] Sprite image;

    public string GetName() {
        return exhibitName;
    }
    public string GetDescription() {
        return description;
    }
    public string GetLongDescription() {
        return longDescription;
    }
    public float GetPrice() {
        return price;
    }
    public string GetYear() {
        return year;
    }
    public Sprite GetImage() {
        return image;
    }
    public Vector3 GetPosition() {
        return transform.position;
    }
}
