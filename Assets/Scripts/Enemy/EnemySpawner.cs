using UnityEngine;

/// <summary>
/// The EnemySpawner class handles the spawning of enemies at regular intervals.
/// It randomly selects an enemy from a list of enemy prefabs and instantiates it at the spawner's position.
/// </summary>
public class EnemySpawner : MonoBehaviour {
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private float spawnTimeInSeconds = 3f;

    private float timer;

    private void Start() {
        timer = 3;
    }

    private void Update() {
        timer -= Time.deltaTime;
        if (timer < 0) {
            SpawnEnemy();
        }
    }

    /// <summary>
    /// Spawns a random enemy at the spawner's position and adds it to the enemy manager's list.
    /// Resets the spawn timer for the next enemy spawn.
    /// </summary>
    public void SpawnEnemy() {
        int randomEnemy = Random.Range(0, enemies.Length);
        GameObject tempEnemy = Instantiate(enemies[randomEnemy], transform.position, Quaternion.identity);
        EnemyManager.instance.AddEnemyToList(tempEnemy);
        timer = spawnTimeInSeconds;
    }
}