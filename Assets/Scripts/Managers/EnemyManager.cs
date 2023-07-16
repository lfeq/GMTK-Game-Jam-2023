using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The EnemyManager class manages enemy instances during gameplay.
/// It handles adding, accessing, and positioning regular enemies and special enemies.
/// </summary>
public class EnemyManager : MonoBehaviour {

    /// <summary>
    /// The static instance of the EnemyManager, ensuring only one instance exists in the game.
    /// </summary>
    public static EnemyManager instance;

    [SerializeField] private List<GameObject> specialEnemies = new List<GameObject>();
    [SerializeField] private GameObject knightPrefab;

    private List<GameObject> enemies = new List<GameObject>();

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private void Start() {
        LevelManager.s_instance.ResetPositions();
    }

    /// <summary>
    /// Adds an enemy to the list of enemies managed by the EnemyManager.
    /// </summary>
    /// <param name="t_enemy">The enemy GameObject to add.</param>
    public void AddEnemyToList(GameObject t_enemy) {
        enemies.Add(t_enemy);
    }

    /// <summary>
    /// Returns the list of regular enemies managed by the EnemyManager.
    /// </summary>
    /// <returns>The list of regular enemy GameObjects.</returns>
    public List<GameObject> GetEnemyList() {
        return enemies;
    }

    /// <summary>
    /// Returns the list of special enemies that can be positioned during gameplay.
    /// </summary>
    /// <returns>The list of special enemy prefabs.</returns>
    public List<GameObject> GetSpecialEnemiesList() {
        return specialEnemies;
    }

    /// <summary>
    /// Positions special enemies based on the provided list of positions.
    /// </summary>
    /// <param name="positions">The list of positions to place the special enemies.</param>
    public void SetSpecialEnemiesPositions(List<Vector2> positions) {
        if (positions.Count == 0) {
            return;
        }
        for (int i = 0; i < positions.Count; i++) {
            Vector2 position = positions[i];
            specialEnemies[i].transform.position = position;
            specialEnemies[i].GetComponent<EnemyController>().StopAttack();
        }
    }

    /// <summary>
    /// Positions regular enemies based on the provided list of positions.
    /// </summary>
    /// <param name="positions">The list of positions to place the regular enemies.</param>
    public void SetEnemiesPositions(List<Vector2> positions) {
        if (positions.Count == 0) {
            return;
        }
        foreach (Vector2 position in positions) {
            GameObject tempEnemy = Instantiate(knightPrefab, position, Quaternion.identity);
            enemies.Add(tempEnemy);
            tempEnemy.GetComponent<EnemyController>().StopAttack();
        }
    }
}