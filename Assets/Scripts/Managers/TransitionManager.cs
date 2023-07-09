using UnityEngine;

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

    public void StartTransition() {
        transitionAnimator.enabled = true;
    }

    public void ChangeToBattleScene() {
        LevelManager.s_instance.ChangeLevelState(LevelState.Dodging);
    }

    public void ChangeToPlayingScene() {
        GameManager.s_instance.changeGameSate(GameState.LoadLevel);
    }
}