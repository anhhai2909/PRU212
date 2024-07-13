using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B2_IdleState : BossIdleState
{
    private King king;
    private int currentSkillIndex = 0;
    private int[] skills = { 0, 1, 2 };
    public B2_IdleState(Boss boss, BossFiniteStateMachine stateMachine, string animBoolName, D_BossIdleState stateData, King king) : base(boss, stateMachine, animBoolName, stateData)
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
        switch (skills[currentSkillIndex])
        {
            //case 0:
            //    stateMachine.ChangeState(king.);
            //    break;
            //case 1:
            //    stateMachine.ChangeState(king.spawnSkeletonState);
            //    break;
            //case 2:
            //    stateMachine.ChangeState(king.spawnMeteorState);
            //    break;
        }
        getNextSkill();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void getNextSkill()
    {
        currentSkillIndex = (currentSkillIndex + 1) % skills.Length; // Increment and wrap around
    }
}
