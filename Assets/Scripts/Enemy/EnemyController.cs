using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    [SerializeField] private float movementSpeed;
    [SerializeField] private Color slowedColor = Color.green;
    [SerializeField] private Sprite backViewSprite;

    private Transform player;
    private Animator animator;
    private float m_movementSpeed;
    private bool isSlowed = false;
    private SpriteRenderer spriteRenderer;

    private void Start() {
        player = PlayerManager.instance.transform;
        animator = GetComponent<Animator>();
        m_movementSpeed = movementSpeed;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        move();
        lookAtPlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Projectile")) {
            Slow();
        }
        if (collision.CompareTag("Player")) {
            LevelManager.s_instance.changeEnemySprite(backViewSprite);
            LevelManager.s_instance.changeLevelState(LevelState.Dodging);
        }
    }

    private void lookAtPlayer() {
        Vector2 distance = player.position - transform.position;
        distance = distance.normalized;
        animator.SetFloat("xMovement", distance.x);
        animator.SetFloat("yMovement", distance.y);
    }

    private void move() {
        float step = m_movementSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, player.position, step);
    }

    private void Slow() {
        if (isSlowed) {
            return;
        }
        m_movementSpeed *= movementSpeed / 2;
        isSlowed = true;
        spriteRenderer.color = slowedColor;
        StartCoroutine(StopSlow());
    }

    private IEnumerator StopSlow() {
        yield return new WaitForSeconds(4);
        isSlowed = false;
        m_movementSpeed = movementSpeed;
        spriteRenderer.color = Color.white;
    }
}