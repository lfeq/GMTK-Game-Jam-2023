using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonePatrol : MonoBehaviour
{
    [SerializeField]private EnemyController enemyController;

    private void OnTriggerStay2D(Collider2D collision) {
        if(collision.CompareTag("Player")) {
            enemyController.ChanceState(EnemyState.chasing);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            enemyController.ChanceState(EnemyState.patroling);
        }
    }
}
