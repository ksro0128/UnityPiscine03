using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner3 : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject enemyPrefab2;
    [SerializeField] private GameObject enemyPrefab3;
    [SerializeField] private GameObject[] waypoints;

    private bool spawning = true;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

	IEnumerator SpawnEnemies()
	{
		yield return StartCoroutine(SpawnEnemy(enemyPrefab, 2f, 15));
		yield return StartCoroutine(SpawnEnemy(enemyPrefab2, 2f, 15));
		StartCoroutine(SpawnEnemy(enemyPrefab, 0.75f, 20));
		yield return StartCoroutine(SpawnEnemy(enemyPrefab2, 1.25f, 16));
		StartCoroutine(SpawnEnemy(enemyPrefab, 0.75f, 30));
		StartCoroutine(SpawnEnemy(enemyPrefab2, 1.25f, 20));
		yield return StartCoroutine(SpawnEnemy(enemyPrefab3, 1.75f, 12));
	}

    IEnumerator SpawnEnemy(GameObject prefab, float spawnTime, int Count)
    {
		while (spawning)
		{
			if (Count <= 0)
				yield break;

			Count--;
			GameObject e = Instantiate(prefab, transform.position, transform.rotation);
			e.GetComponent<EnemyController>().InitWayPoints(waypoints);
			StageManager.instance.DecreaseSpawnCount();

			yield return new WaitForSeconds(spawnTime);
		}
    }


    public GameObject[] GetWayPoints()
    {
        return waypoints;
    }

    public void StopSpawning()
    {
        spawning = false;
    }
}
