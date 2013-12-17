using UnityEngine;
using System.Collections;
using System.IO;

/// <summary>
/// Simply moves the current game object
/// </summary>
public class SpawnController : MonoBehaviour
{
	public int enemyCount = 0;
	public GameObject enemy;

	void Start () {
		InvokeRepeating ("spawnEnemy", 2, 2f);
	}

	// Update is called once per frame
	void Update () {

	}

	void spawnEnemy() {
//		if (enemyCount <= 5) {
			GameObject.Instantiate(enemy, transform.position, transform.rotation);
//			enemyCount++;
			
//		}
	}
}
