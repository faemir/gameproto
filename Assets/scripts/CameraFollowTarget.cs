using UnityEngine;
using System.Collections;

public class CameraFollowTarget : MonoBehaviour 
{
	
	public Transform target;
	public float followSpeed = 8f;
	public float maxCameraDistance = 20f;
	public float minCameraDistance = 2f;
	public float minLookOffset = 2.5f;
	public float cameraSmoothing = 5f;
	public float minCameraSize = 3.5f;
	public float maxCameraSize = 5f;

	private Vector3 lastPosition;
	private Vector3 nextPosition;
	private float lastMoveTime;


	void Start ()
	{
		lastMoveTime = 0f;
		lastPosition = transform.position;
		nextPosition = target.transform.position;
	}



	// Update is called once per frame
	void LateUpdate () 
	{

		if (GUIManager.Instance.finaleMode) {
						float startTime = GUIManager.Instance.finaleTime;
			camera.orthographicSize = Mathf.Lerp (minCameraSize, maxCameraSize, (Time.time-startTime));
				} else {
						float startTime = GUIManager.Instance.finaleTime;
			camera.orthographicSize = Mathf.Lerp (maxCameraSize, minCameraSize, (Time.time-startTime));
				}

		if (GUIManager.Instance.gameOver)
				DeathCam ();
		else
				ChaseCam ();
	}

	void ChaseCam()
	{
		float distance = target.rigidbody2D.velocity.magnitude;

		if (minLookOffset > 0f) 
		{
			if ( target.rigidbody2D.velocity.x < 0f)
				minLookOffset = -minLookOffset;
		}
		if (minLookOffset < 0f) 
		{
			if ( target.rigidbody2D.velocity.x > 0f)
				minLookOffset = -minLookOffset;
		}

		if (distance > maxCameraDistance)
			distance = maxCameraDistance;
		if (distance < minCameraDistance)
			distance = minCameraDistance;
		nextPosition = target.position + Vector3.back *  ((maxCameraDistance+minCameraDistance)-distance);
		if (transform.position != lastPosition) 
		{
			lastPosition = transform.position;
			lastMoveTime = Time.time;
		}
		transform.position = Vector3.Slerp (lastPosition, nextPosition, (Time.time - lastMoveTime) * followSpeed);
		Vector3 lookDirection = (target.position + transform.right * minLookOffset) - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation (lookDirection);
		transform.rotation = Quaternion.Slerp (transform.rotation, lookRotation, Time.deltaTime * cameraSmoothing);
		//transform.LookAt (target.position + transform.right * minLookOffset);
	}

	void DeathCam()
	{
		nextPosition = target.position + Vector3.back * 50f;
		if (transform.position != lastPosition) 
		{
			lastPosition = transform.position;
			lastMoveTime = Time.time;
		}
		transform.position = Vector3.Slerp (lastPosition, nextPosition, (Time.time - lastMoveTime) * followSpeed);
		transform.LookAt (target.position);
	}
}
