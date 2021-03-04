using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject jump1HatL;
    public GameObject jump1HatR;
    public GameObject jumpStonk;
    public GameObject jumpReward;
    public GameObject fallReward;

    private bool movingRight = true;

    private PhaseManager phaseManager;
    private IconManager iconManager;

    void Start()
    {
        phaseManager = GameObject.Find("GameManager").GetComponent<PhaseManager>();
        iconManager = GetComponent<IconManager>();
    }

    public void SpawnJump1Hat() {
        if (phaseManager.GamePhase == 12) 
        {
            Vector3 pos = new Vector3(jump1HatL.transform.position.x, -0.70f, jump1HatL.transform.position.z);
            GameObject flyer = Instantiate(jump1HatL, pos, jump1HatR.transform.rotation);
            flyer.GetComponent<FlyLeft>().ChangeSpeed(10f);
            flyer.transform.parent = this.transform;
        } else {
            if (movingRight) {
                GameObject flyer = Instantiate(jump1HatR, jump1HatR.transform.position, jump1HatR.transform.rotation);
                flyer.transform.parent = this.transform;
                movingRight = false;
            } else {
                GameObject flyer = Instantiate(jump1HatL, jump1HatL.transform.position, jump1HatL.transform.rotation);
                flyer.transform.parent = this.transform;
                movingRight = true;
            }
        }
    }

    public void SpawnJumpIcon() 
    {
        GameObject coin = Instantiate(jumpReward, jumpReward.transform.position, jumpReward.transform.rotation);
        coin.transform.parent = this.transform;
    }

    public void SpawnFallIcon() 
    {
        GameObject coin = Instantiate(fallReward, fallReward.transform.position, fallReward.transform.rotation);
        coin.transform.parent = this.transform;
    }

    public void ChangeJumpIcon()
    {
        jumpReward = iconManager.ChangeJumpReward(jumpReward);
    }

    public void ChangeFallIcon()
    {
        fallReward = iconManager.ChangeFallReward(fallReward);
    }

    public void SpawnJumpStonk() 
    {
        GameObject jumper = Instantiate(jumpStonk, jumpStonk.transform.position, jump1HatL.transform.rotation);
        jumper.transform.parent = this.transform;
    }
}
