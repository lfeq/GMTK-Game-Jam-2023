using UnityEngine;

/// <summary>
/// The PlayerController class manages the player's movement and animation.
/// It provides methods to handle player input and movement, as well as animations and flipping the player sprite.
/// The class also keeps track of the player's states and interactions with the game world.
/// </summary>
public class PlayerController : MonoBehaviour {
    public static PlayerController instance;

    [SerializeField] private float Speed;

    private Rigidbody2D rb2d;
    private Animator animator;

    private bool isFacingRight;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        if (LevelManager.s_instance.getLevelState() == LevelState.LoadingScene) {
            return;
        }
        PlayerMoment();
    }

    /// <summary>
    /// Handles the player's movement and animation based on user input.
    /// </summary>
    private void PlayerMoment() {
        float xMove, yMove;
        xMove = Input.GetAxisRaw("Horizontal");
        yMove = Input.GetAxisRaw("Vertical");

        if (PlayerManager.instance.getPlayerState() == PlayerState.DeadState) {
            return;
        }
        Vector2 direction = new Vector2(xMove, yMove).normalized;
        rb2d.velocity = direction * Speed;
        if ((xMove > 0 && isFacingRight) || (xMove < 0 && !isFacingRight)) {
            flip();
        }
        if (xMove != 0f) {
            PlayerManager.instance.ChangePlayerStates(PlayerState.RunState);
        } else if (xMove == 0) {
            PlayerManager.instance.ChangePlayerStates(PlayerState.IdleState);
        }
        if (yMove >= 0.1f) {
            PlayerManager.instance.ChangePlayerStates(PlayerState.RunBack);
        }
        if (yMove <= -0.1f) {
            PlayerManager.instance.ChangePlayerStates(PlayerState.RunFront);
        }
    }

    /// <summary>
    /// Flips the player's sprite horizontally to face the movement direction.
    /// </summary>
    private void flip() {
        transform.Rotate(0, 180, 0);
        isFacingRight = !isFacingRight;
    }
}