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
	public Transform nuke;

	private Vector2 movement;
	private Transform upperFlame;
	private Transform lowerFlame;
	private bool nukeUsed = false;
	// Use this for initialization
	void Start () 
	{
		upperFlame = transform.FindChild ("flame_upper");
		lowerFlame = transform.FindChild ("flame_lower");

	}

	// Update is called once per frame
	void Update () 
	{


		// tell the gui what speed we're at
		currentSpeed = rigidbody2D.velocity.magnitude;
		GUIManager.Instance.playerSpeed = currentSpeed;

		// do nothing else if the game is over (player dead)
		if (GUIManager.Instance.gameOver)
						return;

		// player rotation is dependent on velocity
		if (rigidbody2D.velocity.x < 0f)
			transform.rotation = Quaternion.Euler( new Vector3(0f,0f,180f));
		else
			transform.rotation = Quaternion.Euler( Vector3.zero);

		// player audio pitch is dependent on speed
		audio.pitch = currentSpeed / (maxSpeed/12f);

		// player controls
		movement.x = (Input.GetAxisRaw ("Horizontal")) * acceleration;
		movement.y = Input.GetAxisRaw ("Vertical") * jumpForce;
		if (!nukeUsed) 
		{
			if (Input.GetKey(KeyCode.Space))
			{
				nukeUsed = true;
				Instantiate (nuke, transform.position + Vector3.back, transform.rotation);
				GUIManager.Instance.StartNukeMessage();
			}
		}

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
		GUIManager.Instance.SetGameOver ();
		audio.Stop();

		Instantiate (deathExplosion, transform.position + Vector3.back, Quaternion.LookRotation (Vector3.back));

		rigidbody2D.fixedAngle = false;
		rigidbody2D.drag = 5f;
		if (lowerFlame != null ) Destroy (lowerFlame.gameObject);
		if (upperFlame != null ) Destroy (upperFlame.gameObject);

		Debug.Log ("You're brown bread!");
	}
}
