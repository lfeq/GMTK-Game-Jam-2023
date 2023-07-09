using UnityEngine;
using UnityEngine.UI;

public class ManagerCollectorFrut : MonoBehaviour {
    public static ManagerCollectorFrut instance;

    [SerializeField] private int fruts;
    [SerializeField] private GameObject canvasWin;
    [SerializeField] private Image fruta_1;
    [SerializeField] private Image fruta_2;
    [SerializeField] private Image fruta_3;
    [SerializeField] private Image fruta_4;
    [SerializeField] private Image fruta_5;
    [SerializeField] private GameObject fruit1GO, fruit2GO, fruit3GO, fruit4GO, fruit5GO;

    private void Awake() {
        instance = this;
        fruts = 0;
    }

    private void WinCondition() {
        if (fruts >= 5) {
            canvasWin.SetActive(true);
        }
    }

    public void getFruits(int index) {
        switch (index) {
            case 0:
                fruta_1.color = new Color32(255, 255, 255, 255);
                fruit1GO.SetActive(false);
                break;
            case 1:
                fruta_2.color = new Color32(255, 255, 255, 255);
                fruit2GO.SetActive(false);
                break;
            case 2:
                fruta_3.color = new Color32(255, 255, 255, 255);
                fruit3GO.SetActive(false);
                break;
            case 3:
                fruta_4.color = new Color32(255, 255, 255, 255);
                fruit4GO.SetActive(false);
                break;
            case 4:
                fruta_5.color = new Color32(255, 255, 255, 255);
                fruit5GO.SetActive(false);
                break;
            default: break;
        }
        Debug.Log("Frutitas");
        fruts++;
        WinCondition();
    }
}