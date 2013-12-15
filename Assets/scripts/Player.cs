using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public float acceleration = 5f;
	public float maxSpeed = 75f;
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
		currentSpeed = rigidbody2D.velocity.magnitude;
		if (GUIManager.Instance.gameOver)
						return;

		audio.pitch = currentSpeed / (maxSpeed/12f);

		movement.x = (Input.GetAxisRaw ("Horizontal") + 1f) * acceleration;
		movement.y = Input.GetAxisRaw ("Vertical") * jumpForce;


		// set flame material according to currentSpeed
		if (currentSpeed < 0.25f * maxSpeed) {
						upperFlame.renderer.material = upperFlames [0];
						lowerFlame.renderer.material = lowerFlames [0];
				} else if (currentSpeed >= 0.25f * maxSpeed && currentSpeed < 0.50f * maxSpeed) {
						upperFlame.renderer.material = upperFlames [1];
						lowerFlame.renderer.material = lowerFlames [1];
				} else if (currentSpeed >= 0.50f * maxSpeed && currentSpeed < 0.75f * maxSpeed) {
						upperFlame.renderer.material = upperFlames [2];
						lowerFlame.renderer.material = lowerFlames [2];
				} else if (currentSpeed >= 0.75f * maxSpeed) {
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
		GUIManager.Instance.gameOver = true;
		audio.Stop();

		Instantiate (deathExplosion, transform.position + Vector3.back, Quaternion.LookRotation (Vector3.back));

		rigidbody2D.fixedAngle = false;
		rigidbody2D.drag = 5f;
		if (lowerFlame != null ) Destroy (lowerFlame.gameObject);
		if (upperFlame != null ) Destroy (upperFlame.gameObject);
		Debug.Log ("You're brown bread!");
	}
}
