using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B2_SpawnSpikeState : BossState
{
    private King king;
    public bool isCastTimeOver;

    public B2_SpawnSpikeState(Boss boss, BossFiniteStateMachine stateMachine, string animBoolName,King king) : base(boss, stateMachine, animBoolName)
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
        isCastTimeOver = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isCastTimeOver)
        {
            stateMachine.ChangeState(king.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
