using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected SO_WeaponData weaponData;

    protected Animator attackAnimation;

    protected PlayerAttackState state;

    protected Core core;

    protected int attackCounter;

    protected virtual void Awake()
    {
        attackAnimation = transform.Find("Attack").GetComponent<Animator>();

        transform.Find("Attack").gameObject.SetActive(false);
    }

    public virtual void EnterWeapon()
    {
        transform.Find("Attack").gameObject.SetActive(true);

        if(attackCounter >= weaponData.amountOfAttacks)
        {
            attackCounter = 0;
        }

        attackAnimation.SetBool("attack", true);
        attackAnimation.SetInteger("attackCounter", attackCounter);
    }

    public virtual void ExitWeapon()
    {
        attackAnimation.SetBool("attack", false);
        attackCounter++;
        transform.Find("Attack").gameObject.SetActive(false);
    }

    #region Animation Triggers

    public virtual void AnimationFinishTrigger()
    {
        state.AnimationFinishTrigger();
    }

    public virtual void AnimationStartMovementTrigger()
    {
        state.SetPlayerVelocity(weaponData.movementSpeed[attackCounter]);
    }

    public virtual void AnimationStopMovementTrigger()
    {
        state.SetPlayerVelocity(0f);
    }

    public virtual void AnimationTurnOffFlipTrigger()
    {
        state.SetFlipCheck(false);
    }

    public virtual void AnimationTurnOnFlipTigger()
    {
        state.SetFlipCheck(true);
    }

    public virtual void AnimationActionTrigger() { }

    #endregion

    public void InitializeWeapon(PlayerAttackState state, Core core)
    {
        this.state = state;
        this.core = core;
    }
    public virtual void TriggerProjectile(Transform pos)
    {

    }
}
