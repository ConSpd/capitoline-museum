using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class HelpUI : MonoBehaviour
{
    private GameObject HelpPanel;
    private GameObject HelpText;
    private bool isHelpOpen = false;
    private ShoppingCart shoppingCart;
    private ChairHandler chair;
    private DescriptionUI descriptionUI;
    private Player player;
    [SerializeField] UnityEditor.DefaultAsset HTMFiles;

    void Start()
    {
        HelpPanel = transform.GetChild(0).gameObject;
        HelpText = transform.GetChild(1).gameObject;
        shoppingCart = FindObjectOfType<ShoppingCart>();
        chair = FindObjectOfType<ChairHandler>();
        descriptionUI = FindObjectOfType<DescriptionUI>();
        player = FindObjectOfType<Player>();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.H) && !isHelpOpen && ΗasNoOtherWindowsOpen())
            OpenHelpWindow();
        if(Input.GetKeyDown(KeyCode.Q) && isHelpOpen && ΗasNoOtherWindowsOpen())
            CloseHelpWindow();
    }

    private void OpenHelpWindow() {
        isHelpOpen = true;
        HelpPanel.SetActive(true);
        HelpText.SetActive(false);
        player.DisablePlayerMovement();
        player.UnlockCursor();
    }

    private void CloseHelpWindow() {
        isHelpOpen = false;
        HelpPanel.SetActive(false);
        HelpText.SetActive(true);
        player.EnablePlayerMovement();
        player.LockCursor();
    }

    private bool ΗasNoOtherWindowsOpen() {
        if(!shoppingCart.IsCartActive() && !chair.isSitting && !descriptionUI.IsDescriptionActive())
            return true;
        else
            return false;
    }
}
