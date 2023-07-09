using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayManager : MonoBehaviour
{
    public void MainMenu() {
        GameManager.s_instance.changeGameSate(GameState.LoadMainMenu);
    }

}
