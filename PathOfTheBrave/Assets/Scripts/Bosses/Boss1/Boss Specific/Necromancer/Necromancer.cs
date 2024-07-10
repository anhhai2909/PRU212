using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Necromancer : Boss
{
    public B1_IdleState idleState { get; private set; }
    public B1_MoveState moveState { get; private set; }
    public B1_RangedAttackState rangedAttackState { get; private set; }
    public B1_SpawnSkeletonState spawnSkeletonState { get; private set; }
    public B1_SpawnMeteorState spawnMeteorState { get; private set; }
    public B1_SpawnSpikeState spawnSpikeState { get; private set; }

    public BossHealthBar healthBar;

    public PortalScript portal;

    [SerializeField]
    private D_BossIdleState idleStateData;
    [SerializeField]
    private D_BossMoveState moveStateData;
    public override void Start()
    {
        base.Start();
        healthBar.setMaxHealth(bossData.maxHealth);
        moveState = new B1_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new B1_IdleState(this, stateMachine, "idle", idleStateData, this);
        rangedAttackState = new B1_RangedAttackState(this, stateMachine, "shoot", this);
        spawnSkeletonState = new B1_SpawnSkeletonState(this, stateMachine, "skeleton", this);
        spawnMeteorState = new B1_SpawnMeteorState(this, stateMachine, "meteor", this);
        spawnSpikeState = new B1_SpawnSpikeState(this, stateMachine, "spike", this);

        stateMachine.Initialize(idleState);
    }

    public override void Update()
    {
        base.Update();
        if (currentHealth <= 0)
        {
            portal.isEnabled = true;
            portal.isBossDead = true;
        }
        healthBar.setHealth(currentHealth);
        
    }
    
}
