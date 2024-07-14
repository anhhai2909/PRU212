using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStuntState : BossState
{
    protected D_BossStuntState stateData;
    protected float stuntTime;
    protected bool isStuntTimeOver;

    public BossStuntState(Boss boss, BossFiniteStateMachine stateMachine, string animBoolName, D_BossStuntState stateData) : base(boss, stateMachine, animBoolName)
    {
        this.stateData = stateData;
        stuntTime = stateData.stuntTime;
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
        if (Time.time >= startTime + stuntTime)
        {
            isStuntTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
