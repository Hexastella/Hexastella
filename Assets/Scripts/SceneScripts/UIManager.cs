using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    //Singleton
    public static UIManager instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<UIManager>();
            }
            return _instance;
        }
    }

    private static UIManager _instance;

    private Dictionary<Type, UIScreen> screens = new Dictionary<Type, UIScreen>();

    private UIScreen currentScreen;

	void Start ()
    {
        //Search for all children
		foreach(UIScreen screen in GetComponentsInChildren<UIScreen>(true))
        {
            screen.gameObject.SetActive(false);
            //Get the types
            screens.Add(screen.GetType(), screen);
        }
        Show(typeof(MainScene));
	}

    public void Show<T>()
    {
        //Call private Show
        Show(typeof(T));
    }

    void Show(Type screenType)
    {
        if(currentScreen != null)
        {
            currentScreen.gameObject.SetActive(false);
        }
        UIScreen newScreen = screens[screenType];
        newScreen.gameObject.SetActive(true);
        currentScreen = newScreen;
    }
}
