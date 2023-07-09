using UnityEngine;

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

    public void StartGame() {
        GameManager.s_instance.changeGameSate(GameState.LoadLevel);
    }
    public void MainMenu() {
        GameManager.s_instance.changeGameSate(GameState.MainMenu);
    }
    public void HowToPlay() {

        GameManager.s_instance.changeGameSate(GameState.HowToPlay);
    }

    public void Credits() {
        GameManager.s_instance.changeGameSate(GameState.Credits);
    }
}
