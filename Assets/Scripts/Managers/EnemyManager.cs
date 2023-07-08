using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    public static EnemyManager instance;

    [SerializeField] private float spawnTimeInSeconds = 3f;

    private float timeUntilNextSpawn;
    private List<EnemySpawner> enemySpawners = new List<EnemySpawner>();

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private void Start() {
        timeUntilNextSpawn = spawnTimeInSeconds;
    }

    private void Update() {
        timeUntilNextSpawn -= Time.deltaTime;
        if (timeUntilNextSpawn < 0) {
            Spawn();
        }
    }

    public void AddEnemySpawn(EnemySpawner spawner) {
        enemySpawners.Add(spawner);
    }

    private void Spawn() {
        timeUntilNextSpawn = spawnTimeInSeconds;
        if (enemySpawners.Count == 0) { return; }
        int randomSpawn = Random.Range(0, enemySpawners.Count);
        enemySpawners[randomSpawn].SpawnEnemy();
        enemySpawners.RemoveAt(randomSpawn);
    }
}