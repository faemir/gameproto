using UnityEngine;
using System.Collections;

public class Fred : MonoBehaviour 
{

	public float lanceTurnMultiplier = 0f;
	private Rigidbody lance;
	private float torque;
	// Use this for initialization
	void Start () 
	{
		lance = transform.FindChild("Lance").rigidbody;
	}
	
	// Update is called once per frame
	void Update () 
	{
		torque = Input.GetAxis("Vertical") * lanceTurnMultiplier;
		lance.AddTorque(0f, -torque, 0f, ForceMode.Acceleration);
	}
}
