using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B2_SpawnBulletState : BossState
{
    private King king;
    public B2_SpawnBulletState(Boss boss, BossFiniteStateMachine stateMachine, string animBoolName, King king) : base(boss, stateMachine, animBoolName)
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
