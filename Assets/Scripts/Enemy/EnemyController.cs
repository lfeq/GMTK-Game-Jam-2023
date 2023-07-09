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
    private bool stopAttack;

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
        navMeshAgent.speed = m_movementSpeed;
        if (movementPoints.Length != 0) {
            randomNumber = Random.Range(0, movementPoints.Length);
        }
    }

    private void Update() {
        if (stopAttack) {
            return;
        }
        switch (enemyState) {
            case EnemyState.patroling:
                Patrol();
                break;
            case EnemyState.chasing:
                move();
                lookAtTarget(player);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Projectile")) {
            Slow();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (stopAttack) {
            return;
        }
        if (collision.gameObject.CompareTag("Player")) {
            LevelManager.s_instance.ChangeEnemySprite(backViewSprite);
            LevelManager.s_instance.ChangeLevelState(LevelState.Dodging);
        }
    }

    public void ChanceState(EnemyState newState) {
        enemyState = newState;
    }

    public void StopAttack() {
        stopAttack = true;
        navMeshAgent.speed = 0;
        Slow();
        StartCoroutine(AtivateAttack());
    }

    private void lookAtTarget(Transform target) {
        Vector2 distance = target.position - transform.position;
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
        navMeshAgent.speed = m_movementSpeed;
        isSlowed = true;
        spriteRenderer.color = slowedColor;
        StartCoroutine(StopSlow());
    }

    private void Patrol() {
        //transform.position = Vector2.MoveTowards(transform.position, movementPoints[randomNumber].position, movementSpeed * Time.deltaTime);
        navMeshAgent.SetDestination(movementPoints[randomNumber].position);
        lookAtTarget(movementPoints[randomNumber]);
        if (Vector2.Distance(transform.position, movementPoints[randomNumber].position) < minimalDistance) {
            randomNumber = Random.Range(0, movementPoints.Length);
        }
    }

    private IEnumerator StopSlow() {
        yield return new WaitForSeconds(4);
        isSlowed = false;
        m_movementSpeed = movementSpeed;
        navMeshAgent.speed = m_movementSpeed;
        spriteRenderer.color = Color.white;
    }

    private IEnumerator AtivateAttack() {
        yield return new WaitForSeconds(2);
        stopAttack = false;
    }
}

public enum EnemyState {
    chasing,
    patroling
}