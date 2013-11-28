using UnityEngine;
using System.Collections;

public class Dylan : MonoBehaviour 
{

	public JoustDirection direction = JoustDirection.LeftToRight;
	public float endForce = 40f;

	public enum JoustDirection { LeftToRight, RightToLeft };

	private Rigidbody body;
	private float force = 0f;

	// Use this for initialization
	void Start () 
	{
		body = transform.FindChild("Body").rigidbody;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		Vector3 forceDirection = Vector3.zero;

		if ( direction == JoustDirection.LeftToRight ) forceDirection = Vector3.forward;
		else forceDirection = Vector3.back;

		if ( force > endForce ) force = endForce;
		body.AddForce(forceDirection * force);
		force = force + Time.time;
	}
}
