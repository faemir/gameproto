using UnityEngine;
using System.Collections;

public class Sun : MonoBehaviour {

	public Color slowColor = Color.blue;
	public Color fastColor = Color.red;

	public Transform player;
	private Player playerScript;

	// Use this for initialization
	void Start () {
		playerScript = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
	}
	
	// Update is called once per frame
	void Update () {
		light.color = Color.Lerp (slowColor, fastColor, playerScript.currentSpeed / playerScript.maxSpeed);
	}
}
