	//  EnemyAttack.CS
	//  Copyright © 2017 Jaxon Stevens. All rights reserved.
	// Created by Jaxon Stevens and Hillary Ocando

	// Created in part of our VFS game final project.

	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine.SceneManagement;

	public class EnemyAttack : MonoBehaviour
	{
		public float timeBetweenAttacks = 0.0f;
		//public int attackDamage = 10;


		internal Vector3 lineStartPos; 
		internal Vector3 lineEndPos; 

		// From EnemyShooting Script NEW

		public int DamageToGive = 10;
		public float timeBetweenBullets = 0.15f;
		public float projectileRange = 200f;


		private float shootTime = 1.0f;
		public float timePeriod = 1.0f; 

		float timer;
		AudioSource Audio;

		// Attack Damage 
		PlayerHealth mainPlayerHealth;
		public int attackDamage = 10;
		EnemyAttack enemyAttack;

		// LineRenderer which is used for the wave attack 
		LineRenderer gunLine;
		ParticleSystem gunParticles;

		Ray shootRay = new Ray(); 

		// Create raycast shootHit
		RaycastHit shootHit;

		// Define the playerDamageZone
		int PlayerDamageZone;
		// Used for the particle system when the enemy shoots
		ParticleSystem enemyParticles; 
		LineRenderer Line;
		// The amount of time the effects display
		float effectsDisplayTime = 0.2f;


		// From EnemyShooting Script NEW 

		Animator anim;
		GameObject player;
		EnemyHealth enemyHealth;
		bool playerInRange;
		// float timer;



		void Awake ()
		{
			player = GameObject.FindGameObjectWithTag ("Player");

			// Make sure that the layer of the main player in unity is set to the defined value below otherwise the player will not recieve damage
			PlayerDamageZone = LayerMask.GetMask ("Shootable");

			gunParticles = GetComponent<ParticleSystem> ();
			gunLine = GetComponent <LineRenderer> ();
			Audio = GetComponent<AudioSource> ();

			mainPlayerHealth = player.GetComponent <PlayerHealth> ();
			enemyHealth = GetComponent<EnemyHealth>();
			anim = GetComponent <Animator> ();
		}



		// When the player enters a trigger
		void OnTriggerEnter (Collider other)
		{
			if(other.gameObject == player)
			{
				playerInRange = true;
			}
		}



		// When the player exits the trigger
		void OnTriggerExit (Collider other)
		{
			if(other.gameObject == player)
			{
				playerInRange = false;
			}
		}



		// This is the 
		/* void Update () {
			shootTime += Time.deltaTime;
			if (shootTime >= timePeriod) {
				shootTime = shootTime - timePeriod;
				//OnCollide (); 
				// Call the Wave Attack Functio every set amount of time predefined
				WaveAttack (); 
				//  Audio.Play ();
				// Print a message for debugging only
				print ("ShotOnce");
			} 
		} /*/



		// Attack player function 
		void Attack ()
		{
			timer = 0f;

			if(mainPlayerHealth.currentHealth > 0)
			{
				mainPlayerHealth.TakeDamage (attackDamage );
			}
		}



		// Kill Player function
		void killPlayer() {

			if(mainPlayerHealth.currentHealth > 0)
			{
				mainPlayerHealth.TakeDamage (attackDamage);
			}

		}


		// COROUTUNES USED FOR ENEMY AI (START)
		private enum State
		{

			Walk,
			WaveAttack,
			Idle,
			LaserAttack

		}

		// The initial start state
		void Start()
		{
			// Set Initial State here
			SetState (State.Walk);

		}




		private State currentState;


		// Create ans set states for AI 
		private void SetState (State newState)
		{

			StopAllCoroutines ();
			currentState = newState;


			switch (currentState) {


			case State.Walk:
				StartCoroutine (OnWalk ());
				break;

			case State.WaveAttack:
				StartCoroutine (OnWaveAttack ());
				break;


			case State.Idle:
				StartCoroutine (OnIdle ());
				break; 


			case State.LaserAttack:
				StartCoroutine (OnLaserAttack());
				break;


			}



		}

		// STATE ONE
		// Initial State where the AI will walk towards the player 
		IEnumerator OnWalk ()
		{

			print("STARTING STATE ONE");
		
			while (mainPlayerHealth.currentHealth >= 250) 

			{

				print("YOU ARE IN STATE ONE");
				// Wait for 1 second before starting next attack
				yield return new WaitForSeconds(1f);

				// Particle System Method
				OnParticleCollision (gameObject);

				print ("Particle Attack used");


			}

			// Switch the state to the Wave Attack State
			SetState(State.WaveAttack);

			yield return null;

			// Switching States Message
			print ("Switching States");
		}


		// STATE TWO
		// WAVE ATTACK STATE
		IEnumerator OnWaveAttack()
		{


			// State Two Code Here
			print ("YOU ARE IN STATE TWO");
		
			// While the player health is above 80 use the wave attack 

			   while (mainPlayerHealth.currentHealth >= 50) {
				// Wait for defined amount of time before executing next Wave Attack
		
				yield return new WaitForSeconds(1f);
				// Call the Wave Attack Functio every set amount of time predefined
				// Set the gunLine renderer to true so that when the boss shoots it will show the lineRenderer.
			    gunLine.enabled = true; 
			    WaveAttack (); 

			    //DestroyImmediate(gunLine); 
				// Play linked audio
				Audio.Play ();

				// Print a message for debugging only
				print ("Wave Attack Used");
		
				// Must yield return null
				yield return null;
			   // Wait for 0.1 seconds before setting the LineRenderer to false
			   yield return new WaitForSeconds (0.1f);
			// Set the gunLine line renderer to false so that when the boss shoots will it will not be visible. 
			   gunLine.enabled = false; 




			//DestroyImmediate(gunLine); 


			}

			// Switching States Message
			print ("Switching States");
			//Switch the state to STATE THREE
			SetState(State.Idle);
		
			// Pauses the execution of this method for one frame
			//yield return null;

		}

		// STATE THREE 
		IEnumerator OnIdle() 
		{

			print("YOU ARE IN STATE THREE");
			// Must yield return null
			// Pauses the execution of this method for one frame
			yield return null;

			// Switching States Message
			print ("Switching States");

			// Switch the scene to the lose state when you lose the game
			// When the player has a health of the set value then play the lose game music and switch the scene
			if (mainPlayerHealth.currentHealth >=0) {

				// Play a audio file by name in the unity Resources folder.
				// This will only work if its in the Resources folder.
				AudioSource audio = gameObject.AddComponent<AudioSource >();
				audio.PlayOneShot((AudioClip)Resources.Load("MainMenuMusic"));

				// Wait for ten seconds before switching the scene to the menu
				yield return new WaitForSeconds (10f);

				SceneManager.LoadScene ("MainScene");

			}

		
		}


		// STATE FOUR
		IEnumerator OnLaserAttack()
		{


			// Must yield return null
			// Pauses the execution of this method for one frame
			yield return null; 

		}


		// COROUTUNES USED FOR ENEMY AI (END)




		////////////////////////////////////////////////////////////////////
		// METHODS USED IN ENEMY AI (START)


		// This is the wave attack method that will be used for the enemy wave attack as one of the enemys AI features.
		// Wave Attack 
		void WaveAttack ()
		{
			//timer = 0f;
			// Do damange to the player via the WaveAttack from the player health script.
			// Play the audio sound of the attack 
			Audio.Play ();

			// Particle effects
			gunParticles.Stop ();
			gunParticles.Play ();
			gunLine.enabled = true;
			gunLine.SetPosition (0, transform.position);
			shootRay.origin = transform.position;
			shootRay.direction = transform.forward;



			if(Physics.Raycast (shootRay, out shootHit, projectileRange, PlayerDamageZone))
			{
				
				PlayerHealth playerHealth = shootHit.collider.GetComponent <PlayerHealth> ();



				// If the playerHealth is higher than 0 then damage the player using the particle effect
				if(playerHealth.currentHealth > 0)
				{
					//  Player will take damage when hit by the LineRenderer. 
					mainPlayerHealth.TakeDamage (DamageToGive);
					// Print message for debugging only

					print("YAY YOU GOT HIT");

				}
				gunLine.SetPosition (1, shootHit.point);
			}
			else
			{
				gunLine.SetPosition (1, shootRay.origin + shootRay.direction * projectileRange);
			}
		}



		// Particle Collider System
		// Make sure that the particle system in unity is set to world and that send collision message is clicked otherwise this script wont work.
		void OnParticleCollision(GameObject other) {
			Rigidbody body = other.GetComponent<Rigidbody>();

			if (body) {
				Vector3 direction = other.transform.position - transform.position;
				direction = direction.normalized;


				gunParticles.Play ();
				mainPlayerHealth.TakeDamage (DamageToGive);
				gunParticles.Stop ();


			}






		}


		// Laser Attack Method
		void LaserAttackMethod()
		{


		}

		// Particle Attack Method
		void ParticleAttackMethod() 
		{


		}

		// Activate Hexagon Method
		void ActivateHexagonMethod() 
		{


		}

		// Check if the player is in reach of attack method
		void CheckPlayerMethod() 

		{


		}


		// Check player distance method 
		void CheckPlayerDistanceMethod() 
		{

		}


		// Check cooldown timer 
		void CheckCoolDownTimer() 

		{

		}

		// Use Boss Tenticle Attack Method
		void TenticleAttackMethod() 

		{


		}


		void BackAwayFromPlayer() {


		}



		////////////////////////////////////////////////////////////////////
		// METHODS USED IN ENEMY AI (END)

	} // END SCRIPT
