using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScreen : UIScreen
{

    public static bool playerWin;
    public static bool playerLose;

    void Update()
    {
        //Lock the cursor
        Cursor.lockState = CursorLockMode.Locked;

        if (playerWin)
        {
            UIManager.instance.Show<WinScreen>();
        }else if (playerLose)
        {
            UIManager.instance.Show<LoseScreen>();
        }
    }
}