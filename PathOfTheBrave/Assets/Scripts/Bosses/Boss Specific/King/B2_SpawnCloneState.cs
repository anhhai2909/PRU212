using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B2_SpawnCloneState : BossCloneState
{
    private King king;
    public GameObject usedKing;

    public B2_SpawnCloneState(Boss boss, BossFiniteStateMachine stateMachine, string animBoolName, D_BossCloneState stateData, King king) : base(boss, stateMachine, animBoolName, stateData)
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

        if(isAliveTimeOver)
        {
            stateMachine.ChangeState(king.waitState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
