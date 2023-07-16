using UnityEngine;

/// <summary>
/// The PlayerShoot class handles the player's shooting mechanics.
/// It spawns projectiles at regular intervals based on the specified time.
/// </summary>
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

    /// <summary>
    /// Spawns a projectile at the player's position and resets the timer.
    /// </summary>
    private void Spawn() {
        timer = spawnProjectileTimeInSeconds;
        Instantiate(projectilePrefab, transform.position, Quaternion.identity);
    }
}