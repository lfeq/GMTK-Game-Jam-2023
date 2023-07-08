using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public static PlayerManager instance;

    private PlayerState playerState;
    private Animator animator;

    ManagerCollectorFrut managerCollectorFrut;

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

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Fruta_1")) {
            ManagerCollectorFrut.instance.getFruits(0);
        } else if (collision.CompareTag("Fruta_2")) {
            ManagerCollectorFrut.instance.getFruits(1);
        } else if (collision.CompareTag("Fruta_3")) {
            ManagerCollectorFrut.instance.getFruits(2);
        } else if (collision.CompareTag("Fruta_4")) {
            ManagerCollectorFrut.instance.getFruits(3);
        } else if (collision.CompareTag("Fruta_5")) {
            ManagerCollectorFrut.instance.getFruits(4);
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
