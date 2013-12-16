using UnityEngine;

/// <summary>
/// Simply moves the current game object
/// </summary>
public class SpawnController : MonoBehaviour
{
	public int enemyCount = 0;
	public GameObject enemy;

	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if (enemyCount <= 5) {
			GameObject.Instantiate(enemy, transform.position, transform.rotation);
			enemyCount++;
		}
	}
}


//public class SpawnController : MonoBehaviour {
//	public GameObject enemy;
//	public int enemyNo;
//
//	// Use this for initialization
//	
//	

//}
