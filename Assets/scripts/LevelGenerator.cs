using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour 
{
	public LevelSection startSection;
	public int numberOfSectionsToSpawn = 100;
	public LevelSection[] sections = new LevelSection[0];
	public LevelSection[] finalSections = new LevelSection[0];

	private Vector3 head = Vector3.zero;

	// Use this for initialization
	void Start () 
	{
		head = transform.position;

		// start section
		for (int i = 0; i < startSection.section.Length; i++) 
		{
			Transform section = startSection.section[i];
			section = Instantiate(section,head,Quaternion.identity) as Transform;
			section.parent = transform;
			head += startSection.end;
		}

		// middle sections
		for (int i = 0; i < numberOfSectionsToSpawn; i++) 
		{
			int sectionsIndex = Random.Range(0, sections.Length);
			int transformIndex = Random.Range(0, sections[sectionsIndex].section.Length);
			Transform section = sections[sectionsIndex].section[transformIndex];
			section = Instantiate(section, head, Quaternion.identity) as Transform;
			section.parent = transform;
			head += sections[sectionsIndex].end;
		}

		// finale
		for (int i = 0; i < finalSections.Length; i++) 
		{
			for (int j = 0; j < finalSections[i].section.Length; j++)
			{
				Transform section = finalSections[i].section[j];
				section = Instantiate (section, head, Quaternion.identity) as Transform;
				section.parent = transform;
				head += finalSections[i].end;
			}
		}
	}

}

[System.Serializable]
public class LevelSection
{
	public Transform[] section = new Transform[0];
	public Vector3 end = Vector3.right * 40f;
}
