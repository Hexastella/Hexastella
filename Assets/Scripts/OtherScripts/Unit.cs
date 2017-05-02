using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {


	// We make team an integer so we can use it as a index in arrays
	public int team; 


	protected Rigidbody rb;
	protected Animator anim; 


	// Virtual method can be orveridden but doesnt have to be
	protected virtual void Start () {



		rb = GetComponent<Rigidbody> (); 

		anim = GetComponentInChildren<Animator>(); 

	
		//Color teamcolor = TeamManager.instance.teamColors [team];
		//transform.Find("Teddy/Teddy_Body").GetComponent<Renderer>().material.color = teamcolor;




	}


}