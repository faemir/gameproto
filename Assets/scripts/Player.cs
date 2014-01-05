using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public float acceleration = 5f;
	public float maxSpeed = 75f;
	public float jumpForce = 100f;
	public float currentSpeed = 0f;
	public float currentAcceleration
	{
		get 
		{
			return movement.magnitude;
		}
	}

	public Material[] upperFlames = new Material[4];
	public Material[] lowerFlames = new Material[4];

	public Transform deathExplosion;
	public Transform nuke;

	private Vector2 movement;
	private Transform upperFlame;
	private Transform lowerFlame;
	private bool nukeUsed = false;
	private bool isDead = false;
	// Use this for initialization
	void Start () 
	{
		isDead = false;
	}

	// Update is called once per frame
	void Update () 
	{


		// tell the gui what speed we're at
		currentSpeed = rigidbody2D.velocity.magnitude;
		//GUIManager.Instance.playerSpeed = currentSpeed;

		// do nothing else if the game is over (player dead)
		if (isDead) 
		{
			return;
		}

		Movement ();
		Aesthetics ();
		
	}

	void Movement()
	{
		// player controls
		movement.x = acceleration;
		movement.y = Input.GetAxisRaw ("Vertical") * jumpForce;
		if (Input.GetAxisRaw ("Vertical") > 0f && rigidbody2D.velocity.y < 0f)
		{
			movement.y += -rigidbody2D.velocity.y;
		}
		if (Input.GetAxisRaw ("Vertical") < 0f && rigidbody2D.velocity.y > 0f)
		{
			movement.y += -rigidbody2D.velocity.y;
		}

		
		if (!nukeUsed) 
		{
			if (Input.GetKey(KeyCode.Space))
			{
				nukeUsed = true;
				Instantiate (nuke, transform.position + Vector3.back, transform.rotation);
				GameManager.Instance.StartDialogue("Commander:", "TURN AROUND AND GET OUTTA THERE! NUKE AWAY!",10f);
			}
		}

		// player rotation is dependent on velocity
		if (rigidbody2D.velocity.x < 0f)
			transform.rotation = Quaternion.Euler( new Vector3(0f,0f,180f));
		else
			transform.rotation = Quaternion.Euler( Vector3.zero);
	}

	void Aesthetics()
	{		
		// player audio pitch is dependent on speed
		audio.pitch = currentSpeed / (maxSpeed/12f);

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
		isDead = true;
		GameManager.Instance.State = GameManager.GameState.GameOver;
		GameManager.Instance.StartDialogue("Commander:", "Well, shit.",1.5f);
		audio.Stop();

		Instantiate (deathExplosion, transform.position + Vector3.back, Quaternion.LookRotation (Vector3.back));

		rigidbody2D.fixedAngle = false;
		rigidbody2D.drag = 2f;
		if (lowerFlame != null ) Destroy (lowerFlame.gameObject);
		if (upperFlame != null ) Destroy (upperFlame.gameObject);

		Debug.Log ("You're brown bread!");

	}
}
