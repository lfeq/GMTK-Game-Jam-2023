using UnityEngine;

public class PlayerControllerBattle : MonoBehaviour
{
    [SerializeField] float speed = 1;
    Rigidbody2D rgbd;
    Animator animator;

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

        if(Input.GetAxisRaw("Horizontal") != 0) {
            xMove = Input.GetAxisRaw("Horizontal") * speed;

            Vector3 movePos = new Vector3(xMove, 0, 0) * Time.deltaTime;

            transform.position += movePos;

            Debug.Log("horizontal value: " + xMove);
            Debug.Log("position: " + transform.position);
        }

    }
}
