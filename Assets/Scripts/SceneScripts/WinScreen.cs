using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : UIScreen{
    
    void OnEnable()
    {
        Time.timeScale = 0;
    }

    void OnDisable()
    {
        Time.timeScale = 1;
    }

    private void Update()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnMenuButton()
    {
        UIManager.instance.Show<MainScene>();
        SceneManager.UnloadSceneAsync("GameScene");
    }
}
