using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public float moveSpeed = 5f;
	public float jumpForce = 100f;
	public float currentSpeed = 0f;

	public Material[] upperFlames = new Material[4];
	public Material[] lowerFlames = new Material[4];

	public Transform deathExplosion;

	private Vector2 movement;
	private Transform upperFlame;
	private Transform lowerFlame;
	// Use this for initialization
	void Start () 
	{
		upperFlame = transform.FindChild ("flame_upper");
		lowerFlame = transform.FindChild ("flame_lower");

	}

	// Update is called once per frame
	void Update () 
	{
		if (GUIManager.Instance.gameOver)
						return;

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

		Vector3 scale = transform.localScale;
		scale.x = moveSpeed * 0.1f;
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
		Instantiate (deathExplosion, transform.position + Vector3.back, Quaternion.LookRotation (Vector3.back));
		GUIManager.Instance.gameOver = true;
		rigidbody2D.fixedAngle = false;
		rigidbody2D.drag = 5f;
		Destroy (lowerFlame.gameObject);
		Destroy (upperFlame.gameObject);
	}
}
