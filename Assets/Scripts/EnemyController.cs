using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
	public Vector2 speed = new Vector2(20, 20);
	public Vector2 direction = new Vector2(-10, -10);
	public float moveForce = 10f;
	public float maxSpeed = 0.5f;

	public float alienSpeed = .02f;
	public ScoreController score;
	
	private Vector2 movement;
	private Transform groundDetector;
	private bool grounded = false;

	void start(){
		score = GameObject.Find ("Score").GetComponent<ScoreController> ();
	}

	void awake(){
		groundDetector = transform.Find("GroundDetectorEn");
	}
	void Update()
	{
		// The enemy is grounded if a linecast to the groundcheck position hits anything on the ground layer.
		grounded = Physics2D.Linecast(transform.position, groundDetector.position, 1 << LayerMask.NameToLayer("Terrain"));

		movement = new Vector2(
			speed.x * direction.x,
			speed.y * direction.y);
		
		if (renderer.IsVisibleFrom(Camera.main) == false)
		{
			Destroy(gameObject);
		}
		
	}
	
	void FixedUpdate()
	{
		if (grounded) {
			rigidbody2D.velocity = movement;
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		// If the alien hits the bullet...
		if (col.gameObject.tag == "Bullet") {
			Debug.Log ("bullet");
			score.score += 100;
			
		}
	}
}
