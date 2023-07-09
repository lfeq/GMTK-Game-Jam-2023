using UnityEngine;
using UnityEngine.Rendering;

public class MusicManager : MonoBehaviour {
    public static MusicManager s_instance;

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
    }

    public void PlayLevelMusic(AudioClip[] t_levelMusic) {
        levelMusic = t_levelMusic;
        int randomSong = Random.Range(0, levelMusic.Length);
        if (levelMusic[randomSong] == audioSource.clip) {
            return;
        }
        audioSource.clip = levelMusic[randomSong];
        audioSource.Play();
    }
}