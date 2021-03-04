using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconManager : MonoBehaviour
{
    public GameObject jumpReward1;
    public GameObject fallReward1;
    public GameObject jumpReward2;
    public GameObject fallReward2;
    public GameObject jumpReward3;
    public GameObject fallReward3;
    private GameObject[] jumpRewards;
    private GameObject[] fallRewards;

    void Start()
    {
        jumpRewards = new GameObject[]{jumpReward1, jumpReward2, jumpReward3};
        fallRewards = new GameObject[]{fallReward1, fallReward2, fallReward3};
    }

    public GameObject ChangeJumpReward(GameObject currJumpReward) 
    {
        return jumpRewards[System.Array.IndexOf(jumpRewards, currJumpReward) + 1];
    }

    public GameObject ChangeFallReward(GameObject currFallReward)
    {
        return fallRewards[System.Array.IndexOf(fallRewards, currFallReward) + 1];
    }
}
