using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int hatCost;
    public int HatCost { get {return hatCost;} set{hatCost = value;} }

    private bool gotScammed = false;
    public bool startEndSequence = false;
    public bool startFinalSequence = false;
    public bool MenuOpen = true;
    private bool finalErrorOpen = false;
    public bool grav = false;

    private int blackHatCost = 50;
    private int jumpReward = 1;
    private int fallReward = 5;

    private float pauseForEffect = 5f;

    private int minBalanceTutorial = 5;

    private Rigidbody2D playerBody;
    private PhaseManager phaseManager;
    private BalanceManager balanceManager;
    private SpawnController spawnManager;

    #region Variables
    public GameObject shopMenuObject;
    public RuntimeAnimatorController player;

    private Animator animator;

    public GameObject tutorialMenuObject;
    
    public GameObject shopIconButton;
    public GameObject shopSign;

    public Texture errorRich;
    public RawImage errorMenuTexture;
    public GameObject errorMenuObject;
    public Texture errorC;

    // Phase 12
    public Texture errorD1;
    public RuntimeAnimatorController playerFall;

    // End
    public Texture errorDied;
    public Texture errorEnd;
    #endregion

    void Awake()
    {
        animator = GameObject.Find("Player").GetComponent<Animator>();
        playerBody = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnController>();
        phaseManager = GetComponent<PhaseManager>();
        balanceManager = GetComponent<BalanceManager>();
        animator.runtimeAnimatorController = player;
        hatCost = blackHatCost;
    }

    // Update is called once per frame
    void Update()
    {
        // Failsafe - if user somehow skips tutorial, shop will show
        if (phaseManager.GamePhase > 0) {
            shopIconButton.SetActive(true);
        }
        // If the user reaches negative gold, game phase changes to phase 6
        /* For users who could not afford 5k on stage 4, we still want them to progress 
         * to negative, so we give them a boost straight to stage 6    
         */
        // Error menu shows up after player gets up from falling
        if ((phaseManager.GamePhase == 4  || phaseManager.GamePhase == 5) && balanceManager.Balance < 0 
            && (animator.GetCurrentAnimatorStateInfo(0).IsName("Stand") && !animator.GetBool("Jumping"))) {
            phaseManager.GamePhase = 5;
            phaseManager.ChangeGameHat();
        }

        // Phase 12 end game sequence!
        if (phaseManager.GamePhase >= 11 && balanceManager.Balance > 0 && !startEndSequence) {
            EndGame();
        }
    }

    public void ChangeBalance(string reward) {
        if (reward == "Jump") 
        {
            balanceManager.AddToBalance(jumpReward);
        } else if (reward == "Fall") 
        {
            balanceManager.AddToBalance(fallReward);
        } else if (reward == "BuyHat") 
        {
            if (phaseManager.GamePhase < 3 || phaseManager.GamePhase > 5) 
            {
                balanceManager.AddToBalance(-hatCost);
            }
            // On phase 5, we do not want to allow players to progress by buying through the store
            if (phaseManager.GamePhase != 5) {
                phaseManager.ChangeGameHat();
            } 
        }
    }

    public void SetJumpReward(int value) 
    {
        jumpReward = value;
    }

    public void SetFallReward(int value) 
    {
        fallReward = value;
    }

    public void OpenTutorialMenu() 
    {
        if (phaseManager.GamePhase == 0) 
        {
            if (phaseManager.TutorialStage <= 3) 
            {
                tutorialMenuObject.SetActive(true);
                MenuOpen = true;
            } else 
            {
                if (balanceManager.Balance >= minBalanceTutorial) 
                {
                    phaseManager.ChangeGameHat();
                }
            }
        }
    }

    public void Phase6Start() 
    {
        tutorialMenuObject.SetActive(true);
        shopSign.SetActive(true);
        errorMenuTexture.texture = errorRich;
    }

    public void Phase7Scam() 
    {
        errorMenuTexture.texture = errorC;
        errorMenuObject.SetActive(true);
        gotScammed = true;
    }

    // Hot swap error menu back to error1
    public void GotScammed() 
    {
        if (gotScammed) {
            errorMenuTexture.texture = errorRich;
        }
    }

    private void EndGame() 
    {
        startEndSequence = true;
        errorMenuTexture.texture = errorD1;
        errorMenuObject.SetActive(true);
    }

    public void GravityEnding() 
    {
        if (startEndSequence) {
            playerBody.gravityScale = 0.15f;
            animator.runtimeAnimatorController = playerFall;
            phaseManager.ChangeGameHat();
            startFinalSequence = true;
        }
    }

    // Let the feeling sink in that the player just died
    // We wait for 15 seconds to allow animation to finish before showing popup
    public IEnumerator EndGameError() 
    {
        yield return new WaitForSeconds(pauseForEffect);
        errorMenuTexture.texture = errorDied;
        errorMenuObject.SetActive(true);
        shopMenuObject.SetActive(false);
        finalErrorOpen = true;
    }

    public void TheEnd() 
    {
        if (finalErrorOpen) {
            errorMenuTexture.texture = errorEnd;
            errorMenuObject.SetActive(true);
            shopMenuObject.SetActive(false);
            playerBody.gravityScale = 0.5f;
            spawnManager.SpawnJump1Hat();
        }
    }
}
