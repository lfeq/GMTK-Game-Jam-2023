using UnityEngine;
using UnityEngine.UI;

public class CaughtInBall : MonoBehaviour
{
    public Image canvasSlider;
    float maxFillAmount = 1;
    float currentAmount;
    float gettingCaught = 0.1f;
    bool hasEscaped;

    private void Start() {
        currentAmount = 0.6f;
        canvasSlider.fillAmount = currentAmount;
    }

    private void Update() {

        if(canvasSlider.fillAmount >= maxFillAmount) {
            hasEscaped = true;
            PlayerControllerBattle.instance.spriteRenderer.enabled = true;
            Destroy(gameObject);
        }
        
        if(!hasEscaped) {
            canvasSlider.fillAmount -= gettingCaught * Time.deltaTime;
        }
        
        if(Input.GetMouseButtonDown(0)) {
            canvasSlider.fillAmount += 0.05f;
        }
    }
}
