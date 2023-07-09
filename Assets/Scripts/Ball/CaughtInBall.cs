using UnityEngine;
using UnityEngine.UI;

public class CaughtInBall : MonoBehaviour {

    #region Public

    public Image canvasSlider;
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

    private void setTimeCaught(int t_timeCaught) {
        if (t_timeCaught == 0) {
            return;
        }
        timesCaught = t_timeCaught;
    }

    private void changeSliderSpeed() {
        if (!hasEscaped && !hasLost) {
            canvasSlider.fillAmount -= (gettingCaughtSpeed * Time.deltaTime);
            //Debug.Log("Times caught: " + timesCaught);
            //Debug.Log("getting caught speed: " + gettingCaughtSpeed + (timesCaught * 0.05f));
            shakeBall();
        }
    }

    private void checkIfYouHaveBeenTrapped() {
        if (canvasSlider.fillAmount <= 0f) {
            hasLost = true;
            Debug.Log("You lose");
        } else {
            if (Input.GetMouseButtonDown(0)) {
                canvasSlider.fillAmount += escapingSpeed;
                Debug.Log("Escaping speed 2: " + escapingSpeed);
            }
        }
    }

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