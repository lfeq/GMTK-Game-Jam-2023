using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    [SerializeField] private float movementSpeed;

    private Transform player;
    private Animator animator;

    private void Start() {
        player = PlayerManager.instance.transform;
        animator = GetComponent<Animator>();
    }

    private void Update() {
        move();
        lookAtPlayer();
    }

    private void lookAtPlayer() {
        Vector2 distance = player.position - transform.position;
        distance = distance.normalized;
        animator.SetFloat("xMovement", distance.x);
        animator.SetFloat("yMovement", distance.y);
    }

    private void move() {
        float step = movementSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, player.position, step);
    }
}