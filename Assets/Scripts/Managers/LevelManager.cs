using UnityEngine;

public class LevelManager : MonoBehaviour {
    public static LevelManager s_instance;

    private LevelState m_levelState;
    private Sprite enemySprite;

    private void Awake() {
        if (s_instance != null && s_instance != this) {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
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
                //Cambiar a escena de persecucion
                break;
            case LevelState.Dodging:
                //Cambiar a escena de atrapar
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
        Destroy(gameObject);
    }

    public void changeEnemySprite(Sprite t_enemySprite) {
        enemySprite = t_enemySprite;
    }
}

public enum LevelState {
    Escaping,
    Dodging
}