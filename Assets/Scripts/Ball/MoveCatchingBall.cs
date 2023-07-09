using UnityEngine;

public class MoveCatchingBall : MonoBehaviour
{
    [SerializeField] private float ballSpeed = 3f;
    private int timesCaught = 0;
    private Vector2 playerPosition;

    private void Start() {
        playerPosition = PlayerControllerBattle.instance.transform.position;
        setTimeCaught(LevelManager.s_instance.getTimesCaught());
    }

    private void FixedUpdate() {
        
        setBallSpeedAndLaunch();

        if (Vector2.Distance(transform.position, playerPosition) < 0.1f) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Destroy(gameObject);
    }

    void setTimeCaught(int t_timeCaught) {
        if(t_timeCaught == 0) {
            return;
        }
        timesCaught = t_timeCaught;
    }

    void setBallSpeedAndLaunch() {
        transform.position = Vector2.MoveTowards(transform.position, playerPosition, (ballSpeed + timesCaught) * Time.deltaTime);
    }
}
