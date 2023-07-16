using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The GameManager class manages the game state and scene transitions.
/// It controls various states of the game, such as loading main menu, levels, showing how-to-play and credits, handling game over and win conditions, and quitting the game.
/// </summary>
public class GameManager : MonoBehaviour {

    /// <summary>
    /// The static instance of the GameManager, ensuring only one instance exists in the game.
    /// </summary>
    public static GameManager s_instance;

    private GameState m_gameState;
    private string m_newLevel;

    private void Awake() {
        if (s_instance != null && s_instance != this) {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        s_instance = this;
        m_gameState = GameState.None;
    }

    /// <summary>
    /// Changes the game state to the provided new state and performs appropriate actions based on the state.
    /// </summary>
    /// <param name="t_newState">The new GameState to transition to.</param>
    public void changeGameSate(GameState t_newState) {
        if (m_gameState == t_newState) {
            return;
        }
        m_gameState = t_newState;
        switch (m_gameState) {
            case GameState.None:
                break;
            case GameState.LoadMainMenu:
                loadMenu();
                break;
            case GameState.MainMenu:
                break;
            case GameState.LoadLevel:
                loadLevel();
                break;
            case GameState.HowToPlay:
                howToPlay();
                break;
            case GameState.Playing:
                break;
            case GameState.RestartLevel:
                restartLevel();
                break;
            case GameState.GameOver:
                gameOver();
                Destroy(LevelManager.s_instance.gameObject);
                break;
            case GameState.Credits:
                credits();
                break;
            case GameState.Win:
                win();
                Destroy(LevelManager.s_instance.gameObject);
                break;
            case GameState.QuitGame:
                quitGame();
                break;
            default:
                throw new UnityException("Invalid Game State");
        }
    }

    /// <summary>
    /// Changes the game state based on the provided string representation of the GameState (for editor usage).
    /// </summary>
    /// <param name="t_newState">The string representation of the new GameState to transition to.</param>
    public void changeGameStateInEditor(string t_newState) {
        changeGameSate((GameState)System.Enum.Parse(typeof(GameState), t_newState));
    }

    /// <summary>
    /// Returns the current game state.
    /// </summary>
    /// <returns>The current GameState.</returns>
    public GameState getGameState() {
        return m_gameState;
    }

    /// <summary>
    /// Sets the new level name to be loaded during the transition to LoadLevel state.
    /// </summary>
    /// <param name="t_newLevel">The name of the new level to be loaded.</param>
    public void setNewLevelName(string t_newLevel) {
        m_newLevel = t_newLevel;
    }

    /// <summary>
    /// Loads the main menu scene.
    /// </summary>
    public void loadMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Loads the how-to-play scene.
    /// </summary>
    public void howToPlay() {
        SceneManager.LoadScene("HowToPlay");
    }

    /// <summary>
    /// Loads the credits scene.
    /// </summary>
    public void credits() {
        SceneManager.LoadScene("Credits");
    }

    /// <summary>
    /// Loads the game over scene and destroys the LevelManager instance.
    /// </summary>
    public void gameOver() {
        SceneManager.LoadScene("GameOver");
    }

    /// <summary>
    /// Restarts the current level scene.
    /// </summary>
    private void restartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Loads the level scene.
    /// </summary>
    private void loadLevel() {
        SceneManager.LoadScene("Level_1");
    }

    /// <summary>
    /// Loads the win scene and destroys the LevelManager instance.
    /// </summary>
    public void win() {
        SceneManager.LoadScene("Win");
    }

    /// <summary>
    /// Quits the application.
    /// </summary>
    private void quitGame() {
        Application.Quit();
    }
}

/// <summary>
/// Enumeration representing the different states of the game.
/// </summary>
public enum GameState {
    None,
    LoadMainMenu,
    MainMenu,
    HowToPlay,
    LoadLevel,
    Playing,
    RestartLevel,
    GameOver,
    Credits,
    Win,
    QuitGame,
}