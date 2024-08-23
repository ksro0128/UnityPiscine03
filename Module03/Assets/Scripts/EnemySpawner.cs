using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] private GameObject enemyPrefab;
	[SerializeField] private float spawnTime = 2f;
	[SerializeField] private GameObject[] waypoints;

    void Start()
    {
        InvokeRepeating("SpawnEnemy", spawnTime, spawnTime);

    }

	void SpawnEnemy()
	{
		if (StageManager.instance.GetSpawnCount() <= 0)
		{
			StopSpawning();
			return;
		}
		GameObject e = Instantiate(enemyPrefab, transform.position, transform.rotation);
		e.GetComponent<EnemyController>().InitWayPoints(waypoints);
		StageManager.instance.DecreaseSpawnCount();
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
