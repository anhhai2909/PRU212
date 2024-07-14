using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWaitState : BossState
{
    protected D_BossWaitState stateData;
    protected float waitTime;
    protected bool isWaitTimeOver;

    public BossWaitState(Boss boss, BossFiniteStateMachine stateMachine, string animBoolName, D_BossWaitState stateData) : base(boss, stateMachine, animBoolName)
    {
        this.stateData = stateData;
        waitTime = stateData.waitTime;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        isWaitTimeOver = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + waitTime)
        {
            isWaitTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
