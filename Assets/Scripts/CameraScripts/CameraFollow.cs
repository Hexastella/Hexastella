using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform camPivot;

    private void Start()
    {
        
    }

    void LateUpdate()
    {
        //Camera look up and down
        float mouseYInput = Input.GetAxis("Mouse Y");
        transform.Rotate(-mouseYInput, 0, 0);

        //Camera look sideways
        float mouseXInput = Input.GetAxis("Mouse X");
        camPivot.Rotate(0, mouseXInput, 0);
    }
}