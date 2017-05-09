using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScene : UIScreen {

    private void Update()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnStartGame()
    {
        UIManager.instance.Show<GameScreen>();
        SceneManager.LoadScene("GameScene", LoadSceneMode.Additive);
    }

    public void OnQuitGame()
    {
        Application.Quit();
    }
}
