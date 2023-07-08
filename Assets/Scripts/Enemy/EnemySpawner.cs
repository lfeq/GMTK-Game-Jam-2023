using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private float spawnTimeInSeconds = 3f;

    private float timer;

    private void Start() {
        EnemyManager.instance.AddEnemySpawn(this);
        timer = spawnTimeInSeconds;
    }

    private void Update() {
        timer -= Time.deltaTime;
        if (timer < 0) {
            SpawnEnemy();
        }
    }

    public void SpawnEnemy() {
        int randomEnemy = Random.Range(0, enemies.Length);
        Instantiate(enemies[randomEnemy], transform.position, Quaternion.identity);
        timer = spawnTimeInSeconds;
    }
}