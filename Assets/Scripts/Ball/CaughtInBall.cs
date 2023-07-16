using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The CaughtInBall class handles the player getting caught in a ball during the game.
/// It controls a slider that represents the escape progress and the associated behaviors.
/// </summary>
public class CaughtInBall : MonoBehaviour {

    #region Public

    /// <summary>
    /// The slider UI element that represents the escape progress of the player.
    /// </summary>
    public Image canvasSlider;

    /// <summary>
    /// The animation curve used to determine the escaping speed based on the number of times caught.
    /// </summary>
    public AnimationCurve curve;

    #endregion Public

    #region Private

    private int timesCaught = 0;
    private float maxFillAmount = 1;
    private float currentAmount;
    private float gettingCaughtSpeed = 0.1f;
    private float turningSpeed = 25f;
    private bool hasEscaped;
    private bool rotateCounterClockwise = true;
    private bool hasLost;
    private float escapingSpeed = 0.03f;

    #endregion Private

    private void Start() {
        currentAmount = 0.6f;
        setTimeCaught(LevelManager.s_instance.getTimesCaught());
        canvasSlider.fillAmount = currentAmount;
        PlayerControllerBattle.instance.isInBall = true;
        escapingSpeed = curve.Evaluate(timesCaught);
        Debug.Log("escaping speed: " + escapingSpeed);
    }

    private void Update() {
        escapeBall();

        changeSliderSpeed();

        checkIfYouHaveBeenTrapped();
    }

    /// <summary>
    /// Shakes the ball by rotating it to simulate being trapped.
    /// </summary>
    private void shakeBall() {
        float rotationLimit = 13f;
        if (rotateCounterClockwise) {
            transform.Rotate(Vector3.forward, turningSpeed * Time.deltaTime);
            if (transform.rotation.eulerAngles.z > rotationLimit) {
                rotateCounterClockwise = !rotateCounterClockwise;
            }
        } else {
            transform.Rotate(Vector3.forward, -turningSpeed * Time.deltaTime);
            if (transform.rotation.eulerAngles.z > rotationLimit) {
                rotateCounterClockwise = !rotateCounterClockwise;
            }
        }
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
    /// Changes the slider fill amount and applies shaking effect while player is not escaped or lost.
    /// </summary>
    private void changeSliderSpeed() {
        if (!hasEscaped && !hasLost) {
            canvasSlider.fillAmount -= (gettingCaughtSpeed * Time.deltaTime);
            //Debug.Log("Times caught: " + timesCaught);
            //Debug.Log("getting caught speed: " + gettingCaughtSpeed + (timesCaught * 0.05f));
            shakeBall();
        }
    }

    /// <summary>
    /// Checks if the player has been fully trapped, resulting in a game over.
    /// Otherwise, allows the player to escape by interacting with the slider.
    /// </summary>
    private void checkIfYouHaveBeenTrapped() {
        if (canvasSlider.fillAmount <= 0f) {
            hasLost = true;
            GameManager.s_instance.changeGameSate(GameState.GameOver);
            //Debug.Log("You lose");
        } else {
            if (Input.GetMouseButtonDown(0)) {
                canvasSlider.fillAmount += escapingSpeed;
                //Debug.Log("Escaping speed 2: " + escapingSpeed);
            }
        }
    }

    /// <summary>
    /// Checks if the player has fully escaped, triggering level state changes and destroying the ball object.
    /// </summary>
    private void escapeBall() {
        if (canvasSlider.fillAmount >= maxFillAmount) {
            hasEscaped = true;
            PlayerControllerBattle.instance.isInBall = false;
            PlayerControllerBattle.instance.spriteRenderer.enabled = true;
            LevelManager.s_instance.ChangeLevelState(LevelState.LoadingScene);
            TransitionManager.instance.StartTransition();
            Destroy(gameObject);
        }
    }
}