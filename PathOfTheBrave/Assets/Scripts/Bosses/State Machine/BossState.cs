using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossState
{
    protected BossFiniteStateMachine stateMachine;
    protected Boss boss;
    protected float startTime;
    public string animBoolName;

    public BossState(Boss boss, BossFiniteStateMachine stateMachine, string animBoolName)
    {
        this.boss = boss;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        startTime = Time.time;
        boss.animator.SetBool(animBoolName, true);
    }
    public virtual void Exit()
    {
        boss.animator.SetBool(animBoolName, false);
    }
    public virtual void LogicUpdate()
    {

    }
    public virtual void PhysicsUpdate()
    {

    }

    public virtual void DoChecks()
    {

    }
}
