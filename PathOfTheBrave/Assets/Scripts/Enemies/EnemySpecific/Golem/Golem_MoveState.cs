using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem_MoveState : MoveState
{
    private Golem golem;

    public Golem_MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData, Golem golem) : base(entity, stateMachine, animBoolName, stateData)
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

        if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(golem.playerDetectedState);
        }
        //else if(isDetectingWall || !isDetectingLedge)
        //{
        //    enemy.idleState.SetFlipAfterIdle(true);
        //    stateMachine.ChangeState(enemy.idleState);
        //}
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
