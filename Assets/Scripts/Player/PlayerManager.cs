using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerManager : MonoBehaviour {
    public static PlayerManager instance;

    [SerializeField] private AudioClip pickupAudioClip;

    private PlayerState playerState;
    private Animator animator;
    private List<int> pickedFruits = new List<int>();
    private AudioSource audioSource;

    private void Awake() {
        instance = this;
        playerState = PlayerState.NullState;
    }

    private void Start() {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        SetPickedFruitsList(LevelManager.s_instance.GetPickedFruits());
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

    public List<int> getPickedFruits() {
        return pickedFruits;
    }

    public void SetPickedFruitsList(List<int> t_fruitList) {
        if (t_fruitList.Count == 0) {
            return;
        }
        pickedFruits = t_fruitList;
        foreach (int fruit in pickedFruits) {
            ManagerCollectorFrut.instance.getFruits(fruit);
        }
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
            pickedFruits.Add(0);
        } else if (collision.CompareTag("Fruta_2")) {
            ManagerCollectorFrut.instance.getFruits(1);
            pickedFruits.Add(1);
        } else if (collision.CompareTag("Fruta_3")) {
            ManagerCollectorFrut.instance.getFruits(2);
            pickedFruits.Add(2);
        } else if (collision.CompareTag("Fruta_4")) {
            ManagerCollectorFrut.instance.getFruits(3);
            pickedFruits.Add(3);
        } else if (collision.CompareTag("Fruta_5")) {
            ManagerCollectorFrut.instance.getFruits(4);
            pickedFruits.Add(4);
        }
        audioSource.clip = pickupAudioClip;
        audioSource.Play();
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