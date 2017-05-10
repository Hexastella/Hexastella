using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyParticleAttack : MonoBehaviour {




	void Awake() {






	}




	
	// Update is called once per frame
	void Update () {

	
	}
		
	// Particle Collider System
	// Make sure that the particle system in unity is set to world and that send collision message is clicked otherwise this script wont work.
	public void ParticleCollision(GameObject other) {
		Rigidbody body = other.GetComponent<Rigidbody>();

		if (body) {
			Vector3 direction = other.transform.position - transform.position;
			direction = direction.normalized;


		


		}



	}










}


