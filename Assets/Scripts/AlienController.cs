﻿using UnityEngine;
using System.Collections;

public class AlienController : MonoBehaviour {
	
	public float alienSpeed = .02f;
	public ScoreController score;
	
	// Use this for initialization
	void Start () { 
		score = GameObject.Find("Score").GetComponent<ScoreController>();
	}
	
	// Update is called once per frame
	void Update () { 
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