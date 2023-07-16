using UnityEngine;

/// <summary>
/// The MainMenuManager class manages the main menu scene and its interactions.
/// It handles starting the game, displaying the how-to-play and credits screens, and playing background music.
/// </summary>
public class MainMenuManager : MonoBehaviour {
    [SerializeField] private GameObject coverGameObject;
    [SerializeField] private AudioClip[] levelMusic;

    private bool skipedCover = false;

    private void Start() {
        MusicManager.s_instance.PlayLevelMusic(levelMusic);
    }

    private void Update() {
        if (skipedCover) {
            return;
        }
        if (Input.anyKey) {
            coverGameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Starts the game by changing the game state to LoadLevel.
    /// </summary>
    public void StartGame() {
        GameManager.s_instance.changeGameSate(GameState.LoadLevel);
    }

    /// <summary>
    /// Returns to the main menu by changing the game state to MainMenu.
    /// </summary>
    public void MainMenu() {
        GameManager.s_instance.changeGameSate(GameState.MainMenu);
    }

    /// <summary>
    /// Shows the how-to-play screen by changing the game state to HowToPlay.
    /// </summary>
    public void HowToPlay() {
        GameManager.s_instance.changeGameSate(GameState.HowToPlay);
    }

    /// <summary>
    /// Shows the credits screen by changing the game state to Credits.
    /// </summary>
    public void Credits() {
        GameManager.s_instance.changeGameSate(GameState.Credits);
    }
}