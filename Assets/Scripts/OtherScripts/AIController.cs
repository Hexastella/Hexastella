using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : Unit {



	// Add a float for the mainbullet and instansiate it below
	public float mainBullet; 



	private NavMeshAgent agent;


	// Enums allows us to create a variable type and in this case a state. 
	// This variable can only have the values that we declare inside the enum.
	private enum State
	{

		Idle,
		MovingToOutpost,
		ChasingEnemy

	}

	private State currentState; 


	protected override void Start()
	{

		base.Start();

		agent = GetComponent<NavMeshAgent>();

		// Start State 
		SetState(State.Idle);


	}




	private void SetState (State newState)
	{
		StopAllCoroutines(); 
		currentState = newState;

		switch (currentState) {


		// State 1 OnIdle
		case State.Idle:
			StartCoroutine(OnIdle());
			break; 

	    // State 2  Moving To OutPost
		case State.MovingToOutpost:
			StartCoroutine (OnMovingToOutpost()); 
			break;

			// State 3 Chasing Enemy
		case State.ChasingEnemy:
			StartCoroutine (ChasingEnemy()); 
			break; 


		}
			

	}

	IEnumerator OnIdle ()
	{

		// This is called once
		while (true)  {
			// This is called every frame


			// Pauses the execution of this method for one frame
			yield return null; 

		}

	}


	IEnumerator OnMovingToOutpost ()
	{

		// This is called once

		while (true) {
			// This is called every frame


			// Pauses the execution of this method for one frame
			yield return null; 

		}

	}



	IEnumerator ChasingEnemy ()
	{

		// This is called once

		while (true) {
			// This is called every frame


			// Pauses the execution of this method for one frame
			yield return null; 

		}

	}

	
	// Update is called once per frame
	void Update () {

	 PlayerController player = FindObjectOfType<PlayerController>(); 

		agent.SetDestination (player.transform.position); 

		// We send the current speed of the agent every frame to display the movement animation

		anim.SetFloat ("VerticalSpeed", agent.velocity.magnitude); 


	

	
		
	}
}
