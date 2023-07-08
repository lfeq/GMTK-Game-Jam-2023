using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruts : MonoBehaviour
{
    [SerializeField] private ManagerCollectorFrut managerCollectorFrut;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            managerCollectorFrut.fruts++;
            this.gameObject.SetActive(false);
        }
    }
}
