using UnityEngine;
using System.Collections;

/*
 * A Class which handles the character behaviour
 * if the Player falls down into a hole
 */

public class FallHandler : MonoBehaviour {

	private LevelBlockHandler levelHandler;
	private bool once = false;
	private GroundCheck grounded;
	// Use this for initialization
	void Start () {
		// Get needed Objects
		levelHandler = GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelBlockHandler>();
		grounded = GameObject.FindGameObjectWithTag("GroundChk").GetComponent<GroundCheck>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		// If the Collided object is the Player
		if(other.tag == "Player")
		{
			// Checks if the Player already collided (because of the two colliders)
			if(once == false)
			{
				// Set grounded to false (player is in the air)
				grounded.SetGrounded(false);

				// Set the Velocity of the Player to 0, so that he can fall straight down the way 
				Vector2 velocityPlayer = other.GetComponent<Rigidbody2D>().velocity;
				velocityPlayer.x = 0;
				other.GetComponent<Rigidbody2D>().velocity = velocityPlayer;

				once = true;
				// Reposition the next level block to the right place
				levelHandler.RepositionBlockAfterFall();
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
