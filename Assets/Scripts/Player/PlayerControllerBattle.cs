using UnityEngine;


public class PlayerControllerBattle : MonoBehaviour {
    public static PlayerControllerBattle instance;
    public bool playerInPosition = false;

    [SerializeField] float speed = 1;
    [SerializeField] GameObject catchingBall;

    Rigidbody2D rgbd;
    Animator animator;
    public SpriteRenderer spriteRenderer;
    bool axisPressed;
   

    private void Awake() {
        instance = this;
    }

    private void Start() {
        rgbd = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate() {
        movePlayer();
    }

    void movePlayer() {
  
        float xMove;
        
        if(PlayerManager.instance.getPlayerState() == PlayerState.DeadState) {
            return;
        }

        xMove = Input.GetAxisRaw("Horizontal") * speed;

        if(xMove == 0) {
            axisPressed = false;
        }

        if(xMove < 0 && transform.position.x > -speed) {
            if(axisPressed == false) {
                axisPressed = true;
                transform.position =  new Vector2 (transform.position.x + xMove, transform.position.y);
            }
        }

        if(xMove > 0 && transform.position.x < speed) {
            if(axisPressed == false) {
                axisPressed = true;
                transform.position = new Vector2 (transform.position.x + xMove, transform.position.y);
            }
        }
        Debug.Log("horizontal value: " + xMove);
        Debug.Log("position: " + transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        spriteRenderer.enabled = false;
        Instantiate(catchingBall, transform.position, Quaternion.identity);
        EnemyControllerBattle.instance.canShoot = false;
    }

    bool canEscapeFromBall() {
        return false;
    }
}

