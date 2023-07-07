using UnityEngine;

public class PlayerControllerBattle : MonoBehaviour
{
    [SerializeField] float speed = 1;
    Rigidbody2D rgbd;
    Animator animator;
    bool axisPressed;

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
                transform.position =  new Vector3 (transform.position.x + xMove, 0, 0);
            }
        }

        if(xMove > 0 && transform.position.x < speed) {
            if(axisPressed == false) {
                axisPressed = true;
                transform.position = new Vector3(transform.position.x + xMove, 0, 0);
            }
        }

        Debug.Log("horizontal value: " + xMove);
        Debug.Log("position: " + transform.position);
    }
}
