using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : State
{

    private bool isTouchingWall;
    private bool isJumping;

    private float jumpingBuffer = 0.1f;

    public JumpState(Player player, FiniteStateMachine stateMachine, PlayerData playerData) : base(player, stateMachine, playerData)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isTouchingWall = player.CheckIfBackTouchingWall();
    }

    public override void Enter()
    {
        base.Enter();

        player.SetJumpStart();
        player.InputHandler.UseJumpInput();
        isJumping = true;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            if (isTouchingWall)
            {
                isJumping = false;
            }

            if (!isJumping && CheckJumpBuffer())
            {
                player.StateMachine.ChangeState(player.PlayerWallState);
            }
        }
    }

    public bool CheckJumpBuffer()
    {
        return (Time.time > startTime + jumpingBuffer);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        player.SetJumpVelocity(player.InputHandler.JumpDirectionInput);
    }
}
