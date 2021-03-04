using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCoinAnim : CoinAnim
{
    protected override float MoveSpeed {get {return 1.5f;}}
    protected override float YDistance {get {return 0.75f;}}
}
