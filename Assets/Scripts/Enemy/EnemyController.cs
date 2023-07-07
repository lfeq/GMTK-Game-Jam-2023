using UnityEngine;

public class EnemyController : MonoBehaviour {
    [SerializeField] private float movementSpeed;

    private Transform player;

    private void Start() {
        player = PlayerManager.instance.transform;
    }

    private void Update() {
        float step = movementSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, player.position, step);
    }
}