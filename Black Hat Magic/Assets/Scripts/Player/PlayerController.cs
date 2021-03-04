using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    private Vector3 prevLoc = Vector3.zero;

    public Animator animator;
    private bool isGrounded = true;

    private SpriteRenderer spriteRenderer;
    private GameManager gameManager;
    private SpawnController spawnController;
    private PhaseManager phaseManager;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spawnController = GameObject.Find("SpawnManager").GetComponent<SpawnController>();
        phaseManager = GameObject.Find("GameManager").GetComponent<PhaseManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !animator.GetBool("Falling") && isGrounded && !gameManager.MenuOpen) {
            isGrounded = false;
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 5f), ForceMode2D.Impulse);
            animator.SetBool("Jumping", true);
            gameManager.ChangeBalance("Jump");
            spawnController.SpawnJumpIcon();

            // 10% chance to trigger screen flying jump1Hat
            // Only occurs after troll phase begins, but stops when the moon appears
            if (!(phaseManager.GamePhase > 8) && phaseManager.GamePhase >= 4 && Random.Range(0, 100) < 10) {
                spawnController.SpawnJump1Hat();
            }
        }

        // Begin fall animation as soon as velocity turns negative during phase 12
        if (phaseManager.GamePhase == 12 && animator.GetBool("Jumping")) {
            Vector3 curVel = (transform.position - prevLoc) / Time.deltaTime;
            // WHY DOES VEL.Y - 0 NOT WORK
            if (curVel.y < -2) {
                animator.SetBool("Falling", true);
                GetComponent<Rigidbody2D>().gravityScale = 0.3f;
            }
            prevLoc = transform.position;
        }

        // Remove player when the game ends
        if (transform.position.y < -20) 
        {
            Destroy(this);
        }
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Ground") {

            // During the tutorial (phase 0), the player cannot fall
            // Until troll mode, 20% chance to fall
            // During troll mode, he will always fall
            // Until scam, 30% chance to fall
            // After scam, he will always fall
            if ((phaseManager.GamePhase > 0 && (Random.Range(0, 100) < 20)) 
                || phaseManager.GamePhase == 4 || phaseManager.GamePhase == 5 
                || (phaseManager.GamePhase > 5 && phaseManager.GamePhase < 8 && (Random.Range(0, 100) < 30))
                || phaseManager.GamePhase >= 8) {
                animator.SetBool("Falling", true);
                gameManager.ChangeBalance("Fall");
                spawnController.SpawnFallIcon();
            }
            animator.SetBool("Jumping", false);
            isGrounded = true;

            // Show tutorial message when player lands (only occurs during phase 0)
            gameManager.OpenTutorialMenu();

            // Trigger phase 11
            phaseManager.StartPhase11();

            if (gameManager.startFinalSequence) {
                animator.SetBool("Jumping", false);
                animator.SetBool("Dying", true);
                StartCoroutine(gameManager.EndGameError());
            }
        }
    }

}
