using UnityEngine;
using System.Collections;

public class Sun : MonoBehaviour {

	public Color slowColor = Color.blue;
	public Color fastColor = Color.red;

	private Player player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
	}
	
	// Update is called once per frame
	void Update () {
		light.color = Color.Lerp (slowColor, fastColor, player.currentSpeed / player.maxSpeed);
	}
}
