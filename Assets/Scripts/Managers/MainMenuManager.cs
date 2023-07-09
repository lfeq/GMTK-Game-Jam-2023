using UnityEngine;

public class MainMenuManager : MonoBehaviour {
    [SerializeField] private GameObject coverGameObject;
    [SerializeField] private AudioClip[] levelMusic;

    private bool skipedCover = false;

    private void Start() {
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
}