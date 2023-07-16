using System.Collections;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// The EnemyController class controls the behavior of the enemy character in the game.
/// It handles movement, chasing, patrolling, and collision interactions with the player and projectiles.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class EnemyController : MonoBehaviour {
    [SerializeField] private float movementSpeed;
    [SerializeField] private Color slowedColor = Color.green;
    [SerializeField] private Sprite backViewSprite;
    [SerializeField] private EnemyState enemyState;
    [SerializeField] private Transform[] movementPoints;
    [SerializeField] private float minimalDistance;
    [SerializeField] private AudioClip hitProjectileAudioClip;
    [SerializeField] private AudioClip hitPlayerAudioClip;

    private Transform player;
    private Animator animator;
    private float m_movementSpeed;
    private bool isSlowed = false;
    private SpriteRenderer spriteRenderer;
    private NavMeshAgent navMeshAgent;
    private int randomNumber;
    private bool stopAttack;
    private AudioSource audioSource;
    private int playerLayerMask;
    private int enemyLayerMask;

    private void Awake() {
        if (navMeshAgent == null) {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
        playerLayerMask = LayerMask.NameToLayer("Player");
        enemyLayerMask = LayerMask.NameToLayer("Enemy");
    }

    private void Start() {
        player = PlayerManager.instance.transform;
        animator = GetComponent<Animator>();
        m_movementSpeed = movementSpeed;
        spriteRenderer = GetComponent<SpriteRenderer>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        navMeshAgent.speed = m_movementSpeed;
        navMeshAgent.enabled = true;
        audioSource = GetComponent<AudioSource>();
        if (movementPoints.Length != 0) {
            randomNumber = Random.Range(0, movementPoints.Length);
        }
        transform.rotation = Quaternion.identity;
    }

    private void Update() {
        if (LevelManager.s_instance.getLevelState() == LevelState.LoadingScene) {
            navMeshAgent.enabled = false;
            return;
        }
        if (stopAttack) {
            return;
        }
        switch (enemyState) {
            case EnemyState.patroling:
                Patrol();
                break;
            case EnemyState.chasing:
                ChasePlayer();
                LookAtTarget(player);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Projectile")) {
            audioSource.clip = hitProjectileAudioClip;
            audioSource.Play();
            Slow();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (stopAttack) {
            return;
        }
        if (collision.gameObject.CompareTag("Player")) {
            LevelManager.s_instance.ChangeEnemySprite(backViewSprite);
            TransitionManager.instance.StartTransition();
            LevelManager.s_instance.ChangeLevelState(LevelState.LoadingScene);
            audioSource.clip = hitPlayerAudioClip;
            audioSource.Play();
        }
    }

    /// <summary>
    /// Changes the enemy state to the provided newState.
    /// </summary>
    /// <param name="newState">The new state to set.</param>
    public void ChanceState(EnemyState newState) {
        enemyState = newState;
    }

    /// <summary>
    /// Stops the enemy's attack and applies slowing effect, followed by activating the attack after a short delay.
    /// </summary>
    public void StopAttack() {
        if (navMeshAgent == null) {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
        Physics2D.IgnoreLayerCollision(playerLayerMask, enemyLayerMask);
        stopAttack = true;
        navMeshAgent.speed = 0;
        Slow();
        StartCoroutine(AtivateAttack());
    }

    /// <summary>
    /// Makes the enemy look at the provided target position.
    /// </summary>
    /// <param name="target">The target to look at.</param>
    private void LookAtTarget(Transform target) {
        Vector2 distance = target.position - transform.position;
        distance = distance.normalized;
        animator.SetFloat("xMovement", distance.x);
        animator.SetFloat("yMovement", distance.y);
    }

    /// <summary>
    /// Moves the enemy towards the player's position.
    /// </summary>
    private void ChasePlayer() {
        navMeshAgent.SetDestination(player.position);
    }

    /// <summary>
    /// Slows down the enemy, changing its movement speed and color temporarily.
    /// </summary>
    private void Slow() {
        if (navMeshAgent == null) {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
        if (spriteRenderer == null) {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        if (isSlowed) {
            return;
        }
        m_movementSpeed = movementSpeed / 2;
        navMeshAgent.speed = m_movementSpeed;
        isSlowed = true;
        spriteRenderer.color = slowedColor;
        StartCoroutine(StopSlow());
    }

    /// <summary>
    /// Moves the enemy along the patrol points, changing the target point when the minimal distance is reached.
    /// </summary>
    private void Patrol() {
        navMeshAgent.SetDestination(movementPoints[randomNumber].position);
        LookAtTarget(movementPoints[randomNumber]);
        if (Vector2.Distance(transform.position, movementPoints[randomNumber].position) < minimalDistance) {
            randomNumber = Random.Range(0, movementPoints.Length);
        }
    }

    /// <summary>
    /// Stops the slowing effect after a short delay, restoring the enemy's original speed and color.
    /// </summary>
    private IEnumerator StopSlow() {
        yield return new WaitForSeconds(4);
        isSlowed = false;
        m_movementSpeed = movementSpeed;
        navMeshAgent.speed = m_movementSpeed;
        spriteRenderer.color = Color.white;
    }

    /// <summary>
    /// Activates the enemy's attack after a short delay, enabling collision with the player again.
    /// </summary>
    private IEnumerator AtivateAttack() {
        yield return new WaitForSeconds(3);
        stopAttack = false;
        Physics2D.IgnoreLayerCollision(playerLayerMask, enemyLayerMask, true);
    }
}

/// <summary>
/// Enumeration representing the different states of the enemy (chasing or patrolling).
/// </summary>
public enum EnemyState {
    chasing,
    patroling
}