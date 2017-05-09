using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

	protected Rigidbody rb;
	protected Animator anim; 

	protected virtual void Start()
    {
		rb = GetComponent<Rigidbody>();
		anim = GetComponentInChildren<Animator>(); 
	}
}