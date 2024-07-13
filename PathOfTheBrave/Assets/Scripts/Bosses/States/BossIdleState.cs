using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : BossState
{
    protected D_BossIdleState stateData;
    protected bool flipAfterIdle;
    protected float idleTime;
    protected bool isIdleTimeOver;
    public BossIdleState(Boss boss, BossFiniteStateMachine stateMachine, string animBoolName, D_BossIdleState stateData) : base(boss, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        boss.SetVelocity(0f);
        isIdleTimeOver = false;
        setRandomIdleTime();
    }

    public override void Exit()
    {
        base.Exit();
            boss.FacingToPlayer();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(Time.time >= startTime + idleTime)
        {
            isIdleTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public void SetFlipAfterIdle(bool flip)
    {
        flipAfterIdle = flip;
    }

    private void setRandomIdleTime()
    {
        idleTime = Random.Range(stateData.minIdleTime, stateData.maxIdleTime);
    }
}
