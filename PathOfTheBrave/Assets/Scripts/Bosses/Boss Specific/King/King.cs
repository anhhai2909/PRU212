using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class King : Boss
{
    public B2_IdleState idleState {  get; private set; }
    public B2_MoveState moveState {  get; private set; }
    public B2_SpawnSpikeState spikeState {  get; private set; }
    public B2_SpawnSwordState swordState {  get; private set; }
    public B2_StuntState stuntState {  get; private set; }
    public B2_SpawnBulletState bulletState {  get; private set; }

    public BossHealthBar healthBar;

    [SerializeField]
    private D_BossIdleState idleStateData;
    [SerializeField]
    private D_BossMoveState moveStateData;
    [SerializeField]
    private D_BossStuntState stuntStateData;

    public override void Start()
    {
        base.Start();
        healthBar.setMaxHealth(bossData.maxHealth);
        idleState = new B2_IdleState(this, stateMachine, "idle", idleStateData, this);
        moveState = new B2_MoveState(this, stateMachine, "move", moveStateData,this);
        stuntState = new B2_StuntState(this,stateMachine,"stunt",stuntStateData,this);
        spikeState = new B2_SpawnSpikeState(this, stateMachine, "spike", this);
        swordState = new B2_SpawnSwordState(this, stateMachine, "sword", this);
        bulletState = new B2_SpawnBulletState(this, stateMachine, "bullet", this);
        stateMachine.Initialize(idleState);
    }
    public override void Update()
    {
        base.Update();
        healthBar.setHealth(currentHealth);
    }

    public override void FacingToPlayer()
    {
        facingDirection *= -1;
        Vector2 target = new Vector2(player.position.x, rb.position.y);

        // Adjust the facing logic to flip the sprite correctly
        if ((target.x < rb.position.x && isFacingRight) || (target.x > rb.position.x && !isFacingRight))
        {
            isFacingRight = !isFacingRight;

            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}
