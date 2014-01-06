using UnityEngine;
using System.Collections;

public class Sun : MonoBehaviour {

	public Color slowColor = Color.blue;
	public Color fastColor = Color.red;

	private Player player;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
	}

	void Update () 
	{
		light.color = Color.Lerp (slowColor, fastColor, player.currentSpeed / player.maxSpeed);
	}
}
