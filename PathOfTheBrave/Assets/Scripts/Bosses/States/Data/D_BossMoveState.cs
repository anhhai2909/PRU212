using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newMoveStateData", menuName = "Data/Boss State Data/Move State")]
public class D_BossMoveState : ScriptableObject
{
    public float movementSpeed = 0f;
    public float minMoveTime = 0.5f;
    public float maxMoveTime = 1f;
}
