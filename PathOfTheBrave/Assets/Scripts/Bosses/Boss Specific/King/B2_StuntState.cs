using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B2_StuntState : BossState
{
    private King king;
    protected D_BossStuntState stateData;
    protected bool isStuntTimeOver;
    public B2_StuntState(Boss boss, BossFiniteStateMachine stateMachine, string animBoolName,D_BossStuntState stateData, King king) : base(boss, stateMachine, animBoolName)
    {
        this.king = king;
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        isStuntTimeOver = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + stateData.stuntTime)
        {
            isStuntTimeOver = true;
        }
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
