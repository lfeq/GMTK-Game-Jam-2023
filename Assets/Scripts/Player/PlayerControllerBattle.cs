using UnityEngine;

/// <summary>
/// The PlayerControllerBattle class handles the player's movement and interactions during battle scenes.
/// It allows the player to move horizontally to dodge enemy attacks.
/// The class keeps track of the player's position, whether the player is in a catching ball or not, and the number of times caught.
/// </summary>
public class PlayerControllerBattle : MonoBehaviour {
    public static PlayerControllerBattle instance;
    public bool playerInPosition = false;

    [HideInInspector]
    public bool isInBall;

    [HideInInspector]
    public SpriteRenderer spriteRenderer;

    [SerializeField] private float speed = 5f;
    [SerializeField] private GameObject catchingBall;

    private Rigidbody2D rgbd;
    private Animator animator;
    private bool axisPressed;
    private float xMove;
    private int timesCaught = 0;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        rgbd = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        timesCaught = LevelManager.s_instance.getTimesCaught();
        transform.position = new Vector3(0, 2.5f, 0);
    }

    private void FixedUpdate() {
        movePlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        spriteRenderer.enabled = false;
        Instantiate(catchingBall, transform.position, Quaternion.identity);
        timesCaught++;
        LevelManager.s_instance.setTimesCaught(timesCaught);
        EnemyControllerBattle.instance.canShoot = false;
    }

    /// <summary>
    /// Handles the player's movement during the battle scene.
    /// The player can move horizontally to dodge enemy attacks.
    /// </summary>
    private void movePlayer() {
        if (PlayerManager.instance.getPlayerState() == PlayerState.DeadState) {
            return;
        }

        if (!isInBall) {
            xMove = Input.GetAxisRaw("Horizontal") * speed;
        }

        if (xMove == 0) {
            axisPressed = false;
        }

        if (xMove < 0 && transform.position.x > -speed) {
            if (MoveCatchingBall.instance == null) {
                if (axisPressed == false) {
                    axisPressed = true;
                    //MoveCatchingBall.instance.setIsBallLaunched(false);
                    transform.position = new Vector2(transform.position.x + xMove, transform.position.y);
                }
            } else {
                if (!MoveCatchingBall.instance.getIsBallLaunched()) {
                    if (axisPressed == false) {
                        axisPressed = true;
                        //MoveCatchingBall.instance.setIsBallLaunched(false);
                        transform.position = new Vector2(transform.position.x + xMove, transform.position.y);
                    }
                }
            }
        }

        if (xMove > 0 && transform.position.x < speed) {
            if (MoveCatchingBall.instance == null) {
                if (axisPressed == false) {
                    axisPressed = true;
                    //MoveCatchingBall.instance.setIsBallLaunched(false);
                    transform.position = new Vector2(transform.position.x + xMove, transform.position.y);
                }
            } else {
                if (!MoveCatchingBall.instance.getIsBallLaunched()) {
                    if (axisPressed == false) {
                        axisPressed = true;
                        //MoveCatchingBall.instance.setIsBallLaunched(false);
                        transform.position = new Vector2(transform.position.x + xMove, transform.position.y);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Determines if the player can escape from the catching ball.
    /// This method is not currently implemented and always returns false.
    /// </summary>
    /// <returns>Returns false as the player cannot escape from the catching ball.</returns>
    private bool canEscapeFromBall() {
        return false;
    }
}