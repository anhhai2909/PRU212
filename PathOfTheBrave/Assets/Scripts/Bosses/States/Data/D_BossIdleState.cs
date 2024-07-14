using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newIdleStateData", menuName = "Data/Boss State Data/Idle State")]
public class D_BossIdleState : ScriptableObject
{
    public float minIdleTime = 2f;
    public float maxIdleTime = 4f;
}
