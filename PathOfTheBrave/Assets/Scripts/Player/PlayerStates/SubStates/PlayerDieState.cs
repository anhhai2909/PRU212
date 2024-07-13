using CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieState : PlayerState
{
    private readonly Movement movement;

    public PlayerDieState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        movement = core.GetCoreComponent<Movement>();
    }

    public override void Enter()
    {
        base.Enter();
        player.gameObject.tag = "Death";
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        movement.SetVelocityX(0);
    }
}
