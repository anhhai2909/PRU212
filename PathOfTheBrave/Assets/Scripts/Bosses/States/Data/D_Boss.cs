using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newBossData",menuName ="Data/Boss Data/Base Data")]
public class D_Boss : ScriptableObject
{
    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;

    public float closeRangeActionDistance = 1f;

    public float maxHealth;
}
