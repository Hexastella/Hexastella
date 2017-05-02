using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outpost : MonoBehaviour {

	// Use this for initialization

	public int team; 


	public float captureValue;

	private float captureRate = 0.02f; 

	private SkinnedMeshRenderer flag; 


	void Start() 

	{
		flag = GetComponentInChildren<SkinnedMeshRenderer> ();


	}


	void Update() 



	{

		Color teamColor = TeamManager.instance.teamColors [team];

		// We use lerp to show the color between white and the team color when the flag is being captured. 

		flag.material.color = Color.Lerp (Color.white, teamColor, captureValue); 


	}


	void OnTriggerStay (Collider otherCollider)

	{

		Unit unit = otherCollider.GetComponent<Unit> (); 

		if (unit != null) {

			if (unit.team == team) {

				captureValue += captureRate; 


				// If capture value 
				if (captureValue >= 1) {
					captureValue = 1; 


				}
		

			} else {

				captureValue -= captureRate; 
				if (captureValue < 0) {
					team = unit.team; 
					captureValue = 0;
				}
				

			}
		}	

	}


}
