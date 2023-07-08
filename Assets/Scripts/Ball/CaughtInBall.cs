using UnityEngine;
using UnityEngine.UI;

public class CaughtInBall : MonoBehaviour
{
    #region Public
    public Image canvasSlider;
    #endregion

    #region Private
    float maxFillAmount = 1;
    float currentAmount;
    float gettingCaught = 0.1f;
    float turningSpeed = 25f;
    bool hasEscaped;
    bool rotateCounterClockwise = true;
    bool hasLost;
    #endregion

    private void Start() {
        currentAmount = 0.6f;
        canvasSlider.fillAmount = currentAmount;
        PlayerControllerBattle.instance.isInBall = true;
    }

    private void Update() {
        if(canvasSlider.fillAmount >= maxFillAmount) {
            hasEscaped = true;
            PlayerControllerBattle.instance.isInBall = false;
            PlayerControllerBattle.instance.spriteRenderer.enabled = true;
            Destroy(gameObject);
        }

        if(!hasEscaped && !hasLost) {
            canvasSlider.fillAmount -= gettingCaught * Time.deltaTime;
            shakeBall();
        }

        if(canvasSlider.fillAmount <= 0f) {
            hasLost = true;
            Debug.Log("You lose");
        }
        else {
            if(Input.GetMouseButtonDown(0)) {
                canvasSlider.fillAmount += 0.03f;
            }
        }
    }

    void shakeBall() {
        float rotationLimit = 13f;
        if (rotateCounterClockwise) {
            transform.Rotate(Vector3.forward, turningSpeed * Time.deltaTime);
            if (transform.rotation.eulerAngles.z > rotationLimit) {
                rotateCounterClockwise = !rotateCounterClockwise;
            }
        } 
        else {
            transform.Rotate(Vector3.forward, -turningSpeed* Time.deltaTime);
            if(transform.rotation.eulerAngles.z > rotationLimit) {
                rotateCounterClockwise = !rotateCounterClockwise;
            }
        }  
    }
}
