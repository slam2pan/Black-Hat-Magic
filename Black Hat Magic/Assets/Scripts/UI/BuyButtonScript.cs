using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyButtonScript : MonoBehaviour
{
    public GameObject errorMenu;
    public GameObject shopMenu;
    public GameObject yayHatMenu;
    private GameManager gameManager;
    private PhaseManager phaseManager;
    private BalanceManager balanceManager;
    

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        phaseManager = GameObject.Find("GameManager").GetComponent<PhaseManager>();
        balanceManager = GameObject.Find("GameManager").GetComponent<BalanceManager>();
    }

    public void BuyHat() 
    {
        if (Mathf.Abs(balanceManager.Balance) < Mathf.Abs(gameManager.HatCost)) 
        {
            // change to phase 4 if the player cannot afford when he first opens the shop in stage 3
            if (phaseManager.GamePhase == 3)
            {
                gameManager.ChangeBalance("BuyHat");
            }
            errorMenu.SetActive(true);
            shopMenu.SetActive(false);
            gameManager.MenuOpen = true;
        } else // if he can afford
        {
            if (phaseManager.GamePhase == 7) {
                gameManager.Phase7Scam();
            } else {
                yayHatMenu.SetActive(true);
            }
            shopMenu.SetActive(false);
            gameManager.MenuOpen = true;
            gameManager.ChangeBalance("BuyHat");
        }  
    }
}
