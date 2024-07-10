using System;
using UnityEngine;

namespace Weapons.Components
{
    [Serializable]
    public class AttackChargeToProjectileSpawner : AttackData
    {
        [field: SerializeField] public ProjectileSpawnInfo[] chargeProjectile { get; private set; }
        [field: SerializeField, Range(0f, 360f)] public float AngleVariation { get; private set; }
    }
}