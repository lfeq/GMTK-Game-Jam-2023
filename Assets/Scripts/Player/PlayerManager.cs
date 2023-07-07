using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public static PlayerManager instance;

    private PlayerState playerState;
    private Animator animator;
    
    private void Awake() {
        instance = this;
        playerState = PlayerState.NullState;
    }

    private void Start() {
        animator = GetComponent<Animator>();
    }

    public void ChangePlayerStates(PlayerState newState) {
        if (playerState == newState) {
            return;
        }
        resetAnimatonParameters();
        playerState = newState;
        switch (playerState) {
            case PlayerState.NullState:
                break;
            case PlayerState.IdleState:
                animator.SetBool("IsIdle", true);
                break;
            case PlayerState.RunState:
                animator.SetBool("IsRun", true);
                break;
            case PlayerState.RunBack:
                animator.SetBool("IsRunBack", true);
                break;
            case PlayerState.RunFront:
                animator.SetBool("IsRunFront", true);
                break;
            case PlayerState.DeadState:
                animator.SetBool("IsDead", true);
                break;
        }
    }

    public PlayerState getPlayerState() {
        return playerState;
    }
    private void resetAnimatonParameters() {
        foreach (AnimatorControllerParameter parameter in animator.parameters) {
            if (parameter.type == AnimatorControllerParameterType.Bool) {
                animator.SetBool(parameter.name, false);
            }
        }
    }
}

public enum PlayerState {
    NullState,
    IdleState,
    RunState,
    RunFront,
    RunBack,
    DeadState
}
