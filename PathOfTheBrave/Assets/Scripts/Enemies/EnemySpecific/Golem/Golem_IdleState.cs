using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem_IdleState : IdleState
{
    private Golem golem;
    public Golem_IdleState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData, Golem golem) : base(etity, stateMachine, animBoolName, stateData)
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
        else if (isIdleTimeOver)
        {
            stateMachine.ChangeState(golem.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
