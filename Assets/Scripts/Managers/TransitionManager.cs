using UnityEngine;

/// <summary>
/// The TransitionManager class handles scene transitions and manages the transition animator.
/// It ensures that only one instance of the TransitionManager exists and persists across scenes.
/// The class provides methods to trigger different types of scene transitions and state changes.
/// </summary>
public class TransitionManager : MonoBehaviour {
    public static TransitionManager instance;

    [SerializeField] private Animator transitionAnimator;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    /// <summary>
    /// Starts the transition animation by enabling the transition animator.
    /// </summary>
    public void StartTransition() {
        transitionAnimator.enabled = true;
    }

    /// <summary>
    /// Changes the level state to Dodging, transitioning to the battle scene.
    /// </summary>
    public void ChangeToBattleScene() {
        LevelManager.s_instance.ChangeLevelState(LevelState.Dodging);
    }

    /// <summary>
    /// Changes the game state to LoadLevel, transitioning to the playing scene.
    /// </summary>
    public void ChangeToPlayingScene() {
        GameManager.s_instance.changeGameSate(GameState.LoadLevel);
    }

    /// <summary>
    /// Changes the level state to Escaping, transitioning to the playing scene.
    /// Used when level was already started
    /// </summary>
    public void ChangeToPlayingScene2() {
        LevelManager.s_instance.ChangeLevelState(LevelState.Escaping);
    }
}