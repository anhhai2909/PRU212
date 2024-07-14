using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B1_IdleState : BossIdleState
{
    private Necromancer necromancer;
    private int currentSkillIndex = 0;
    private int[] skills = { 0, 1, 2 };
    private int phase2SkillIndex = 0;
    private int[] phase2Skills = { 0, 1, 2 ,3};

    private Vector3 positionStage1 = new Vector3(-5f, -2.7f, 0f);
    private Vector3 positionStage2 = new Vector3(12f, 1f, 0f);
    private Vector3 positionStage3 = new Vector3(1.8f, 3f, 0f);

    public B1_IdleState(Boss boss, BossFiniteStateMachine stateMachine, string animBoolName, D_BossIdleState stateData, Necromancer necromancer) : base(boss, stateMachine, animBoolName, stateData)
    {
        this.necromancer = necromancer;
    }

    public override void Enter()
    {
        base.Enter();
        getNextSkill(); // Get the next skill in sequence
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        CheckHealthThresholds();
        if (isIdleTimeOver && necromancer.currentHealth >= 500)
        {
            switch (skills[currentSkillIndex])
            {
                case 0:
                    stateMachine.ChangeState(necromancer.rangedAttackState);
                    break;
                case 1:
                    stateMachine.ChangeState(necromancer.spawnSkeletonState);
                    break;
                case 2:
                    stateMachine.ChangeState(necromancer.spawnMeteorState);
                    break;
            }
            getNextSkill();
        }
        else if (isIdleTimeOver && necromancer.currentHealth < 500 && necromancer.currentHealth > 0)
        {
            switch (phase2Skills[phase2SkillIndex])
            {
                case 0:
                    stateMachine.ChangeState(necromancer.spawnSpikeState);
                    break;
                case 1:
                    stateMachine.ChangeState(necromancer.spawnSkeletonState);
                    break;
                case 2:
                    stateMachine.ChangeState(necromancer.spawnMeteorState);
                    break;
                case 3:
                    stateMachine.ChangeState(necromancer.rangedAttackState);
                    break;
            }
            getNextSkillPhase2();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void getNextSkill()
    {
        currentSkillIndex = (currentSkillIndex + 1) % skills.Length; // Increment and wrap around
    }

    public void getNextSkillPhase2()
    {
        phase2SkillIndex = (phase2SkillIndex + 1) % phase2Skills.Length; // Increment and wrap around
    }
    private void CheckHealthThresholds()
    {
        if (necromancer.currentHealth >= 600 && necromancer.currentHealth <= 800)
        {
            necromancer.transform.position = positionStage1;
        }
        else if (necromancer.currentHealth >= 300 && necromancer.currentHealth < 600)
        {
            necromancer.transform.position = positionStage2;
        }
        else if (necromancer.currentHealth > 0 && necromancer.currentHealth < 300)
        {
            necromancer.transform.position = positionStage3;
        }
    }
}
