using UnityEngine;
using System.Collections;

public class ScoreController : MonoBehaviour {
	
	public int score = 0;					// The player's score. 
	
	void Awake ()
	{
	}
	
	void Update ()
	{
		// Set the score text.
		guiText.text = "Score: " + score;
		
		// Set the previous score to this frame's score.
	}
}