using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour {


	// The team gets thier colors by using the team as a index in the array
	public Color[] teamColors; 

	// This is the singleton desogn pattern
	// A singleton is a script with a static variable that refers to an instance of that script in the scene.
	// We have to make sure there is only one instance of yhis type in our scene. 

	public static TeamManager instance 

	{

		get  

		{
			// Only the first time anyone gets  instance we search for it and then after that the _instance is no longer null and we simply return
			if (_instance == null) 
			
			{

				_instance = FindObjectOfType<TeamManager>(); 
			}


			return _instance;

			}

	}


	private static TeamManager _instance; 

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
