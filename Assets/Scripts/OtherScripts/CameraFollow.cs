using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform player;
    public float camSpeed;
    public float zOffSet;
    public float yOffSet;

    void Update ()
    {
	    if(player)
        {
            Vector3 newPos = transform.position;
            newPos.x = player.transform.position.x;
            newPos.y = player.transform.position.y + yOffSet;
            newPos.z = player.transform.position.z + zOffSet;

            transform.position = Vector3.Lerp(transform.position, newPos, camSpeed * Time.deltaTime);
        }
	}
}
