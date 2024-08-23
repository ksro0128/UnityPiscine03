using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemyController : MonoBehaviour
{
	[SerializeField] private float speed = 5f;
	[SerializeField] private float health = 3f;
	[SerializeField] private int damage = 1;
	[SerializeField] private int money = 10;

	private GameObject[] waypoints;
	private int currentWaypointTarget = 0;

	private bool isInitialized = false;

    void Start()
    {

    }

    void Update()
    {
		if (!isInitialized)
			return;
		if (Vector3.Distance(transform.position, waypoints[currentWaypointTarget].transform.position) < 0.1f)
		{
			currentWaypointTarget++;
			if (currentWaypointTarget >= waypoints.Length)
			{
				Destroy(gameObject);
				StageManager.instance.TakeDamage(damage);
				StageManager.instance.DecreaseEnemyCount();
			}
		}
		else
		{
			transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointTarget].transform.position, speed * Time.deltaTime);
		}
	}

	public void InitWayPoints(GameObject[] waypoints)
	{
		this.waypoints = waypoints;
		isInitialized = true;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Base")
		{
			StageManager.instance.TakeDamage(damage);
			StageManager.instance.DecreaseEnemyCount();
			Destroy(gameObject);
		}
    }

	public float GetDamage()
	{
		return damage;
	}

	public float GetSpeed()
	{
		return speed;
	}

	public Vector3 GetCurrentWaypointPosition()
	{
		return waypoints[currentWaypointTarget].transform.position;
	}

	public void TakeDamage(float damage)
	{
		if (health <= 0)
			return;
		health -= damage;
		if (health <= 0)
		{
			StageManager.instance.AddMoney(money);
			StageManager.instance.DecreaseEnemyCount();
			Destroy(gameObject);
		}
	}

	public GameObject[] GetWaypoints()
	{
		return waypoints;
	}

	public void SetCurrentWaypointTarget(int target)
	{
		currentWaypointTarget = target;
	}

	public int GetCurrentWaypointTarget()
	{
		return currentWaypointTarget;
	}

}
