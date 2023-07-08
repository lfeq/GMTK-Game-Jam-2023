using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerCollectorFrut : MonoBehaviour
{
    [SerializeField] public  int fruts;
    [SerializeField] private GameObject canvasWin;

    void Start()
    {
        fruts = 0;
    }

    // Update is called once per frame
    void Update()
    {
        WinCondition();
    }

    private void WinCondition() {
        if ( fruts >= 5 ) {
            canvasWin.SetActive(true);
        }
    }
}
