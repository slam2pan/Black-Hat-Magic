using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanFly : MonoBehaviour
{
    private float snowmanSpeed = 5f;
    private float topOfScreen = 8f;
    private GameManager gameManager;
    private PhaseManager phaseManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        phaseManager = GameObject.Find("GameManager").GetComponent<PhaseManager>();
    }

    void Update()
    {
        if (phaseManager.GamePhase == 11) {
            transform.position += transform.up * Time.deltaTime * snowmanSpeed;
        }

        // Remove snowman once it leaves the screen
        if (transform.position.y > topOfScreen) {
            Destroy(this.transform.parent.gameObject);
        }
    }
}
