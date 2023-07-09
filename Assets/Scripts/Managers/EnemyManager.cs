using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
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

    public void AddEnemyToList(GameObject t_enemy) {
        enemies.Add(t_enemy);
    }

    public List<GameObject> GetEnemyList() {
        return enemies;
    }

    public List<GameObject> GetSpecialEnemiesList() {
        return specialEnemies;
    }

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