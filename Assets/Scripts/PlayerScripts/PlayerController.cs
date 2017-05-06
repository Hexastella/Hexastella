using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : Unit {

	public float speed;
	public float jumpheight;
    public float deathPos;

    public Transform camPivot;

	private float raycastDistance = 1f;

	protected override void Start()
	{
		base.Start();
	}

	void Update() {

        if(transform.position.y < deathPos)
        {
            SceneManager.LoadScene("GameScene");
        }

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

        if(verticalInput > 0.1f || verticalInput < -0.1f)
        {
            anim.SetBool("Run", true);
        }
        else
        {
            anim.SetBool("Run", false);
        }

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
            //anim.SetTrigger("Roll");
            transform.position += new Vector3(speed * Time.deltaTime, 0.0f, -20f);
        }

    }

    bool IsGrounded()
	{
		// Shooting a raycast down will return true if hit something
		return Physics.Raycast(transform.position, Vector3.down, raycastDistance);
	}
}
