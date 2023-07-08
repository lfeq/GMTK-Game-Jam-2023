using UnityEngine;

public class MoveCatchingBall : MonoBehaviour
{
    [SerializeField] float ballSpeed = 2f;
    Transform playerPosition;

    private void Awake() {
        playerPosition = PlayerControllerBattle.instance.transform;
        Debug.Log("Player pos start: " + playerPosition.position);
    }

    private void FixedUpdate() {
        transform.position = Vector2.MoveTowards(transform.position, playerPosition.position, ballSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Destroy(gameObject);
    }
}
