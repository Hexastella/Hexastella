using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : Unit {

	public float speed;
	public float jumpheight;
    public float deathPos;
    public float dashForce;
    public Transform enemy;
    public Transform cam;

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

        //player is looking at and rotating around the enemy
        transform.LookAt(enemy);

        //MOVEMENT
		float horizontalInput = Input.GetAxis("Horizontal");
		float verticalInput = Input.GetAxis("Vertical");

		//Normalize our input vector
		Vector3 input = new Vector3(horizontalInput, 0, verticalInput).normalized * speed;
        
        //JUMP
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

        //RUN animation
        if(verticalInput > 0.1f)
        {
            anim.SetBool("Run", true);
        }
        else
        {
            anim.SetBool("Run", false);
        }

        //SLASH animation
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Slash");
        }

        //DASH
        if (Input.GetKeyDown(KeyCode.F))
        {
            print("boom");
            //rb.AddRelativeForce(Vector3.back, ForceMode.Force);
            //transform.position += new Vector3(speed * Time.deltaTime, 0.0f, -40f);
        }

        //DODGE ROLL
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            anim.SetTrigger("Roll");
            transform.position += new Vector3(speed * Time.deltaTime, 0.0f, -20f);
        }

    }

    bool IsGrounded()
	{
		//Shoot raycast downw
		return Physics.Raycast(transform.position, Vector3.down, raycastDistance);
	}
}
