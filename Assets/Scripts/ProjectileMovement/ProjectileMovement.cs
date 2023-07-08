using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileMovement : MonoBehaviour {
    [SerializeField] private float moveSpeed = 4f;

    private Vector2 moveDirection;
    private Rigidbody2D rb2d;

    private void Start() {
        float xMove = Input.GetAxisRaw("Horizontal");
        float yMove = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(xMove, yMove);
        float angle = Mathf.Atan2(yMove, xMove) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        rb2d = GetComponent<Rigidbody2D>();
        if (moveDirection == Vector2.zero) {
            Destroy(gameObject);
        }
        Destroy(gameObject, 10);
    }

    private void FixedUpdate() {
        rb2d.velocity = moveDirection * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        //Logica de colision con enemigo
    }
}