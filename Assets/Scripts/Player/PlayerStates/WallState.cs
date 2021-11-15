using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallState : State
{
    private bool isTouchingWall;
    private bool JumpInput;
    private bool isInBounds;
    public WallState(Player player, FiniteStateMachine stateMachine, PlayerData playerData) : base(player, stateMachine, playerData)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isTouchingWall =  player.CheckIfBackTouchingWall();
    }

    public override void Enter()
    {
        base.Enter();

        isTouchingWall = true;
        JumpInput = false;

        player.SetVelocityZero();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        JumpInput = player.InputHandler.JumpInput;
        if (!isExitingState)
        {
            if (isTouchingWall && player.CheckIfValidJumpRange() && JumpInput)
            {
                Debug.Log(player.CheckIfValidJumpRange());
                player.StateMachine.ChangeState(player.PlayerJumpState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        player.SetVelocityY(player.InputHandler.NormInputY);
    }
}
