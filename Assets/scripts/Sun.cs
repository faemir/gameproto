using UnityEngine;
using System.Collections;

public class Sun : MonoBehaviour {

	public Color slowColor = Color.blue;
	public Color fastColor = Color.red;

	public Transform player;
	private Player playerScript;

	void Awake()
	{
		playerScript = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
	}

	void Update () 
	{
		light.color = Color.Lerp (slowColor, fastColor, playerScript.currentSpeed / playerScript.maxSpeed);
	}
}
