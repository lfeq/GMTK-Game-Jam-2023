using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] private GameObject[] enemies;

    private void Start() {
        EnemyManager.instance.AddEnemySpawn(this);
    }

    public void SpawnEnemy() {
        int randomEnemy = Random.Range(0, enemies.Length);
        Instantiate(enemies[randomEnemy], transform.position, Quaternion.identity);
    }
}