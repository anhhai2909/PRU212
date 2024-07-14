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
        getNextSkill();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(isIdleTimeOver)
        {
            switch (skills[currentSkillIndex])
            {
                case 0:
                    if (GameObject.FindGameObjectWithTag("Enemy") == null)
                    {
                        stateMachine.ChangeState(king.bulletState);
                    }
                    else
                    {
                        if (Random.Range(0, 2) == 0)
                        {
                            stateMachine.ChangeState(king.spikeState);
                        }
                        else
                        {
                            stateMachine.ChangeState(king.swordState);
                        }
                    }
                    break;
                case 1:
                    stateMachine.ChangeState(king.swordState);
                    break;
                case 2:
                    stateMachine.ChangeState(king.spikeState);
                    break;
            }
            getNextSkill();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void getNextSkill()
    {
        currentSkillIndex = (currentSkillIndex + 1) % skills.Length;
    }
}
