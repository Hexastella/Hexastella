using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : Unit {

	public float speed;
	public float jumpheight;
    public float deathPos;

    //public float vertRotation;
    // Camera up and down range float and make this public so we can set or change this float the unity editor.

    //public float CameraUpDownRange = 30f;
    // Set the mouse sensitivity and make this public so we can set or change this float in the  unity editor.
    //public float mousesensitivity = 4f;

    public Transform camPivot;

	private float raycastDistance = 4.5f;

	protected override void Start()
	{
		base.Start();
	}

	void Update() {

        //Lock the cursor
        Cursor.lockState = CursorLockMode.Locked;

        if(transform.position.y < deathPos)
        {
            SceneManager.LoadScene("GameScene");
        }

        // This is what controls the camera pivot allowing the user to rotate the camera up and down
        //vertRotation -= Input.GetAxis ("Mouse Y") * mousesensitivity;

        // We clamp the rotation of the camera to CameraUpDownRange which is the float defined and pass it in.
        //vertRotation = Mathf.Clamp (vertRotation, -CameraUpDownRange, CameraUpDownRange);
		//Camera.main.transform.localRotation = Quaternion.Euler (vertRotation, 0, 0);

		float horizontalInput = Input.GetAxis("Horizontal");
		float verticalInput = Input.GetAxis("Vertical");

		// We normalized our input vetor to make sure our input value always has a length of 1
		Vector3 input = new Vector3(horizontalInput, 0, verticalInput).normalized * speed;

		if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
			input.y = jumpheight;
			//anim.SetTrigger("Jump");
		} else

		{
			//make sure that the Y value of input is not 0
			input.y = rb.velocity.y;
		}

		//clamp max length of speed
		input = Vector3.ClampMagnitude(input, speed);

		// Rotates a vector from local to world space
		rb.velocity = transform.TransformVector(input);

		// Now we get the delta movement of the mouse
		float mouseXInput = Input.GetAxis("Mouse X");
        float mouseYInput = Input.GetAxis("Mouse Y");
        
        camPivot.Rotate(0, mouseXInput, 0);

        //anim.SetFloat("HorizontalSpeed", horizontalInput);
        //anim.SetFloat("VerticalSpeed", verticalInput);

        //make the player dash when they press F
        if (Input.GetKeyDown(KeyCode.F))
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0.0f, -40f);
        }

        //Dodge roll
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0.0f, -40f);
        }

    }

    bool IsGrounded()
	{
		// Shooting a raycast down will return true if hit something
		return Physics.Raycast(transform.position, Vector3.down, raycastDistance);
	}
}
