using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/* This class is a Singleton which controls the playtime
 * it can be used in other classes for example,
 * to add time in powerups or decrease time if the Player
 * got hit by an obstacle
 */

public class TimeLineController : MonoBehaviour {

	private static TimeLineController instance;

	public float startTime = 30f;
	public float maxTime = 60f;
	private float timeLeft = 0;

	public bool gameOver = false;
	private Slider timeLineDummy;
	public Text time;
	private float livedTime = 0;

	// Singleton creation
	public static TimeLineController Instance
	{
		get{
			if(instance == null)
			{
				instance = GameObject.FindGameObjectWithTag("GameController").GetComponent<TimeLineController>();
			}
			return instance;
		}
	}

	void Awake()
	{
		timeLineDummy = GameObject.FindGameObjectWithTag("TimeLine").GetComponent<Slider>();
	}

	// Use this for initialization
	void Start () {
		timeLeft = startTime;
		timeLineDummy.maxValue = maxTime;
		timeLineDummy.value = timeLeft;
	}

	// Control method for Time adding
	public void AddTime(float extraTime)
	{
		timeLeft += extraTime;

		if(timeLeft > maxTime)
			timeLeft = maxTime;
	}

	// Control method for time reduction
	public void DecreaseTime(float timeLoss)
	{
		timeLeft -= timeLoss;

		if(timeLeft < 0)
		{
			timeLeft = 0;
			gameOver = true;
		}
	}

	public float GetLivedTime()
	{
		return livedTime;
	}

	// Update is called once per frame
	void Update () {
		timeLeft -= Time.deltaTime;
		livedTime += Time.deltaTime;
		timeLineDummy.value = timeLeft;
		time.text = Mathf.RoundToInt(timeLeft).ToString();;

		if(timeLeft <= 0)
			gameOver = true;
	}
}
