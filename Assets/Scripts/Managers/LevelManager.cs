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
                //Cambiar a escena de persecucion
                break;
            case LevelState.Dodging:
                ChangeDodgeScene();
                break;
            default:
                throw new UnityException("Invalid Levela State");
        }
    }

    private void ChangeEscapingScene() {
        StartCoroutine(loadScene("Copia_Level_1_Test")); // Escena temporal
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

    public Sprite getSprite() {
        return enemySprite;
    }

    public void ResetPositions() {
        EnemyManager.instance.SetSpecialEnemiesPositions(specialEnemiesPositions);
        EnemyManager.instance.SetEnemiesPositions(enemiesPositions);
        PlayerManager.instance.transform.position = playerPos;
        specialEnemiesPositions.Clear();
        enemiesPositions.Clear();
    }

    private void ChangeDodgeScene() {
        playerPos = PlayerManager.instance.transform.position;
        SafeSpecialEnemiesPositions();
        SafeEnemiesPositions();
        SceneManager.LoadScene("Level_2");
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

    private IEnumerator loadScene(string sceneName) {
        var asyncLoadLevel = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        while (!asyncLoadLevel.isDone) {
            yield return null;
        }
        ResetPositions();
    }
}

public enum LevelState {
    Escaping,
    Dodging
}