using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner2 : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject enemyPrefab2;
	[SerializeField] private float spawnTime = 2f;
	[SerializeField] private GameObject[] waypoints;

	private int enemyType = 0;
	private int enemyCount = 0;
	private bool spawning = true;

	void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (spawning)
        {
            if (StageManager.instance.GetSpawnCount() <= 0)
            {
                StopSpawning();
                yield break;
            }

            if (enemyCount > 40)
            {
                spawnTime = 0.6f;
            }
            else if (enemyCount > 20)
            {
                enemyType = 1;
            }

            enemyCount++;
            GameObject e;
            if (enemyType == 0)
            {
                e = Instantiate(enemyPrefab, transform.position, transform.rotation);
            }
            else
            {
                e = Instantiate(enemyPrefab2, transform.position, transform.rotation);
            }
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
		CancelInvoke("SpawnEnemy");
	}
}
