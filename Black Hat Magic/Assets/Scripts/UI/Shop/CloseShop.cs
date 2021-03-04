using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseShop : MonoBehaviour
{
    public GameObject menu;

    private GameManager gameManager;
    private PhaseManager phaseManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        phaseManager = GameObject.Find("GameManager").GetComponent<PhaseManager>();
    }

    public void CloseMenu() {
        // change to phase 4 if the player cannot afford when he first opens the shop in stage 3
        if (phaseManager.GamePhase == 3)
        {
            gameManager.ChangeBalance("BuyHat");
        }
        menu.SetActive(false);
        gameManager.MenuOpen = false;
        phaseManager.HardCode();
    }
}
