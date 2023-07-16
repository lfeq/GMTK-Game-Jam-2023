using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The ManagerCollectorFrut class manages the collection of fruits in the game.
/// It keeps track of the number of fruits collected and handles the win condition.
/// </summary>
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

    /// <summary>
    /// getFruits method is called when a fruit is collected.
    /// It updates the corresponding fruit image and game object state,
    /// increments the fruit count, and checks if the win condition is met.
    /// </summary>
    /// <param name="index">The index of the fruit to be collected.</param>
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
        //Debug.Log("Frutitas");
        fruts++;
        WinCondition();
    }

    /// <summary>
    /// WinCondition checks if the required number of fruits have been collected.
    /// If so, it triggers the win state in the game.
    /// </summary>
    private void WinCondition() {
        if (fruts >= 5) {
            //canvasWin.SetActive(true);
            GameManager.s_instance.changeGameSate(GameState.Win);
        }
    }
}