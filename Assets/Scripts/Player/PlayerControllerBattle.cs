using System.Collections;
using UnityEngine;

public class PlayerControllerBattle : MonoBehaviour {
    public static PlayerControllerBattle instance;
    public bool playerInPosition = false;
    [HideInInspector]
    public bool isInBall;
    [HideInInspector]
    public SpriteRenderer spriteRenderer;

    [SerializeField] float speed = 5f;
    [SerializeField] GameObject catchingBall;

    Rigidbody2D rgbd;
    Animator animator;
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

    void movePlayer() {
        if(PlayerManager.instance.getPlayerState() == PlayerState.DeadState) {
            return;
        }

        if(!isInBall) {
            xMove = Input.GetAxisRaw("Horizontal") * speed;
        }

        if(xMove == 0) {
            axisPressed = false;
        }

        if(xMove < 0 && transform.position.x > -speed) {
            if(MoveCatchingBall.instance == null) {
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

        if(xMove > 0 && transform.position.x < speed) {
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

    private void OnTriggerEnter2D(Collider2D collision) {
        spriteRenderer.enabled = false;
        Instantiate(catchingBall, transform.position, Quaternion.identity);
        timesCaught++;
        LevelManager.s_instance.setTimesCaught(timesCaught);
        EnemyControllerBattle.instance.canShoot = false;

    }
    bool canEscapeFromBall() {
        return false;
    }
}

