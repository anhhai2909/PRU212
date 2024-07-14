using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B2_WaitState : BossWaitState
{
    private King king;
    public B2_WaitState(Boss boss, BossFiniteStateMachine stateMachine, string animBoolName, D_BossWaitState stateData,King king) : base(boss, stateMachine, animBoolName, stateData)
    {
        this.king = king;
    }

    public override void DoChecks()
    {
        base.DoChecks();
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
        if(isWaitTimeOver)
        {
            stateMachine.ChangeState(king.stuntState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
