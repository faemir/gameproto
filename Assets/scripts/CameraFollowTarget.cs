using UnityEngine;
using System.Collections;

public class CameraFollowTarget : MonoBehaviour 
{
	
	public Transform target;
	public float horizontalPercScreenMoveArea = 0.25f;
	public float verticalPercScreenMoveArea = 0.25f;
	public float followSpeed = 8f;
	public float maxCameraDistance = 20f;
	public float minCameraDistance = 2f;
	public float minLookOffset = 2.5f;
	public float cameraSmoothing = 5f;

	private Vector3 lastPosition;
	private Vector3 nextPosition;
	private float lastMoveTime;

	void Start ()
	{
		lastPosition = transform.position;
		nextPosition = transform.position;
	}

	// Update is called once per frame
	void LateUpdate () 
	{
		if (GUIManager.Instance.finaleMode) {
						float startTime = GUIManager.Instance.finaleTime;
						camera.orthographicSize = Mathf.Lerp (3.5f, 7f, (Time.time - startTime)*5f);
				} else {
						float startTime = GUIManager.Instance.finaleTime;
						camera.orthographicSize = Mathf.Lerp (7f, 3.5f, (Time.time - startTime)*5f);
				}

		if (GUIManager.Instance.gameOver)
				DeathCam ();
		else
				ChaseCam ();
	}

	void ChaseCam()
	{
		float distance = target.rigidbody2D.velocity.magnitude;
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
