using UnityEngine;
using System.Collections;

// Simple Character movement class

public class CharacterMovement_Runner : MonoBehaviour {

	public float accelaration = 1f;
	public float jumpForce = 1f;
	public float maxSpeed = 10f;
	public float maxJumpHeight = 20f;

	public GroundCheck groundcheck;
	private Rigidbody2D rb;
	private bool jumped = false;
	private Animator anim;

	void Awake()
	{
		groundcheck = GameObject.FindObjectOfType(typeof(GroundCheck)) as GroundCheck;
//		anim = GetComponent<Animator>();
	}

	// Use this for initialization
	void Start () {
		//Get The Rigidbody of this Object
		rb = GetComponent<Rigidbody2D>();
	}
	// Update is called once per frame
	void Update () {
//		Jump();
	}

	// Method for Jumping
	public void Jump()
	{
		// If Jump Button is Pressed, add Jumpforce to Character
		if(Input.GetAxis("Jump") > 0.1 && jumped == false)
		{
			rb.AddForce(Vector2.up * jumpForce);
//			anim.SetTrigger("Jump");
			jumped = true;
			groundcheck.SetGrounded(false);
		}
		
		if(rb.velocity.y > maxJumpHeight)
			rb.velocity = new Vector3(rb.velocity.x, maxJumpHeight);
	}

	// Method for Moving Character
	public void MoveCharacter()
	{
		//Add Force to Character
		if(groundcheck.GetGrounded())
		{
			rb.AddForce(Vector2.right * accelaration);
			
			//Check if Character has reached Max speed, if so, set Speed to max Speed
			if(rb.velocity.x > maxSpeed)
				rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
		}

		if(groundcheck.GetGrounded())
		{
			jumped = false;
		}
	}

	//Decrease Speed if Character is Hit by Enemy
	public void DecreaseSpeed()
	{

	}

	// Method for climbin Floor
	public void ClimbFloor()
	{

	}

	void FixedUpdate()
	{
		MoveCharacter();
		Jump();
	}
}
