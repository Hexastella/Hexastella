using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScene : MonoBehaviour {

	public void OnStartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnQuitGame()
    {
        Application.Quit();
    }
}
