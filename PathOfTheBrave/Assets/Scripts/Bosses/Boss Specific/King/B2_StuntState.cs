using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B2_StuntState : BossStuntState
{
    private King king;

    public B2_StuntState(Boss boss, BossFiniteStateMachine stateMachine, string animBoolName, D_BossStuntState stateData,King king) : base(boss, stateMachine, animBoolName, stateData)
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
        if(isStuntTimeOver)
        {
            stateMachine.ChangeState(king.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
