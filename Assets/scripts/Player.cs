using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public float moveSpeed = 5f;
	public float jumpForce = 100f;
	public float currentSpeed = 0f;

	public Material[] upperFlames = new Material[4];
	public Material[] lowerFlames = new Material[4];

	private Vector2 movement;
	private Transform upperFlame;
	private Transform lowerFlame;
	// Use this for initialization
	void Start () 
	{
		upperFlame = transform.FindChild ("flame_upper");
		lowerFlame = transform.FindChild ("flame_lower");

	}
	public float angle;
	// Update is called once per frame
	void Update () 
	{
		movement.x = moveSpeed;
		movement.y = Input.GetAxisRaw ("Vertical") * jumpForce;
		currentSpeed = rigidbody2D.velocity.magnitude;

		// set flame material according to currentSpeed
		if (currentSpeed < 25f) {
						upperFlame.renderer.material = upperFlames [0];
						lowerFlame.renderer.material = lowerFlames [0];
				} else if (currentSpeed >= 25f && currentSpeed < 50f) {
						upperFlame.renderer.material = upperFlames [1];
						lowerFlame.renderer.material = lowerFlames [1];
				} else if (currentSpeed >= 50f && currentSpeed < 75f) {
						upperFlame.renderer.material = upperFlames [2];
						lowerFlame.renderer.material = lowerFlames [2];
				} else if (currentSpeed >= 75f) {
						upperFlame.renderer.material = upperFlames [3];
						lowerFlame.renderer.material = lowerFlames [3];
				}

	}

	void FixedUpdate()
	{
		rigidbody2D.AddForce(movement);
	}

	void OnCollisionEnter2D(Collision2D info)
	{
		if (info.relativeVelocity.magnitude > 10f)
						PlayerDeath();
	}

	void PlayerDeath()
	{
		Debug.Log ("You're brown bread!");
	}
}
