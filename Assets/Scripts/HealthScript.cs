using UnityEngine;

/// <summary>
/// Handle hitpoints and damages
/// </summary>
public class HealthScript : MonoBehaviour
{
	/// <summary>
	/// Total hitpoints
	/// </summary>
	public int hp = 1;
	
	/// <summary>
	/// Enemy or player?
	/// </summary>
	public bool isEnemy = true;
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		// Is this a shot?
		BulletController shot = collider.gameObject.GetComponent<BulletController>();
		if (shot != null)
		{
			// Avoid friendly fire
			if (shot.isEnemyShot != isEnemy)
			{
				hp -= shot.damage;
				
				// Destroy the shot
				// Remember to always target the game object,
				// otherwise you will just remove the script.
				Destroy(shot.gameObject);
				
				if (hp <= 0)
				{
					// Dead!
					Destroy(gameObject);
				}
			}
		}
	}
}
