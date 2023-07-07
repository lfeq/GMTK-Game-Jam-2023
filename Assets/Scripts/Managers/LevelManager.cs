using UnityEngine;

public class LevelManager : MonoBehaviour {
    public static LevelManager s_instance;

    private LevelState m_levelState;

    private void Awake() {
        if (s_instance != null && s_instance != this) {
            Destroy(gameObject);
            return;
        }
        s_instance = this;
    }

    private void Start() {
        GameManager.s_instance.changeGameSate(GameState.Playing);
    }

    public void changeLevelState(LevelState t_levelState) {
        if (t_levelState == m_levelState) {
            return;
        }
        m_levelState = t_levelState;
        switch (m_levelState) {
            case LevelState.Escaping:
                break;
            case LevelState.Dodging:
                break;
            default:
                throw new UnityException("Invalid Levela State");
        }
    }

    public void restartLevel() {
        GameManager.s_instance.changeGameSate(GameState.RestartLevel);
    }

    public void returnToMenu() {
        GameManager.s_instance.changeGameSate(GameState.LoadMainMenu);
    }
}

public enum LevelState {
    Escaping,
    Dodging
}