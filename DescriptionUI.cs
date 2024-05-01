using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DescriptionUI : MonoBehaviour
{
    private Image itemImage;
    private TMP_Text title;
    private TMP_Text description;
    private Transform panel;
    private bool isActive = false;
    private AudioSource audioSource;

    void Start()
    {
        panel = transform.GetChild(0);
        InitializeFields();
        audioSource = GetComponent<AudioSource>();
    }

    private void InitializeFields() {
        itemImage = panel.GetChild(0).GetComponent<Image>();
        title = panel.GetChild(1).GetComponent<TMP_Text>();
        description = panel.GetChild(2).GetComponent<TMP_Text>();
    }

    public void ShowDescriptionUI(Exhibit exhibit) {
        audioSource.volume = 0.7f;
        audioSource.Play();
        title.SetText(exhibit.GetDescription().GetName());
        description.SetText(exhibit.GetDescription().GetLongDescription());
        itemImage.sprite = exhibit.GetDescription().GetImage();
        panel.gameObject.SetActive(true);
        isActive = true;
    }

    public void CloseDescriptionUI() {
        panel.gameObject.SetActive(false);
        isActive = false;
    }

    public bool IsDescriptionActive() {
        return isActive;
    }
}
