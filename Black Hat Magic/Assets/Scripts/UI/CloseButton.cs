using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour
{
    public GameObject menu;

    private bool phase6NotStarted = true;
    private GameManager gameManager;
    private PhaseManager phaseManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        phaseManager = GameObject.Find("GameManager").GetComponent<PhaseManager>();
    }

    public void CloseMenu() {
        menu.SetActive(false);
        gameManager.MenuOpen = false;
        phaseManager.HardCode();
    }

    public void ChangeTutorialPopup() {
        phaseManager.HandleTutorial();
    }

    public void ShowPhase6ShopMenu() {
        if (phaseManager.GamePhase == 6 && phase6NotStarted) {
            gameManager.Phase6Start();
            phase6NotStarted = false;
        }
    }

    public void PlayerGotScammed() {
        gameManager.GotScammed();
    }

    public void ChangeGravity() {
        gameManager.GravityEnding();
    }

    public void GameOver() {   
        gameManager.TheEnd();
    }

}
