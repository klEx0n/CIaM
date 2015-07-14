using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/* A Class which controls the next Level Segments
 * it spawns them on the right position depending on
 * the block before
 */

public class LevelBlockHandler : MonoBehaviour {
	
	public List<GameObject> levelBlocks;
	public int floor = 0;
	public int maxBlocks = 5;
	private int maxBlockSpawns = 2;
	private static int blocksTillFloorUP;
	private List<GameObject> levelSegments;

	// Use this for initialization
	void Start () {

		// Load all LevelBlocks into a List with Resources.LoadAll
		// The List is used for a fast access to Levelblocks
		levelSegments = new List<GameObject>();
		levelSegments = Resources.LoadAll<GameObject>("LevelBlocks/FloorClimbing").ToList();

		// Variable for a small variation in LevelSegments till Stairs can spawn
		blocksTillFloorUP = Random.Range(maxBlocks, (maxBlocks + Random.Range(0, 4)));
//		blocksTillFloorUP = maxBlocks;
	}

	public void SpawnNextBlock(float childrenSize)
	{
		// Run through LevelBlock List, if there is a Object with SpawnNewBlock bool
		// spawn a New Block
		// Trim Block on Position 0

		int randomBlock = Random.Range(0, levelSegments.Count);
		int lastIndex = levelBlocks.Count - 1;
		
		// Set the New Object to the Calculated Position depends on childrenSize
		Vector2 newPos = levelBlocks[lastIndex].transform.position;
		newPos.x += childrenSize;

		// Adding a new BlockObject to List
		/* If the stairs arent the next Block
		 * add a new random block to the blocklist
		 * also reposition the block
		 */
		if(blocksTillFloorUP > 0)
		{
			levelBlocks.Add(GameObject.Instantiate<GameObject>(levelSegments[randomBlock]));
			blocksTillFloorUP--;
			newPos.y = 5.5f * floor;
		} else
		{
			// If Blockscount is Zero, Spawn the Stairs for Floor Up
			blocksTillFloorUP = Random.Range(maxBlocks, maxBlocks * 2);
			levelBlocks.Add(GameObject.Instantiate(Resources.Load ("LevelBlocks/LevelBlock_end")) as GameObject);
			newPos.y = 5.5f * floor;
		}

		// reposition the last block
		levelBlocks[levelBlocks.Count - 1].transform.position = newPos;

		// If the LevelBlockCount is more then 3, Destroy and Delete the First Object in the List
		if(levelBlocks.Count > maxBlockSpawns)
		{
			GameObject.Destroy (levelBlocks[0]);
			levelBlocks.RemoveAt(0);
		}
	}


	/*
	 * Method for Repositioning LevelBlocks after Falling down a Floor
	 * Special Case needed:
	 * If the last Object in the List is a Stair, replace the Last Object
	 * with a new Random Segment
	 */
	public void RepositionBlockAfterFall()
	{
		ChangeFloor(-1);

		int lastIndex = levelBlocks.Count - 1;

		Vector2 newPos = levelBlocks[lastIndex].transform.position;
		newPos.y = 5.5f * floor;

		if(levelBlocks[lastIndex].tag == "Stairs")
		{
			Destroy (levelBlocks[lastIndex]);

			int randomBlock = Random.Range(0, levelSegments.Count);
			levelBlocks[lastIndex] = GameObject.Instantiate<GameObject>(levelSegments[randomBlock]);
		}

		levelBlocks[lastIndex].transform.position = newPos;

	}

	public void ChangeFloor(int floorCount)
	{
		// Add floorCount to variable
		floor += floorCount;
	}
	
	// Update is called once per frame
	void Update () {
	}
}
