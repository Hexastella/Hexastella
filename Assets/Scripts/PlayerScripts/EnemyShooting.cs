using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour {

	public int enemyDamage = 40;
	public float timeBetweenBullets = 0.15f;
	public float projectileRange = 200f;


	private float shootTime = 1.0f;
	public float timePeriod = 0.1f; 

	float timer;
	AudioSource Audio;


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


	// create the Awake function 

	void Awake ()
	{

		PlayerDamageZone = LayerMask.GetMask ("PlayerDamageZone");
		gunParticles = GetComponent<ParticleSystem> ();
		gunLine = GetComponent <LineRenderer> ();
		Audio = GetComponent<AudioSource> ();

	

	}





	// This update function will execute every set amount of time
	// This is where I am working on getting the enemy to shoot at the player 
	void Update () {
		shootTime += Time.deltaTime;

		if (shootTime >= timePeriod) {
			shootTime = shootTime - timePeriod;

			// Code here
			Shoot (); 
			//  Audio.Play ();
			print ("ShotOnce");



		}

	}





	void Shoot ()
	{
		timer = 0f;

		Audio.Play ();

		gunParticles.Stop ();
		gunParticles.Play ();

		gunLine.enabled = true;
		gunLine.SetPosition (0, transform.position);

		shootRay.origin = transform.position;
		shootRay.direction = transform.forward;

		if(Physics.Raycast (shootRay, out shootHit, projectileRange, PlayerDamageZone))
		{
			EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
			if(enemyHealth != null)
			{
				enemyHealth.TakeDamage (enemyDamage, shootHit.point);
			}
			gunLine.SetPosition (1, shootHit.point);
		}
		else
		{
			gunLine.SetPosition (1, shootRay.origin + shootRay.direction * projectileRange);
		}
	}
}
