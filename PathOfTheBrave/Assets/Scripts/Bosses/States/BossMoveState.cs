using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoveState : BossState
{
    protected D_BossMoveState stateData;
    protected float moveTime;
    protected bool isMoveTimeOver;
    public BossMoveState(Boss boss, BossFiniteStateMachine stateMachine, string animBoolName, D_BossMoveState stateData) : base(boss, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        isMoveTimeOver = false;
        boss.SetVelocity(stateData.movementSpeed);
        setRandomMoveTime();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + moveTime)
        {
            isMoveTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void setRandomMoveTime()
    {
        moveTime = Random.Range(stateData.minMoveTime, stateData.maxMoveTime);
    }
}
