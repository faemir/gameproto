using UnityEngine;
using System.Collections;

public class AlienController : MonoBehaviour {
	
	public float alienSpeed = .02f;
	public ScoreController score;
	
	// Use this for initialization
	void Start () { 
		Debug.Log ("hello");
		// Find("Score").GetComponent<ScoreController>()
		//ScoreController score = GameObject.Find("Score").GetComponent.<ScoreController>();
		score = GameObject.Find("Score").GetComponent<ScoreController>();

		Debug.Log (score);
	}
	
	// Update is called once per frame
	void Update () { 
		Debug.Log ("hello");
		//transform.position += new Vector3(0, -alienSpeed, 0);
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		// If the alien hits the trigger...
		if (col.gameObject.tag == "Bullet") {
			Destroy (gameObject);
			//if(score.score >= 100)
			//	score.score -= 100;
			//else if(score.score > 0)
			score.score++;

		}
	}
	
}