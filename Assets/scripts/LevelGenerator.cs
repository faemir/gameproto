using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour 
{
	public LevelSection[] sections = new LevelSection[0];
	public int numberOfSectionsToSpawn = 100;

	private Vector3 head = Vector3.zero;

	// Use this for initialization
	void Start () 
	{
		for (int i = 0; i < numberOfSectionsToSpawn; i++) 
		{
			int sectionsIndex = Random.Range(0, sections.Length);
			int transformIndex = Random.Range(0, sections[sectionsIndex].section.Length);
			Transform section = sections[sectionsIndex].section[transformIndex];
			Instantiate(section, head, Quaternion.identity);
			head += sections[sectionsIndex].end;
		}
	}

}

[System.Serializable]
public class LevelSection
{
	public Transform[] section = new Transform[0];
	public Vector3 end = Vector3.right * 40f;
}
