using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
<<<<<<< Updated upstream
	[SerializeField]
	private GameObject enemyPrefab;
	[SerializeField]
	private GameObject enemyContainer;

=======
>>>>>>> Stashed changes
	private bool stopSpawning = false;
	[SerializeField]
<<<<<<< Updated upstream
	private GameObject[] powerups;

	private void Start()
	{

	}

	public void StartSpawning()
=======
	private PowerupProbabilityClass[] powerups;

	void Start()
>>>>>>> Stashed changes
	{
		StartCoroutine(SpawnPowerupRoutine());
	}

<<<<<<< Updated upstream
	IEnumerator SpawnEnemyRoutine()
	{
		yield return new WaitForSeconds(2);
		
		while (stopSpawning == false)
		{
			Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
			GameObject newEnemy = Instantiate(enemyPrefab, posToSpawn, Quaternion.identity);
			newEnemy.transform.parent = enemyContainer.transform;
			yield return new WaitForSeconds(5.0f);
		}
	}
=======
>>>>>>> Stashed changes

	IEnumerator SpawnPowerupRoutine()
	{
		yield return new WaitForSeconds(2);

		while (stopSpawning == false)
		{
			Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7f, 0f);
<<<<<<< Updated upstream
			int randomPowerup = Random.Range(0, 3);
			Instantiate(powerups[randomPowerup], posToSpawn, Quaternion.identity);
			yield return new WaitForSeconds(Random.Range(5, 13));
=======
			int i = Random.Range(0, 100);


			for (int j = 0; j < powerups.Length; j++)
			{
				if (i >= powerups[j].minProbabilityRange && i <= powerups[j].maxProbabilityRange)
				{
					Instantiate(powerups[j].spawnedPowerups, posToSpawn, Quaternion.identity);
					yield return new WaitForSeconds(Random.Range(2, 4));
					break;

				}
				yield return new WaitForSeconds(Random.Range(2, 4));
			}
>>>>>>> Stashed changes
		}
	}

	public void OnPlayerDeath()
	{
		stopSpawning = true;
	}
}
