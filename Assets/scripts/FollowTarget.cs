using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour {
	
	public Transform target;
	public Vector3 offset = Vector3.zero;

	// Update is called once per frame
	void Update () {
		transform.position = target.position + offset;
	}
}
