using UnityEngine;
using System.Collections;

public class DialogueTrigger : MonoBehaviour {

	public string dialogue;
	public string character;
	public float displayTime;
	void OnTriggerEnter2D(Collider2D info)
	{
		if (info.gameObject.tag == "Player") {
						GUIManager.Instance.StartDialogue (character, dialogue, displayTime);
						Destroy(this.gameObject);
				}
	}
}
