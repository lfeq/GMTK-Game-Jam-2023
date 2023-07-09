using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class ManagerCollectorFrut : MonoBehaviour {

    public static ManagerCollectorFrut instance;


    [SerializeField] public int fruts;
    [SerializeField] private GameObject canvasWin;
    [SerializeField] private Image fruta_1;
    [SerializeField] private Image fruta_2;
    [SerializeField] private Image fruta_3;
    [SerializeField] private Image fruta_4;
    [SerializeField] private Image fruta_5;

    private void Awake() {
        instance = this;
    }
    void Start() {
        fruts = 0;
    }

    // Update is called once per frame
    void Update() {
        WinCondition();
    }

    private void WinCondition() {
        if (fruts >= 5) {
            canvasWin.SetActive(true);
        }
    }

    public void  getFruits(int index) {
        switch (index) {
            case 0:
                fruta_1.color = new Color32(255,255,255,255);
                break;
            case 1:
                fruta_2.color = new Color32(255, 255, 255, 255);
                break;
            case 2:
                fruta_3.color = new Color32(255, 255, 255, 255);
                break;
            case 3:
                fruta_4.color = new Color32(255, 255, 255, 255);
                break;
            case 4:
                fruta_5.color = new Color32(255, 255, 255, 255);
                break;
            default: break;

        }
        
    }
}

