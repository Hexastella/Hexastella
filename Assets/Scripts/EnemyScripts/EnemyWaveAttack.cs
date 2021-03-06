﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveAttack : EnemyAIController {



	//This is the wave attack method that will be used for the enemy wave attack as one of the enemys AI features.
    // Wave Attack 

	// Define the EnemyAIController Script
	EnemyAIController enemyAIController;


	void Awake() {

		// Get the function of the EnemyAIControlleScript
		enemyAIController = GetComponent <EnemyAIController>();


	}



		public void WaveAttack ()
	{
		//timer = 0f;
		// Do damange to the player via the WaveAttack from the player health script.
		// Play the audio sound of the attack 
		enemyAIController.Audio.Play ();
	

		//waveAttackLine.material = new Material (Shader.Find("Particles/Additive"));
		// Set the new colors here
		enemyAIController.waveAttackLine.SetColors (enemyAIController.waveAttackColor, enemyAIController.waveAttackColor2);


		// Particle effects
		enemyAIController.gunParticles.Stop ();
		enemyAIController.gunParticles.Play ();
        // Enable the Wave Attack Line Renderer
		enemyAIController.waveAttackLine.enabled = true;
		// Set the width of the Wave Attack Line
		enemyAIController.waveAttackLine.SetWidth (50, 50); 
		//  Set the position of thw Wave Attack Line
		enemyAIController.waveAttackLine.SetPosition (0, transform.position);
	     
		enemyAIController.shootRay.origin = transform.position;
		enemyAIController.shootRay.direction = transform.forward;


		if (Physics.Raycast (enemyAIController.shootRay, out enemyAIController.shootHit, enemyAIController.projectileRange, enemyAIController.PlayerDamageZone)) {

			PlayerHealth playerHealth = enemyAIController.shootHit.collider.GetComponent <PlayerHealth> ();

			// If the playerHealth is higher than 0 then damage the player using the particle effect
			if (playerHealth.currentHealth > 0) {
				//  Player will take damage when hit by the LineRenderer. 
				enemyAIController.mainPlayerHealth.TakeDamage (enemyAIController.DamageToGive);

				// Print message for debugging only
				print ("Wave Attack Used");

			}
			enemyAIController.waveAttackLine.SetPosition (1, enemyAIController.shootHit.point);
		

		} else {
			enemyAIController.waveAttackLine.SetPosition (1, enemyAIController.shootRay.origin + enemyAIController.shootRay.direction * enemyAIController.projectileRange);


		}




	}


}


