	//  EnemyAttack.CS
	//  Copyright © 2017 Jaxon Stevens. All rights reserved.
	// Created by Jaxon Stevens and Hillary Ocando

	// Created in part of our VFS game final project.

	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine.SceneManagement;
     using UnityEngine.UI;

public class EnemyAIController : MonoBehaviour
	{
		public float timeBetweenAttacks = 0.0f;
		//public int attackDamage = 10;

	    // Display the currrent state of the EnemyAttack AI
	    public Text currentStateText; 


	    // Line Renderer Wave Attack Colors 
	    internal Color waveAttackColor = Color.white;
	    internal Color waveAttackColor2 = Color.red; 
	   

		// Laser Attack Line Renderer Wave Attack Colors
		internal Color laserAttackColor = Color.white;
		internal Color laserAttackColor2 = Color.yellow; 

	   
		internal Vector3 lineStartPos; 
		internal Vector3 lineEndPos; 


	    internal int DamageToGive = 40;
		internal float timeBetweenBullets = 0.15f;
		internal float projectileRange = 200f;


		internal float shootTime = 1.0f;
		internal float timePeriod = 1.0f; 

		//float timer;
		internal AudioSource Audio;

		// Attack Damage 
		internal PlayerHealth mainPlayerHealth;


	    // Define enemyParticleAttack from the enemyParticleAttack Script
	    internal  EnemyParticleAttack enemyParticleAttack;


	    // Define enemyWaveAttack from the enemyWaveAttack Script
	    internal EnemyWaveAttack enemyWaveAttack;

	    // Define enemyLaserAttack from the enemyLaserAttack script
	    internal EnemyLaserAttack enemyLaserAttack;


		public int attackDamage = 10;
	    EnemyAIController enemyAttack;

		// LineRenderer which is used for the wave attack 
	    internal LineRenderer waveAttackLine;
	    public  ParticleSystem gunParticles;

	    // LineRenderer which is used for the laser attack
	    internal LineRenderer laserAttackLine;
	    internal Ray shootRay = new Ray(); 

		// Create raycast shootHit
		internal RaycastHit shootHit;

		// Define the playerDamageZone
	    internal int PlayerDamageZone;

		// Used for the particle system when the enemy shoots
		ParticleSystem enemyParticles; 
		LineRenderer Line;

		// The amount of time the effects display
		float effectsDisplayTime = 0.2f;


		Animator anim;
		GameObject player;
		EnemyHealth enemyHealth;
		bool playerInRange;
		// float timer;


	    // Awake Method
		void Awake ()
		{
			player = GameObject.FindGameObjectWithTag ("Player");

		     currentStateText.text = "";

			// Make sure that the layer of the main player in unity is set to the defined value below otherwise the player will not recieve damage
			PlayerDamageZone = LayerMask.GetMask ("Shootable");
		    // Get the Particle System Component
			gunParticles = GetComponent<ParticleSystem> ();
		    // Wave Attack Line
			waveAttackLine = GetComponent <LineRenderer> ();
		    // Laser Attack Line 
		    laserAttackLine = GetComponent<LineRenderer> ();
		    // Get the AudioSource component
			Audio = GetComponent<AudioSource> ();
		    // Get the component of the PlayerHealth Script
			mainPlayerHealth = player.GetComponent <PlayerHealth> ();
		   // Get the component of the EnemyHealth Script
			enemyHealth = GetComponent<EnemyHealth>();
		    // Get the Animator Component
			anim = GetComponent <Animator> ();
		    // Get the component of the EnemyParticleAttack Script
		   enemyParticleAttack = GetComponent <EnemyParticleAttack>();
		    // Get the component of the EnemyWaveAttack Script
		   enemyWaveAttack = GetComponent<EnemyWaveAttack> ();
		   // Get the component of the EnemyLaserAttack Script
		   enemyLaserAttack = GetComponent<EnemyLaserAttack> ();


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
		//	timer = 0f;

			if(mainPlayerHealth.currentHealth > 0)
			{
				mainPlayerHealth.TakeDamage (attackDamage );
			}
		}



		// Kill Player function
		void KillPlayer() {

			if(mainPlayerHealth.currentHealth > 0)
			{
				mainPlayerHealth.TakeDamage (attackDamage);
			}

		}


		// COROUTUNES USED FOR ENEMY AI (START)
		private enum State
		{


		    ActivateHexagon,
		    UseHexagon,
			Walk,
			WaveAttack,
			LaserAttack,
			PlayerDie

		}

		// The initial start state
		void Start()
		{
			// Set Initial State here
			SetState (State.ActivateHexagon);

		}


		private State currentState;


		// Create ans set states for AI 
		private void SetState (State newState)
		{

			StopAllCoroutines ();
			currentState = newState;

			switch (currentState) {

		    case State.ActivateHexagon: 
			      StartCoroutine (OnActivateHexagon());
			      break; 


		   // case State.UseHexagon:
			   //  StartCoroutine (OnUseHexagon ());
			   //  break;

		     
			case State.Walk:
				StartCoroutine (OnWalk ());
				break;

			case State.WaveAttack:
				StartCoroutine (OnWaveAttack ());
				break;


			case State.LaserAttack:
				StartCoroutine (OnLaserAttack ());
				break; 


			case State.PlayerDie:
				StartCoroutine (OnPlayerDie());
				break;


			}



		}

	   // Activate Hexagon State 
	    IEnumerator OnActivateHexagon () {


		yield return new WaitForSeconds (1f);
		currentStateText.text = "Finding Hexagon";
		yield return new WaitForSeconds(1f);  
		currentStateText.text = "No supported Hexagon found";
		yield return new WaitForSeconds(1f);  
		currentStateText.text = "Switching to Particle Attack";

		// Switch the state to the Walk State (Particle Attack)
		SetState(State.Walk);

		yield return null;

	    }


	//IEnumerator OnUseHexagon() {


		//currentStateText


	//}



		// STATE ONE
		// Initial State where the AI will walk towards the player 
		IEnumerator OnWalk ()
		{

		// Define the current state and output the result on the GUI
		currentStateText.text = "Starting State One";


		yield return new WaitForSeconds(1f);  
		currentStateText.text = "Starting in 5";

		yield return new WaitForSeconds(1f);  
		currentStateText.text = "Starting in 4";

		yield return new WaitForSeconds(1f);  
		currentStateText.text = "Starting in 3";

		yield return new WaitForSeconds(1f);  
		currentStateText.text = "Starting in 2";

		yield return new WaitForSeconds(1f);  
		currentStateText.text = "Starting in 1";



		print("STARTING STATE ONE (Particle Attack)");
		
			while (mainPlayerHealth.currentHealth >= 300) 

			{
			
				print("BOSS IS IN STATE ONE");
				// Wait for 1 second before starting next attack
				yield return new WaitForSeconds(1f);

			currentStateText.text = "Boss is in State One";


			// Particle System Method

			enemyParticleAttack.ParticleCollision (player);


			// Define the current state and output the result on the GUI
			currentStateText.text = "Particle Attack Used";


			}


			// Switch the state to the Wave Attack State
			SetState(State.WaveAttack);

			yield return null;

			// Switching States Message
			//print ("Switching to Wave Attack State");

		// Define the current state and output the result on the GUI
		currentStateText.text = "Switching to Wave Attack State";


		}


		// STATE TWO
		// WAVE ATTACK STATE
		IEnumerator OnWaveAttack()
		{

		// CoolDown Timer Countdown
		currentStateText.text = "Cooldown Ends in 3";
		yield return new WaitForSeconds (1f);
		currentStateText.text = "Cooldown Ends in 2";
		yield return new WaitForSeconds(1f); 
		currentStateText.text = "Cooldown Ends in 1";
		yield return new WaitForSeconds(1f);  


			// State Two Code Here
			print ("BOSS IS IN STATE TWO");

		// Define the current state and output the result on the GUI
		currentStateText.text = "Boss is in State Two";

		//print (mainPlayerHealth);
		
			// While the player health is higher than 150 use the wave attack 

			   while (mainPlayerHealth.currentHealth >= 150) {
				// Wait for defined amount of time before executing next Wave Attack
		
				yield return new WaitForSeconds(0.1f);
				// Call the Wave Attack Functio every set amount of time predefined
				// Set the gunLine renderer to true so that when the boss shoots it will show the lineRenderer.

			    waveAttackLine.enabled = true; 
			   


			     // WaveAttack (); 
			    // Use the wave attack from the EnemyWaveAttack Script
			  yield return new WaitForSeconds(1f);

			   enemyWaveAttack.WaveAttack (); 

			    //DestroyImmediate(gunLine); 
				// Play linked audio
				//Audio.Play ();

			AudioSource waveAttackSound = gameObject.AddComponent<AudioSource >();
			waveAttackSound.PlayOneShot((AudioClip)Resources.Load("DeathRay"));


			// Define the current state and output the result on the GUI
			currentStateText.text = "Wave Attack Used";

				
			   // Wait for 0.1 seconds before setting the LineRenderer to false
			   yield return new WaitForSeconds (0.1f);
			   // Set the gunLine line renderer to false so that when the boss shoots will it will not be visible. 
			   waveAttackLine.enabled = false; 


			} // END WAVE ATTACK STATE



		//Switch the state to the Laser Attack State
		SetState(State.LaserAttack);
		    // Must yield return null
		    yield return null;
		    // Switching States Message
		    //print ("Switching To Laser Attack");

		// Define the current state and output the result on the GUI
		currentStateText.text = "Switching to Laser Attack";
			
		
			// Pauses the execution of this method for one frame
			//yield return null;

		}


	   
		// LASER ATTACK STATE
		IEnumerator OnLaserAttack()
	{

		// CoolDown Timer Countdown
		currentStateText.text = "Cooldown Ends in 3";
		yield return new WaitForSeconds (1f);
		currentStateText.text = "Cooldown Ends in 2";
		yield return new WaitForSeconds(1f); 
		currentStateText.text = "Cooldown Ends in 1";
		yield return new WaitForSeconds(1f);  


		// Define the current state and output the result on the GUI
		currentStateText.text = "Boss is in State Three";


		// Switch the scene to the lose state when you lose the game
		// When the player has a health of the set value then play the lose game music and switch the scene
		// When the player health is higher than 0 
		while (mainPlayerHealth.currentHealth > 0) {

			yield return new WaitForSeconds (1f);
			// Call the Wave Attack Functio every set amount of time predefined
			// Set the gunLine renderer to true so that when the boss shoots it will show the lineRenderer.

			laserAttackLine.enabled = true; 

			// Change Line Renderer Colors 

			enemyLaserAttack.LaserAttack ();


			//DestroyImmediate(gunLine); 
			// Play linked audio
			Audio.Play ();

			// Print a message for debugging only
			//print ("Laser Attack Used");

			// Define the current state and output the result on the GUI
			currentStateText.text = "Laser Attack Used";

			// Wait for 0.1 seconds before setting the LineRenderer to false
			yield return new WaitForSeconds (0.1f);
			// Set the gunLine line renderer to false so that when the boss shoots will it will not be visible. 
			laserAttackLine.enabled = false; 

			// Define the current state and output the result on the GUI
			currentStateText.text = "You are almost dead";

			// Define the current state and output the result on the GUI
			//currentStateText.text = "Switching to Final State";

			if (mainPlayerHealth.currentHealth <= 0) {


				//Switch the state to STATE THREE
				SetState (State.PlayerDie);

       

				// Must yield return null
				yield return null;

			}

		} // END LASER ATTACK


	}


		// PLAYER DIE STATE
		IEnumerator OnPlayerDie()
		{
	     
		// Define the current state and output the result on the GUI
		currentStateText.text = "Player Die State";

		//print ("Player Die State");
		if (mainPlayerHealth.currentHealth == 0) {

			// Play a audio file by name in the unity Resources folder.
			// This will only work if its in the Resources folder.
			AudioSource audio = gameObject.AddComponent<AudioSource >();
			audio.PlayOneShot((AudioClip)Resources.Load("MainMenuMusic"));

			// Wait for ten seconds before switching the scene to the menu
			yield return new WaitForSeconds (1f);

			SceneManager.LoadScene ("MainScene");

		}

			// Must yield return null
			// Pauses the execution of this method for one frame
			yield return null; 

		}


		// COROUTUNES USED FOR ENEMY AI (END)




		////////////////////////////////////////////////////////////////////
		// METHODS USED IN ENEMY AI (START)


		// This is the wave attack method that will be used for the enemy wave attack as one of the enemys AI features.

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
