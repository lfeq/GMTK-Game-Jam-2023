using UnityEngine;

public class MusicManager : MonoBehaviour {
    public static MusicManager s_instance;

    private AudioSource EscapingAudioClip;
    private AudioSource BattleAudioClip;
    private AudioSource audioSource;
    private AudioClip[] levelMusic;

    private void Awake() {
        if (s_instance != null && s_instance != this) {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        s_instance = this;
        audioSource = GetComponent<AudioSource>();
        // m_gameState = GameState.None;
    }

    public void PlayLevelMusic(AudioClip[] t_levelMusic) {
        levelMusic = t_levelMusic;
        int randomSong = Random.Range(0, levelMusic.Length);
        audioSource.clip = levelMusic[randomSong];
    }
}