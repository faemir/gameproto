using UnityEngine;
using System.Collections;

public class RandomAmbience : MonoBehaviour {

	public AudioClip[] clips = new AudioClip[0];

	// Use this for initialization
	void Start () 
	{
		int index = Random.Range (0, clips.Length);
		audio.clip = clips [index];
		audio.loop = true;
		audio.Play();

	}

}
