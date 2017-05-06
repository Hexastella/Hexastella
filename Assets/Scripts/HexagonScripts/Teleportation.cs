using UnityEngine;
using System.Collections;

public class Teleportation : MonoBehaviour
{

	// Create a public GameObject for the teleportLabel which will appear when the player is by the teleport trigger.
	public GameObject teleportLabel;
	// Create a public GameObject for the object to teleport. In the case we wanyt to teleport the player controller.
	public GameObject objectToTeleport;
	// Create a public transform where you create a empty GameObject where the player will teleport to.
	public Transform toLocation;

	void Start()
	{

		// Set the teleportLabel to false.
		teleportLabel.SetActive(false);
	}

	void OnTriggerStay(Collider other)
	{

		// Set the teleportLabel to true. 
		teleportLabel.SetActive(true);

		// If a gameobject has the tag "Player" and the key defined below is pressed then execute the code below.
		if ((other.gameObject.tag == "Player") && Input.GetKeyDown(KeyCode.T))
		{

			// Teleport the objectToTeleport defined to the location of the empty GameObject using transform.
			objectToTeleport.transform.position = toLocation.transform.position;
		}
	}


	// When the trigger is exited
	void OnTriggerExit()
	{

		// Set the teleportLable to false 
		teleportLabel.SetActive(false);
	}
}