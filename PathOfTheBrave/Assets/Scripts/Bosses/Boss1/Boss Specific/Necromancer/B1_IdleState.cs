using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B1_IdleState : BossIdleState
{
    private Necromancer necromancer;
    private int randSkill;
    public int count = 0;
    public B1_IdleState(Boss boss, BossFiniteStateMachine stateMachine, string animBoolName, D_BossIdleState stateData, Necromancer necromancer) : base(boss, stateMachine, animBoolName, stateData)
    {
        this.necromancer = necromancer;
    }

    public override void Enter()
    {
        base.Enter();
        getRandomSkill();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isIdleTimeOver && (necromancer.currentHealth >= 200 || necromancer.currentHealth <= 100))
        {
            Debug.Log("Phase1");
            switch (randSkill)
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
        }
        else if (isIdleTimeOver && necromancer.currentHealth < 200 && necromancer.currentHealth > 100 )
        {
            Debug.Log("Phase2");
            stateMachine.ChangeState(necromancer.spawnSpikeState);
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void getRandomSkill()
    {
        randSkill = Random.Range(0, 3);
    }
}
