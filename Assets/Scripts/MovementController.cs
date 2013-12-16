using UnityEngine;

/// <summary>
/// Simply moves the current game object
/// </summary>
public class MovementController : MonoBehaviour
{
	// 1 - Designer variables
	
	/// <summary>
	/// Object speed
	/// </summary>
	public Vector2 speed = new Vector2(10, 10);
	
	/// <summary>
	/// Moving direction
	/// </summary>
	public Vector2 direction = new Vector2(-1, 0);
	
	private Vector2 movement;

	public float moveForce = 10f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 0.5f;				// The fastest the player can travel in the x axis.
	
	void Update()
	{

		// 2 - Movement
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
		// Apply movement to the rigidbody
		rigidbody2D.velocity = movement;
	}
}
