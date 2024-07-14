using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCloneState : BossState
{
    protected D_BossCloneState stateData;
    protected float cloneAliveTime;
    public bool isAliveTimeOver;

    public BossCloneState(Boss boss, BossFiniteStateMachine stateMachine, string animBoolName, D_BossCloneState stateData) : base(boss, stateMachine, animBoolName )
    {
        this.stateData = stateData;
        cloneAliveTime = stateData.cloneAliveTime;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        isAliveTimeOver = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(Time.time >= startTime + cloneAliveTime)
        {
            isAliveTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
