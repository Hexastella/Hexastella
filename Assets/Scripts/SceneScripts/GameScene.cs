using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScene : MonoBehaviour {

    public static bool gameOver;

	void Update () {
        //Lock the cursor
        Cursor.lockState = CursorLockMode.Locked;

        if (gameOver)
        {
            SceneManager.LoadScene("WinOrLoseScreen");
        }
    }
}