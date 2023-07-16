using UnityEngine;

/// <summary>
/// The HowToPlayManager class manages the "How to Play" scene and its interactions.
/// It provides a method to return to the main menu when the "Main Menu" button is clicked.
/// </summary>
public class HowToPlayManager : MonoBehaviour {

    /// <summary>
    /// Called when the "Main Menu" button is clicked.
    /// Triggers a transition to the main menu by changing the game state to LoadMainMenu.
    /// </summary>
    public void MainMenu() {
        GameManager.s_instance.changeGameSate(GameState.LoadMainMenu);
    }
}