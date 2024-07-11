using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackState : State
{
    protected Transform attackPosition;

    public BossAttackState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition) : base(etity, stateMachine, animBoolName)
    {
        this.attackPosition = attackPosition;
    }
}
