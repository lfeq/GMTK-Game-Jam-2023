using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager s_instance;

    AudioSource EscapingAudioClip;
    AudioSource BattleAudioClip;

    private void Awake() {
        if (s_instance != null && s_instance != this) {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        s_instance = this;
       // m_gameState = GameState.None;
    }
}
