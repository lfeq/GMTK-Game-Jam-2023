using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    public static LevelManager s_instance;

    private LevelState m_levelState;
    private Sprite enemySprite;
    private Vector2 playerPos;
    private List<Vector2> specialEnemiesPositions = new List<Vector2>();
    private List<Vector2> enemiesPositions = new List<Vector2>();
    private bool isReloadingLevel = false;
    [SerializeField] private AudioClip[] battleMusic;
    [SerializeField] private AudioClip[] escapingMusic;

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

    public void ChangeLevelState(LevelState t_levelState) {
        if (t_levelState == m_levelState) {
            return;
        }
        m_levelState = t_levelState;
        switch (m_levelState) {
            case LevelState.Escaping:
                ChangeEscapingScene();
                MusicManager.s_instance.PlayLevelMusic(escapingMusic);
                break;
            case LevelState.Dodging:
                ChangeDodgeScene();
                MusicManager.s_instance.PlayLevelMusic(battleMusic);
                break;
            default:
                throw new UnityException("Invalid Levela State");
        }
    }

    public Sprite getSprite() {
        return enemySprite;
    }

    public void RestartLevel() {
        GameManager.s_instance.changeGameSate(GameState.RestartLevel);
    }

    public void ReturnToMenu() {
        GameManager.s_instance.changeGameSate(GameState.LoadMainMenu);
        Destroy(gameObject);
    }

    public void ChangeEnemySprite(Sprite t_enemySprite) {
        enemySprite = t_enemySprite;
    }

    public void ResetPositions() {
        if (!isReloadingLevel) {
            return;
        }
        PlayerManager.instance.transform.position = playerPos;
        EnemyManager.instance.SetSpecialEnemiesPositions(specialEnemiesPositions);
        EnemyManager.instance.SetEnemiesPositions(enemiesPositions);
        specialEnemiesPositions.Clear();
        enemiesPositions.Clear();
    }

    private void ChangeDodgeScene() {
        isReloadingLevel = true;
        playerPos = PlayerManager.instance.transform.position;
        SafeSpecialEnemiesPositions();
        SafeEnemiesPositions();
        SceneManager.LoadScene("Level_2");
        //Cambiar de escena
    }

    private void SafeSpecialEnemiesPositions() {
        List<GameObject> specialEnemies = EnemyManager.instance.GetSpecialEnemiesList();
        foreach (GameObject enemy in specialEnemies) {
            specialEnemiesPositions.Add(enemy.transform.position);
        }
    }

    private void SafeEnemiesPositions() {
        List<GameObject> enemies = EnemyManager.instance.GetEnemyList();
        foreach (GameObject enemy in enemies) {
            enemiesPositions.Add(enemy.transform.position);
        }
    }

    private void ChangeEscapingScene() {
        StartCoroutine(loadScene("Level_1"));
    }

    private IEnumerator loadScene(string sceneName) {
        AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        while (!asyncLoadLevel.isDone) {
            yield return null;
        }
        //ResetPositions();
    }

    public LevelState getLevelState() { return m_levelState; }
}

public enum LevelState {
    Escaping,
    Dodging
}