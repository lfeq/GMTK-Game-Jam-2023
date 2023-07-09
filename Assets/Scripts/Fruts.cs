using UnityEngine;

public class Fruts : MonoBehaviour {
    [SerializeField] private ManagerCollectorFrut managerCollectorFrut;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            gameObject.SetActive(false);
        }
    }
}