using UnityEngine;
using System.Collections;

// This Class triggers the next Spawn for a level Segment

public class LevelBlock : MonoBehaviour {

	private bool generateNew = false;
	private float childrenLength;
	private LevelBlockHandler handler;

	// Use this for initialization
	void Start () {
		handler = GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelBlockHandler>();
		transform.parent = handler.transform;


		// This Loop is used to calculate the Size of the LevelBlock (the childrin inside)
		// the Size is used for the next Positioning
		for(int i = 0; i < transform.childCount; i++)
		{
			if(transform.GetChild(i).tag == "Ground")
			{
				if(transform.GetChild(i).GetComponent<BoxCollider2D>() != null)
					// childrenLenght is used for the reposition
					childrenLength += transform.GetChild(i).GetComponent<BoxCollider2D>().size.y;
			}
		}
	}

	/*
	 * If the hitted Object is the Player
	 * generate the Next Block
	 * Also special Case:
	 * If I m a Stair, also add 1 to Floorcount
	 */
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			if(generateNew == false)
			{
				generateNew = true;

				if(gameObject.tag == "Stairs")
				{
					handler.ChangeFloor(1);
				}

				handler.SpawnNextBlock(childrenLength);
			}
		}
	}

	public bool GetGenerateNew()
	{
		return generateNew;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
