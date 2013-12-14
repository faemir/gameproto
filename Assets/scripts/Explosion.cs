using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {
	
	public float endSize = 10f;
	public float speedOfExpansion = 1f;
	public Material[] materials = new Material[1];


	private float startTime;
	// Use this for initialization
	void Start () 
	{
		int matIndex = Random.Range (0, materials.Length);
		renderer.material = materials [matIndex];

		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () 
	{
		float scale = Mathf.Lerp (0f, endSize, (Time.time - startTime) * 1/speedOfExpansion);
		transform.localScale = Vector3.one * scale;
	}
}
