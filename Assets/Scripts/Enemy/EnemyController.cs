using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {
    [SerializeField] private float movementSpeed;
    [SerializeField] private Color slowedColor = Color.green;
    [SerializeField] private Sprite backViewSprite;
    [SerializeField] private EnemyState enemyState;
    [SerializeField] private Transform[] movementPoints;
    [SerializeField] private float minimalDistance;

    private Transform player;
    private Animator animator;
    private float m_movementSpeed;
    private bool isSlowed = false;
    private SpriteRenderer spriteRenderer;
    private NavMeshAgent navMeshAgent;
    private int randomNumber;

    private void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start() {
        player = PlayerManager.instance.transform;
        animator = GetComponent<Animator>();
        m_movementSpeed = movementSpeed;
        spriteRenderer = GetComponent<SpriteRenderer>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        if (movementPoints.Length != 0) {
            randomNumber = Random.Range(0, movementPoints.Length);
        }
    }

    private void Update() {
        switch (enemyState) {
            case EnemyState.patroling:
                Patrol();
                break;
            case EnemyState.chasing:
                move();
                lookAtPlayer();
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Projectile")) {
            Slow();
        }
        //if (collision.CompareTag("Player")) {
        //    LevelManager.s_instance.ChangeEnemySprite(backViewSprite);
        //    LevelManager.s_instance.ChangeLevelState(LevelState.Dodging);
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            LevelManager.s_instance.ChangeEnemySprite(backViewSprite);
            LevelManager.s_instance.ChangeLevelState(LevelState.Dodging);
        }
    }

    private void lookAtPlayer() {
        Vector2 distance = player.position - transform.position;
        distance = distance.normalized;
        animator.SetFloat("xMovement", distance.x);
        animator.SetFloat("yMovement", distance.y);
    }

    private void move() {
        navMeshAgent.SetDestination(player.position);
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

    private void Patrol() {
        //transform.position = Vector2.MoveTowards(transform.position, movementPoints[randomNumber].position, movementSpeed * Time.deltaTime);
        navMeshAgent.SetDestination(movementPoints[randomNumber].position);
        if (Vector2.Distance(transform.position, movementPoints[randomNumber].position) < minimalDistance) {
            randomNumber = Random.Range(0, movementPoints.Length);
        }
    }

    private IEnumerator StopSlow() {
        yield return new WaitForSeconds(4);
        isSlowed = false;
        m_movementSpeed = movementSpeed;
        spriteRenderer.color = Color.white;
    }

    public void ChanceState(EnemyState newState) {
        enemyState = newState;
    }
}

public enum EnemyState {
    chasing,
    patroling
}