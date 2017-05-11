using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyParticleAttack : EnemyAIController {


	// Reference to the player 
	public GameObject mainPlayer;

	EnemyAIController enemyAIController;
		
	// Particle Collider System
	// Make sure that the particle system in unity is set to world and that send collision message is clicked otherwise this script wont work.
	   public void ParticleCollision(GameObject mainPlayer) {
		Rigidbody body = mainPlayer.GetComponent<Rigidbody>();

		    if (body) {
			Vector3 direction = mainPlayer.transform.position - transform.position;
		    direction = direction.normalized;

//			enemyAIController.gunParticles.Play ();
			mainPlayerHealth.TakeDamage (DamageToGive);

			print("Particle Attack Used");
			//enemyAIController.gunParticles.Stop ();



	 	}

	

	}
		

}


