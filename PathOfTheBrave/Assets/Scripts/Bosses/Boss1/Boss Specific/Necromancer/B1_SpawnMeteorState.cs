using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B1_SpawnMeteorState : BossState
{
    private Necromancer necromancer;
    public bool isCastTimeOver;

    public B1_SpawnMeteorState(Boss boss, BossFiniteStateMachine stateMachine, string animBoolName, Necromancer necromancer) : base(boss, stateMachine, animBoolName)
    {
        this.necromancer = necromancer;
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
            stateMachine.ChangeState(necromancer.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }
}
