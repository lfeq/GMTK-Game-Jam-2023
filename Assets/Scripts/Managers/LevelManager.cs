using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The LevelManager class manages the level state and scene transitions during gameplay.
/// It handles switching between escaping and dodging scenes, restarting the level, and returning to the main menu.
/// </summary>
public class LevelManager : MonoBehaviour {

    /// <summary>
    /// The static instance of the LevelManager, ensuring only one instance exists in the game.
    /// </summary>
    public static LevelManager s_instance;

    private LevelState m_levelState;
    private Sprite enemySprite;
    private Vector2 playerPos;
    private List<Vector2> specialEnemiesPositions = new List<Vector2>();
    private List<Vector2> enemiesPositions = new List<Vector2>();
    private bool isReloadingLevel = false;
    private List<int> pickedFruits = new List<int>();
    private int timesCaught = 0;

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
        MusicManager.s_instance.PlayLevelMusic(escapingMusic);
    }

    /// <summary>
    /// Changes the level state to the provided new state and performs appropriate actions based on the state.
    /// </summary>
    /// <param name="t_levelState">The new LevelState to transition to.</param>
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
            case LevelState.LoadingScene:
                break;
            default:
                throw new UnityException("Invalid Levela State");
        }
    }

    /// <summary>
    /// Returns the current sprite of the enemy that collisioned with the player.
    /// </summary>
    /// <returns>The enemy sprite.</returns>
    public Sprite getSprite() {
        return enemySprite;
    }

    /// <summary>
    /// Triggers a restart of the current level by changing the game state to RestartLevel.
    /// </summary>
    public void RestartLevel() {
        GameManager.s_instance.changeGameSate(GameState.RestartLevel);
    }

    /// <summary>
    /// Returns to the main menu scene by changing the game state to LoadMainMenu and destroying the LevelManager instance.
    /// </summary>
    public void ReturnToMenu() {
        GameManager.s_instance.changeGameSate(GameState.LoadMainMenu);
        Destroy(gameObject);
    }

    /// <summary>
    /// Changes the sprite to the sprite that collisioned with the player.
    /// </summary>
    /// <param name="t_enemySprite">The new enemy sprite to be used.</param>
    public void ChangeEnemySprite(Sprite t_enemySprite) {
        enemySprite = t_enemySprite;
    }

    /// <summary>
    /// Resets the positions of special enemies and regular enemies during level reload.
    /// </summary>
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

    /// <summary>
    /// Returns the list of picked fruits by the player.
    /// </summary>
    /// <returns>The list of picked fruit indices.</returns>
    public List<int> GetPickedFruits() {
        return pickedFruits;
    }

    /// <summary>
    /// Returns the number of times the player has been caught by enemies.
    /// </summary>
    /// <returns>The number of times the player has been caught.</returns>
    public int getTimesCaught() {
        return timesCaught;
    }

    /// <summary>
    /// Sets the number of times the player has been caught by enemies.
    /// </summary>
    /// <param name="t_timesCaught">The new value for the times caught.</param>
    public void setTimesCaught(int t_timesCaught) {
        timesCaught = t_timesCaught;
    }

    /// <summary>
    /// Changes to the dodge scene by setting the reloading level flag, storing current positions, and loading the level_2 scene.
    /// </summary>
    private void ChangeDodgeScene() {
        isReloadingLevel = true;
        playerPos = PlayerManager.instance.transform.position;
        SaveSpecialEnemiesPositions();
        SaveEnemiesPositions();
        pickedFruits = PlayerManager.instance.getPickedFruits();
        SceneManager.LoadScene("Level_2");
        //Cambiar de escena
    }

    /// <summary>
    /// Stores the positions of special enemies in the specialEnemiesPositions list.
    /// </summary>
    private void SaveSpecialEnemiesPositions() {
        List<GameObject> specialEnemies = EnemyManager.instance.GetSpecialEnemiesList();
        foreach (GameObject enemy in specialEnemies) {
            specialEnemiesPositions.Add(enemy.transform.position);
        }
    }

    /// <summary>
    /// Stores the positions of regular enemies in the enemiesPositions list.
    /// </summary>
    private void SaveEnemiesPositions() {
        List<GameObject> enemies = EnemyManager.instance.GetEnemyList();
        foreach (GameObject enemy in enemies) {
            enemiesPositions.Add(enemy.transform.position);
        }
    }

    /// <summary>
    /// Changes to the escaping scene by loading the level_1 scene asynchronously.
    /// </summary>
    private void ChangeEscapingScene() {
        StartCoroutine(loadScene("Level_1"));
    }

    /// <summary>
    /// Loads the specified scene name asynchronously and waits until the scene is fully loaded.
    /// </summary>
    /// <param name="sceneName">The name of the scene to load.</param>
    private IEnumerator loadScene(string sceneName) {
        AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        while (!asyncLoadLevel.isDone) {
            yield return null;
        }
        //ResetPositions();
    }

    /// <summary>
    /// Returns the current level state.
    /// </summary>
    /// <returns>The current LevelState.</returns>
    public LevelState getLevelState() {
        return m_levelState;
    }
}

/// <summary>
/// The possible states of the game level.
/// </summary>
public enum LevelState {
    Escaping,
    Dodging,
    LoadingScene
}