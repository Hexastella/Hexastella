<<<<<<< HEAD
﻿using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;


    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    bool playerInRange;
    float timer;


    void Awake ()
    {
        player = GameObject.Find("PlayerController");
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();
    }


    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = true;
        }
    }


    void OnTriggerExit (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = false;
        }
    }


    void Update ()
    {
        timer += Time.deltaTime;

        if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            Attack ();
        }

        if(playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger ("PlayerDead");
        }
    }


    void Attack ()
    {
        timer = 0f;

        if(playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage (attackDamage);
        }
    }
}
=======
﻿//  EnemyAttack.CS
//  Copyright © 2017 Jaxon Stevens. All rights reserved.
// Created by Jaxon Stevens and Hillary Ocando

// Created in part of our VFS game final project.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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


	// Particle collider 
	// Make sure that the particle system in unity is set to world and that send collision message is clicked otherwise this script wont work.
	void OnParticleCollision(GameObject other) {
		Rigidbody body = other.GetComponent<Rigidbody>();

		//if (body) {
			//Vector3 direction = other.transform.position - transform.position;
			//direction = direction.normalized;
			//body.AddForce (direction * 20); 

		//}

		// Print a message for debugging only
		print ("PARTICLES");
	


	}




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
		//This is called once, when I enter the OnIdle Coroutine

	         
		    print("YOU ARE IN STATE ONE");


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
		while (mainPlayerHealth.currentHealth > 80) {
			


			// Wait for defined amount of time before executing next Wave Attack
				yield return new WaitForSeconds(1f);

					// Call the Wave Attack Functio every set amount of time predefined
					WaveAttack (); 
			        // Play linked audio
					Audio.Play ();

					// Print a message for debugging only
					print ("ShotOnce");

			    // Must yield return null
				yield return null;
	
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

	

	}


	// STATE FOUR
	IEnumerator OnLaserAttack()
	{


		// Must yield return null
		// Pauses the execution of this method for one frame
		yield return null; 

	}



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

			//print("YAY YOU GOT HIT");

			PlayerHealth playerHealth = shootHit.collider.GetComponent <PlayerHealth> ();

			// If the playerHealth is higher than 0 then damage the player using the particle effect
			if(playerHealth.currentHealth > 0)
			{


				//  Player will take damage when hit by the LineRenderer. 
				mainPlayerHealth.TakeDamage (DamageToGive);

				// Print message for debugging only
				print("YAY YOU GOT HIT");


				//mainPlayerHealth.TakeDamage (attackDamage);

			}
			gunLine.SetPosition (1, shootHit.point);
		}
		else
		{
			gunLine.SetPosition (1, shootRay.origin + shootRay.direction * projectileRange);
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


	void 

}
>>>>>>> 01f42df601605adcc1849ebc3012da98982efb46
