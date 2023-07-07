using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public static PlayerController instance;

    [SerializeField] float Speed;
   
    Rigidbody2D rb2d;
    Animator animator;

    bool isFacingRight;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate() {
        PlayerMoment();
    }
    private void PlayerMoment() {
        float xMove, yMove;
        xMove = Input.GetAxisRaw("Horizontal");
        yMove = Input.GetAxisRaw("Vertical");
        
        if (PlayerManager.instance.getPlayerState() == PlayerState.DeadState) {
            return;
        }
        Vector2 direction = new Vector2(xMove , yMove).normalized;
        rb2d.velocity = direction * Speed;
        if ((xMove > 0 && isFacingRight) || (xMove < 0 && !isFacingRight)) {
            flip();
        }
        if (xMove != 0f) {
            PlayerManager.instance.ChangePlayerStates(PlayerState.RunState);
        } else if (xMove == 0) {
            PlayerManager.instance.ChangePlayerStates(PlayerState.IdleState);
        }
    }
    void flip() {
        transform.Rotate(0, 180, 0);
        isFacingRight = !isFacingRight;
    }
}