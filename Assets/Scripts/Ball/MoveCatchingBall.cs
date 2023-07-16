using System.Collections;
using UnityEngine;

/// <summary>
/// The MoveCatchingBall class controls the movement and behavior of the catching ball in the game.
/// It launches the ball towards a random player position and increases its speed based on the number of times caught.
/// </summary>
public class MoveCatchingBall : MonoBehaviour {

    /// <summary>
    /// The static instance of the MoveCatchingBall, ensuring only one instance exists in the game.
    /// </summary>
    public static MoveCatchingBall instance;

    /// <summary>
    /// The speed at which the catching ball moves towards the player position.
    /// </summary>
    [SerializeField] private float ballSpeed = 6f;

    private int timesCaught = 0;

    //private Vector2 playerPosition;
    private bool isBallLaunched = false;

    private Vector2[] positionsArray = new Vector2[3];
    private Vector2 randomPlayerPosition;

    private void Awake() {
        instance = this;
        positionsArray[0] = new Vector2(-5f, 2.5f);
        positionsArray[1] = new Vector2(0, 2.5f);
        positionsArray[2] = new Vector2(5f, 2.5f);
    }

    private void Start() {
        int randomPos = Random.Range(0, positionsArray.Length);
        randomPlayerPosition = positionsArray[randomPos];
        setTimeCaught(LevelManager.s_instance.getTimesCaught());
        ballSpeed += timesCaught;
        Debug.Log("Times caught: " + timesCaught);
    }

    private void FixedUpdate() {
        setBallSpeedAndLaunch();

        if (Vector2.Distance(transform.position, randomPlayerPosition) < 0.1f) {
            isBallLaunched = false;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        isBallLaunched = false;
        Destroy(gameObject);
    }

    /// <summary>
    /// Returns the current launch status of the ball.
    /// </summary>
    /// <returns>True if the ball is launched, False otherwise.</returns>
    public bool getIsBallLaunched() {
        return isBallLaunched;
    }

    /// <summary>
    /// Sets the launch status of the ball.
    /// </summary>
    /// <param name="t_isBallLaunched">The new launch status to set.</param>
    public void setIsBallLaunched(bool t_isBallLaunched) {
        isBallLaunched = t_isBallLaunched;
    }

    /// <summary>
    /// Sets the number of times the player has been caught in the ball.
    /// </summary>
    /// <param name="t_timeCaught">The number of times caught.</param>
    private void setTimeCaught(int t_timeCaught) {
        if (t_timeCaught == 0) {
            return;
        }
        timesCaught = t_timeCaught;
    }

    /// <summary>
    /// Moves the ball towards the random player position at a specified speed and enables its launch status.
    /// </summary>
    private void setBallSpeedAndLaunch() {
        //setTimeCaught(LevelManager.s_instance.getTimesCaught());
        StartCoroutine(lastChanceToDodgeBall());
        transform.position = Vector2.MoveTowards(transform.position, randomPlayerPosition, ballSpeed * Time.deltaTime);
        Debug.Log("ball speed: " + ballSpeed);
    }

    /// <summary>
    /// Waits for a short time before setting the launch status to true, giving the player a last chance to dodge the ball.
    /// </summary>
    /// <returns>An enumerator to wait for a specified duration.</returns>
    private IEnumerator lastChanceToDodgeBall() {
        yield return new WaitForSeconds(.4f);
        isBallLaunched = true;
    }
}