using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public float moveSpeed = 10f;
	public float jumpForce = 100f;
	private Vector2 movement;

	// Use this for initialization
	void Start () 
	{
	}
	public float angle;
	// Update is called once per frame
	void Update () 
	{
		movement.x = Input.GetAxisRaw ("Horizontal") * moveSpeed;
		movement.y = Input.GetAxisRaw ("Vertical") * jumpForce;
	}

	void FixedUpdate()
	{
		rigidbody2D.AddForce(movement);
	}

	void OnCollisionEnter()
	{
		if (rigidbody2D.velocity.magnitude > 10f)
						PlayerDeath ();
	}

	void PlayerDeath()
	{

	}
}
