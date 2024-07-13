using CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtState : PlayerState
{
    private readonly Movement movement;
    private readonly CollisionSenses collisionSenses;

    public PlayerHurtState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        movement = core.GetCoreComponent<Movement>();
        collisionSenses = core.GetCoreComponent<CollisionSenses>();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + playerData.hurtTime || collisionSenses.Ground)
        {
            movement.SetVelocityX(0);
            stateMachine.ChangeState(player.IdleState);
        }
    }
}
