using UnityEngine;

/// <summary>
/// The MusicManager class is responsible for managing the background music in the game.
/// It ensures that only one instance of the MusicManager exists and persists across scenes.
/// The class provides a method to play a random track from an array of level music.
/// </summary>
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

    /// <summary>
    /// Plays the background music for the level from the provided array of level music.
    /// Randomly selects a track from the array to play, ensuring no repetition of the current track.
    /// </summary>
    /// <param name="t_levelMusic">An array of AudioClips representing the level's background music tracks.</param>
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