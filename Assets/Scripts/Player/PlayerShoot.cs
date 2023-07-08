using UnityEngine;

public class PlayerShoot : MonoBehaviour {
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float spawnProjectileTimeInSeconds;

    private float timer;

    private void Start() {
        timer = spawnProjectileTimeInSeconds;
    }

    private void Update() {
        timer -= Time.deltaTime;
        if (timer <= 0) {
            Spawn();
        }
    }

    private void Spawn() {
        timer = spawnProjectileTimeInSeconds;
        Instantiate(projectilePrefab, transform.position, Quaternion.identity);
    }
}