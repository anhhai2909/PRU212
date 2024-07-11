using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem_PlayerDetectedState : PlayerDetectedState
{
    private Golem golem;

    public Golem_PlayerDetectedState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData, Golem golem) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.golem = golem;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (performCloseRangeAction)
        {
            stateMachine.ChangeState(golem.meleeAttackState);
        }
        //else if (performLongRangeAction)
        //{
        //    stateMachine.ChangeState(golemgeState);
        //}
        else if (!isPlayerInMaxAgroRange)
        {
            stateMachine.ChangeState(golem.lookForPlayerState);
        }
        //else if (!isDetectingLedge) {
        //	Movement?.Flip();
        //	stateMachine.ChangeState(enemy.moveState);
        //}

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
