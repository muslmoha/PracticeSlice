using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected Player player;
    protected FiniteStateMachine stateMachine;
    protected PlayerData playerData;
    protected float startTime;

    protected bool isAnimationFinished;
    protected bool isExitingState;

    private string animBoolName;

    public State(Player player, FiniteStateMachine stateMachine, PlayerData playerData, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
    }

    //Not sure if I'm going to be using animBoolsYet
    public State(Player player, FiniteStateMachine stateMachine, PlayerData playerData)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
    }

    public virtual void Enter()
    {
        DoChecks();
        startTime = Time.time;
        player.Anim.SetBool(animBoolName, true);
        isAnimationFinished = false;
        isExitingState = false;
    }

    public virtual void Exit()
    {
        isAnimationFinished = true;
        player.Anim.SetBool(animBoolName, false);
        isExitingState = true;
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void DoChecks() { }

    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;


}
