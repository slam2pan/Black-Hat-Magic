using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenShopButton : MonoBehaviour
{ 
    public GameObject shopMenu;
    public GameObject errorMenu;
    public GameObject yayMenu;
    public GameObject tutorialMenu;
    public GameObject shopSign;

    private PhaseManager phaseManager;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        phaseManager = GameObject.Find("GameManager").GetComponent<PhaseManager>();
    }

    public void OpenMenu() {
        // Don't allow user to open shop if an error/yay message is open
        if (!errorMenu.activeInHierarchy && !yayMenu.activeInHierarchy && !tutorialMenu.activeInHierarchy && phaseManager.GamePhase <= 11) {
            shopMenu.SetActive(true);
            gameManager.MenuOpen = true;
            phaseManager.HardCodeOn();
            shopSign.SetActive(false);
        }
    }
}
