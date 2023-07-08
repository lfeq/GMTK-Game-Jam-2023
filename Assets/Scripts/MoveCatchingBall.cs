using UnityEngine;

public class MoveCatchingBall : MonoBehaviour
{
    [SerializeField] float ballSpeed = 2f;
    Vector2 playerPosition;
    
    private void Awake() {
        playerPosition = PlayerControllerBattle.instance.transform.position;
        Debug.Log("Player pos start: " + playerPosition);
    }

    private void FixedUpdate() {
        transform.position = Vector2.MoveTowards(transform.position, playerPosition, ballSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, playerPosition) < 0.1f) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Destroy(gameObject);
    }
}
