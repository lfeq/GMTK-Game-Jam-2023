using UnityEngine;

/// <summary>
/// The Fruts class represents a collectible fruit in the game.
/// It handles interactions with the player, making the fruit inactive when collected.
/// </summary>
public class Fruts : MonoBehaviour {
    [SerializeField] private ManagerCollectorFrut managerCollectorFrut;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            gameObject.SetActive(false);
        }
    }
}