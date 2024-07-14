using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B1_MoveState : BossMoveState
{
    private Necromancer necromancer;
    public B1_MoveState(Boss boss, BossFiniteStateMachine stateMachine, string animBoolName, D_BossMoveState stateData, Necromancer necromancer) : base(boss, stateMachine, animBoolName, stateData)
    {

        this.necromancer = necromancer;
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
        if (isMoveTimeOver)
        {
            necromancer.idleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(necromancer.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
