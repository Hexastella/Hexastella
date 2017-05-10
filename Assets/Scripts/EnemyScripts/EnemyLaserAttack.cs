using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserAttack : MonoBehaviour {


	// Define the EnemyAIController Script
	EnemyAIController enemyAIController;


	void Awake () {

		enemyAIController = GetComponent <EnemyAIController>();


	}


	// Laser Attack
	public void LaserAttack ()
	{
		//timer = 0f;
		// Do damange to the player via the WaveAttack from the player health script.
		// Play the audio sound of the attack 
		enemyAIController.Audio.Play ();


		enemyAIController.laserAttackLine.material = new Material (Shader.Find("Particles/Additive"));
		// Set the new colors here
		enemyAIController.laserAttackLine.SetColors (enemyAIController.laserAttackColor,enemyAIController.laserAttackColor2);

		enemyAIController.laserAttackLine.SetWidth (1, 1); 


		// Particle effects
		enemyAIController.gunParticles.Stop ();
		enemyAIController.gunParticles.Play ();

		enemyAIController.laserAttackLine.enabled = true;
		enemyAIController.laserAttackLine.SetPosition (0, transform.position);
		enemyAIController.shootRay.origin = transform.position;
		enemyAIController.shootRay.direction = transform.forward;



		if(Physics.Raycast (enemyAIController.shootRay, out enemyAIController.shootHit, enemyAIController.projectileRange, enemyAIController.PlayerDamageZone))
		{

			PlayerHealth playerHealth = enemyAIController.shootHit.collider.GetComponent <PlayerHealth> ();




			// If the playerHealth is higher than 0 then damage the player using the particle effect
			if(playerHealth.currentHealth > 0)
			{
				//  Player will take damage when hit by the LineRenderer. 
				enemyAIController.mainPlayerHealth.TakeDamage (enemyAIController.DamageToGive);
				// Print message for debugging only

				print("PLAYER GOT HIT");

			}
			enemyAIController.laserAttackLine.SetPosition (1, enemyAIController.shootHit.point);
		}
		else
		{
			enemyAIController.laserAttackLine.SetPosition (1, enemyAIController.shootRay.origin + enemyAIController.shootRay.direction * enemyAIController.projectileRange);
		}
	}





}
