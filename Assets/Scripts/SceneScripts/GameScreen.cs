using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScreen : UIScreen
{

    void Update()
    {
        //Lock the cursor
        Cursor.lockState = CursorLockMode.Locked;

        if (GameManager.instance.playerWin)
        {
            UIManager.instance.Show<WinScreen>();
        } 
        
        if (GameManager.instance.playerLose)
        {
            UIManager.instance.Show<LoseScreen>();
        }
    }
}