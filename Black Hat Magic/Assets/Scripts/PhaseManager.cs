using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhaseManager : MonoBehaviour
{
    [SerializeField] private int gamePhase = 0;
    public int GamePhase { get {return gamePhase;} set{gamePhase = value;} }

    private int tutorialStage = 0;
    public int TutorialStage { get {return tutorialStage;} set{} }

    private int blackHatCost = 50;
    private int invisHatCost = 100;
    private int blackHat2Cost = 250;
    private int happinessCost = -2000;
    private int mysteryDogCost = -7777;
    private int snowmanCost = -10000;
    private int moonCost = -42069;
    private int jumpReward2 = -69;
    private int fallReward2 = -420;
    private int jumpReward3 = 13;
    private int fallReward3 = 37;

    private float mmWaitTime = 3f;

    private GameManager gameManager;
    private BalanceManager balanceManager;
    private SpawnController spawnManager;

    #region Variables
    public AnimatorOverrideController animHat;
    public Sprite standHat;
    public Sprite groundHat;
    public Texture shopMenuHat;
    public Texture errorMenuHat;
    public Texture yayMenu;
    public Sprite price2;
    public Texture shopHat;
    public GameObject shopMenuObject;

    // Plain objects/"invis hat" objects
    public RuntimeAnimatorController player;
    public Sprite stand;
    public Sprite ground;
    public Texture shopMenu1;
    public Texture error1;
    public Texture error2;
    public Texture yayHat;
    public Sprite price3;
    public Texture shop;

    // Phase 4
    public Sprite price4;
    public AnimatorOverrideController player1;

    private Text balanceText;
    private Animator animator;
    private SpriteRenderer playerSprite;
    private SpriteRenderer groundSprite;

    // Tutorial
    public Texture controls;
    public Texture wow1;
    public Texture wow2;
    public Texture wow3;
    public RawImage tutorialMenu;
    public GameObject tutorialMenuObject;

    // Inactive menus must be assigned via inspector
    public GameObject whiteBox;

    public RawImage shopMenuTexture;
    public RawImage errorMenuTexture;
    public RawImage yayTexture;
    public Image priceImage;
    public GameObject closeShopButton;
    public RawImage shopIconTexture;
    public GameObject shopIconButton;
    public GameObject shopSign;

    // Phase 6
    public Texture errorD;
    public Texture wowNewShop;
    public GameObject errorMenuObject;
    public GameObject yayMenuObject;
    public Texture negShopMenu1;
    public Sprite price5;

    // Phase 7
    public Sprite groundHappy;
    public Texture yayHappy;
    public Texture negShopMenu2;
    public Sprite price6;
    public Texture errorC;

    // Phase 8
    public Texture negShopMenu3;
    public Sprite price7;
    public Texture yaySnow;
    public GameObject snowman;
    public Texture yayMM;
    public Texture errorRich;

    // Phase 9
    public Texture negShopMenu4;
    public Sprite price8;

    // Phase 10
    public Texture shopMenuEnd;
    public GameObject moon;
    public Texture yayMoon;
    public GameObject priceObject;
    public GameObject buyButtonObject;

    // Phase 11
    public SpriteRenderer snowmanSprite;
    public SpriteRenderer moonSprite;
    public Sprite snowmanHat1;
    public Sprite moon1;
    public Sprite groundHappy1;
    public Texture errorMoon;

    // Phase 12
    public Texture errorD1;
    public RuntimeAnimatorController playerFall;

    // End
    public Texture errorDied;
    public Texture errorEnd;
    #endregion

    void Start()
    {
        playerSprite = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        animator = GameObject.Find("Player").GetComponent<Animator>();
        groundSprite = GameObject.Find("Ground").GetComponent<SpriteRenderer>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        balanceManager = GameObject.Find("GameManager").GetComponent<BalanceManager>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnController>();
    }

    void Update()
    {
        if ((gamePhase == 2 || gamePhase >= 7) && (errorMenuObject.activeSelf || yayTexture.gameObject.activeSelf))
        {
            whiteBox.SetActive(true);
        }
    }

    // Recreate game with all hats based on game phase.
    // Each case's code is what happens before changing to next phase
    public void ChangeGameHat() 
    {
         switch (gamePhase) 
        {
            // Tutorial
            case 0:
                shopIconButton.SetActive(true);
                shopSign.SetActive(true);
                gamePhase++;
                break;
            // Black hat
            case 1:
                animator.runtimeAnimatorController = animHat;
                playerSprite.sprite = standHat;
                groundSprite.sprite = groundHat;
                shopMenuTexture.texture = shopMenuHat;
                errorMenuTexture.texture = errorMenuHat;
                yayTexture.texture = yayMenu;
                priceImage.sprite = price2;
                shopIconTexture.texture = shopHat;
                gameManager.HatCost = invisHatCost;
                gamePhase++;
                break;
            // Invis hat
            case 2:
                animator.runtimeAnimatorController = player;
                playerSprite.sprite = stand;
                groundSprite.sprite = ground;
                shopMenuTexture.texture = shopMenu1;
                errorMenuTexture.texture = error1;
                yayTexture.texture = yayHat;
                priceImage.sprite = price3;
                shopIconTexture.texture = shop;
                gameManager.HatCost = blackHat2Cost;
                gamePhase++;
                break;
            // Black hat
            case 3:
                yayTexture.texture = error2;
                priceImage.sprite = price4;
                animator.runtimeAnimatorController = player1;
                gameManager.SetJumpReward(jumpReward2);
                gameManager.SetFallReward(fallReward2);
                spawnManager.ChangeJumpIcon();
                spawnManager.ChangeFallIcon();
                closeShopButton.SetActive(false);
                gamePhase++;
                break;
            // Troll mode
            case 4:
                yayTexture.texture = error1;
                gamePhase++;
                break;
            // Player reaches 5kgold troll mode
            case 5:
                errorMenuTexture.texture = errorD;
                tutorialMenu.texture = wowNewShop;
                animator.runtimeAnimatorController = player;
                errorMenuObject.SetActive(true);
                shopMenuObject.SetActive(false);
                gameManager.MenuOpen = true;
                shopMenuTexture.texture = negShopMenu1;
                closeShopButton.SetActive(true);
                priceImage.sprite = price5;
                gameManager.HatCost = happinessCost;
                gamePhase++;
                break;
            // Negative mode
            case 6:
                groundSprite.sprite = groundHappy;
                yayTexture.texture = yayHappy;
                shopMenuTexture.texture = negShopMenu2;
                gameManager.HatCost = mysteryDogCost;
                priceImage.sprite = price6;
                gamePhase++;
                break;
            // Happiness
            case 7:
                shopMenuTexture.texture = negShopMenu3;
                priceImage.sprite = price7;
                gameManager.HatCost = snowmanCost;
                StartCoroutine(MMMode(mmWaitTime));
                gamePhase++;
                break;
            // Scam
            case 8:
                snowman.SetActive(true); 
                shopMenuTexture.texture = negShopMenu4;
                errorMenuTexture.texture = errorRich;
                priceImage.sprite = price8;
                yayTexture.texture = yaySnow;
                gameManager.HatCost = moonCost;
                gamePhase++;
                break;
            // Snowman
            case 9:
                moon.SetActive(true);
                spawnManager.SpawnJumpStonk();
                yayTexture.texture = yayMoon;
                shopMenuTexture.texture = shopMenuEnd;
                priceObject.SetActive(false);
                buyButtonObject.SetActive(false);
                gamePhase++;
                break;
            // Moon
            case 10:
                snowmanSprite.sprite = snowmanHat1;
                moonSprite.sprite = moon1;
                groundSprite.sprite = groundHappy1;
                gameManager.SetJumpReward(jumpReward3);
                gameManager.SetFallReward(fallReward3);
                spawnManager.ChangeJumpIcon();
                spawnManager.ChangeFallIcon();
                errorMenuTexture.texture = errorMoon;
                errorMenuObject.SetActive(true);
                gameManager.MenuOpen = true;
                shopMenuObject.SetActive(false);
                gamePhase++;
                break;
            case 11:
                gamePhase++;
                break;
            // ENDING!
            default: break;
        }
    }

    public void StartPhase11() 
    {
        if (gamePhase == 10) {
            ChangeGameHat();
        }
    }

    // Change sprites during each stage of tutorial. At the end, enter stage 1.
    public void HandleTutorial() {
        if (tutorialStage == 0) {
            tutorialMenu.texture = wow1;
        } else if (tutorialStage == 1) {
            tutorialMenu.texture = wow2;
        } else if (tutorialStage == 2) {
            tutorialMenu.texture = wow3;
        }
        tutorialStage++;
    }

    private IEnumerator MMMode(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        yayTexture.texture = yayMM;
        yayMenuObject.SetActive(true);
        gameManager.MenuOpen = true;
        shopMenuObject.SetActive(false);
    }

    public void HardCode()
    {
        if (gamePhase == 2 || gamePhase == 3 || gamePhase >= 7)
        {
            whiteBox.SetActive(false);
        }
    }

    public void HardCodeOn()
    {
        if (gamePhase == 2)
        {
            whiteBox.SetActive(true);
        }
    }
}
