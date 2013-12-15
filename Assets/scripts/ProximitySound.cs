using UnityEngine;
using System.Collections;


public class ProximitySound : MonoBehaviour {

	public float proximityDistance = 1.5f;
	public float lookAheadOffset = 10f;
	public Transform proximity;

	Vector3 proxpos;
	bool spawningSound = false;

	void Start()
	{
		proxpos = transform.position;
	}

	void Update()
	{


		Vector2 origin;
		origin.x = transform.position.x +lookAheadOffset;
		origin.y = transform.position.y;

		RaycastHit2D hit;
		//look up
		hit =  Physics2D.Raycast (origin, Vector2.up, proximityDistance);
		if (hit != null) {
			proxpos.x = hit.point.x;
			proxpos.y = hit.point.y;
			if (!spawningSound) StartCoroutine("SpawnProximitySound");

		}
		//look down
		hit =  Physics2D.Raycast (origin, -Vector2.up, proximityDistance);
		if (hit != null) {
			proxpos.x = hit.point.x;
			proxpos.y = hit.point.y;
			if (!spawningSound) StartCoroutine("SpawnProximitySound");
		}

	}

	IEnumerator SpawnProximitySound()
	{
		spawningSound = true;
		Transform proxSound = Instantiate (proximity, proxpos, Quaternion.identity) as Transform;
		yield return new WaitForSeconds(Random.Range (0.1f, 1f));
		Destroy (proxSound.gameObject);
		spawningSound = false;
	}
	
}
