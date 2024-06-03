using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationToWeapon : MonoBehaviour
{
    private Weapon weapon;

    [SerializeField]
    private Transform ProjectileSpawnPos;
    private void Start()
    {
        weapon = GetComponentInParent<Weapon>();
    }

    private void AnimationFinishTrigger()
    {
        weapon.AnimationFinishTrigger();
    }

    private void AnimationStartMovementTrigger()
    {
        weapon.AnimationStartMovementTrigger();
    }

    private void AnimationStopMovementTrigger()
    {
        weapon.AnimationStopMovementTrigger();
    }

    private void AnimationTurnOffFlipTrigger()
    {
        weapon.AnimationTurnOffFlipTrigger();
    }

    private void AnimationTurnOnFlipTrigger()
    {
        weapon.AnimationTurnOnFlipTigger();
    }

    private void AnimationActionTrigger()
    {
        weapon.AnimationActionTrigger();
    }

    private void TriggerProjectile()
    {
        weapon.TriggerProjectile(ProjectileSpawnPos);
    }

}
