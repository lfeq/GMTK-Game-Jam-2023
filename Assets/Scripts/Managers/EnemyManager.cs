using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    public static EnemyManager instance;

    [SerializeField] private List<GameObject> specialEnemies = new List<GameObject>();

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
}