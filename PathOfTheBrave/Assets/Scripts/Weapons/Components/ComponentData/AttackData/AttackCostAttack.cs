using System;
using UnityEngine;

namespace Weapons.Components
{
    [Serializable]
    public class AttackCostAttack : AttackData
    {
        [field: SerializeField] public float CostMana { get; private set; } = 0;
        [field: SerializeField] public float CostHealth { get; private set; } = 0;
    }
}


