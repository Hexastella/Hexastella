using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour {

    private float raycastDistance = 2f;

	void Start()
    {
		
	}

	void Update()
    {
		if(playerRange())
        {
            print("Attack!");
        }

        if (Input.GetMouseButtonDown(0) && playerRange())
        {
            print("Swish swosh");
        }
	}

    bool playerRange()
    {
        return Physics.Raycast(transform.position, Vector3.left, raycastDistance);
    }
}
