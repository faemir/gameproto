using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour {
	
	public Transform target;
	public float minHeight = 2f;
	public float maxHeight = 10f;
	public float maxSpeed = 20f;

	// Update is called once per frame
	void Update () 
	{
		Vector3 offset = Vector3.zero;
		float speed = target.rigidbody.velocity.magnitude;
		offset.y = FloatScale(0f, maxSpeed, minHeight, maxHeight, speed);
		transform.position = target.position + offset;

	}

	float FloatScale(float oldMin, float oldMax, float newMin, float newMax, float value)
	{
		float oldRange = oldMax - oldMin;
		float newRange = newMax - newMin;
		return (((value - oldMin)*newRange)/oldRange)+newMin;
	}
}
