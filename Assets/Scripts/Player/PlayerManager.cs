using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The PlayerManager class handles the player's states, animations, and interactions with fruits.
/// It keeps track of the player's current state, picked fruits, and plays audio clips for fruit pickups.
/// </summary>
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

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Fruta_1")) {
            ManagerCollectorFrut.instance.getFruits(0);
            pickedFruits.Add(0);
            audioSource.clip = pickupAudioClip;
            audioSource.Play();
        } else if (collision.CompareTag("Fruta_2")) {
            ManagerCollectorFrut.instance.getFruits(1);
            pickedFruits.Add(1);
            audioSource.clip = pickupAudioClip;
            audioSource.Play();
        } else if (collision.CompareTag("Fruta_3")) {
            ManagerCollectorFrut.instance.getFruits(2);
            pickedFruits.Add(2);
            audioSource.clip = pickupAudioClip;
            audioSource.Play();
        } else if (collision.CompareTag("Fruta_4")) {
            ManagerCollectorFrut.instance.getFruits(3);
            pickedFruits.Add(3);
            audioSource.clip = pickupAudioClip;
            audioSource.Play();
        } else if (collision.CompareTag("Fruta_5")) {
            ManagerCollectorFrut.instance.getFruits(4);
            pickedFruits.Add(4);
            audioSource.clip = pickupAudioClip;
            audioSource.Play();
        }
    }

    /// <summary>
    /// Changes the player's state and triggers the corresponding animation.
    /// </summary>
    /// <param name="newState">The new state to set for the player.</param>
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

    /// <summary>
    /// Gets the current state of the player.
    /// </summary>
    /// <returns>The current state of the player.</returns>
    public PlayerState getPlayerState() {
        return playerState;
    }

    /// <summary>
    /// Gets the list of fruits picked by the player.
    /// </summary>
    /// <returns>The list of fruits picked by the player.</returns>
    public List<int> getPickedFruits() {
        return pickedFruits;
    }

    /// <summary>
    /// Sets the list of picked fruits for the player and updates the fruit collector accordingly.
    /// </summary>
    /// <param name="t_fruitList">The list of picked fruits to set.</param>
    public void SetPickedFruitsList(List<int> t_fruitList) {
        if (t_fruitList.Count == 0) {
            return;
        }
        pickedFruits = t_fruitList;
        foreach (int fruit in pickedFruits) {
            ManagerCollectorFrut.instance.getFruits(fruit);
        }
    }

    /// <summary>
    /// Resets all animation parameters to their default values.
    /// </summary>
    private void resetAnimatonParameters() {
        foreach (AnimatorControllerParameter parameter in animator.parameters) {
            if (parameter.type == AnimatorControllerParameterType.Bool) {
                animator.SetBool(parameter.name, false);
            }
        }
    }
}

/// <summary>
/// Enum representing different player states.
/// </summary>
public enum PlayerState {
    NullState,
    IdleState,
    RunState,
    RunFront,
    RunBack,
    DeadState
}