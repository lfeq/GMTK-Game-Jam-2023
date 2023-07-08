using UnityEngine;

public class MainMenuManager : MonoBehaviour {
    [SerializeField] private GameObject coverGameObject;

    private void Update() {
        if (Input.anyKey) {
            coverGameObject.SetActive(false);
        }
    }
}