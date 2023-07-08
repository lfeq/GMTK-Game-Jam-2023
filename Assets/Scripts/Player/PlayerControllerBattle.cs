using UnityEngine;

public class PlayerControllerBattle : MonoBehaviour {
    public static PlayerControllerBattle instance;

    [SerializeField] float speed = 1;
    Rigidbody2D rgbd;
    Animator animator;
    bool axisPressed;
    public bool playerInPosition = false;
    public bool canMove;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        rgbd = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
}

