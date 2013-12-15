using UnityEngine;
using System.Collections;

public class FinaleTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D info)
	{
		if (info.collider2D.tag == "Player") {
			GUIManager.Instance.finaleMode = true;
				}
	}

	void OnTriggerExit2D(Collider2D info)
	{
		if (info.collider2D.tag == "Player") {
			GUIManager.Instance.finaleMode = false;
		}
	}
}
