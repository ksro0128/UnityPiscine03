using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShortcutManagement;
using UnityEngine;
using UnityEngine.UIElements;

public class Turret : MonoBehaviour
{
	[SerializeField] private float detectionRadius = 5f;
	[SerializeField] private float fireRate = 1f;
	[SerializeField] private float damage = 0.3f;
	[SerializeField] private GameObject bulletPrefab;
	[SerializeField] private float bulletSpeed = 50f;
	[SerializeField] private float rotationSpeed = 10f;
	[SerializeField] private float coolTime = 2f;

	private GameObject target;
	private Vector3 targetPredictedPosition;
	private Vector3 bulletDirection;
	private float fireTimer = 0f;
	[SerializeField] private bool active = false;

    void Update()
    {
		if (!active)
			return;

		UpdateTarget();
		if (target == null)
			return;
		if (fireTimer <= 0f)
		{
			Shoot();
			fireTimer = 1f / fireRate;
		}
		fireTimer -= Time.deltaTime;
    }

	private void UpdateTarget()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		if (enemies.Length == 0)
		{
			target = null;
			return;
		}
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;

		foreach (GameObject enemy in enemies)
		{
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			if (distanceToEnemy < shortestDistance)
			{
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}

		if (nearestEnemy != null)
		{
			RotateToWard(nearestEnemy.transform.position);
		}

		if (shortestDistance <= detectionRadius)
		{
			target = nearestEnemy;
			targetPredictedPosition = GetPredictedPosition(target);
			bulletDirection = targetPredictedPosition - transform.position;
		}
		else
			target = null;
	}

	private void RotateToWard(Vector3 targetPosition)
	{
		Vector3 direction = targetPosition - transform.position;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
	}

	private void Shoot()
	{
		GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
		Bullet bulletComponent = bullet.GetComponent<Bullet>();
		bulletComponent.Initialize(bulletSpeed, damage, bulletDirection);
	}

	private Vector3 GetPredictedPosition(GameObject target)
	{
		EnemyController enemyController = target.GetComponent<EnemyController>();
		if (enemyController == null)
			return target.transform.position;
		
		Vector3 targetPosition = target.transform.position;
		Vector3 targetWaypoint = enemyController.GetCurrentWaypointPosition();
		float speed = enemyController.GetSpeed();
		Vector3 direction = (targetWaypoint - targetPosition).normalized;
		float distance = Vector3.Distance(transform.position, targetPosition);
		float travelTime = distance / bulletSpeed;

		return targetPosition + direction * travelTime * speed;
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, detectionRadius);
	}

	public void SetActive(bool active)
	{
		this.active = active;
	}

	public Vector3 GetDetectionRadius()
	{
		return new Vector3(detectionRadius, detectionRadius, 0);
	}

	public float GetFireRate()
	{
		return fireRate;
	}

	public float GetDamage()
	{
		return damage;
	}

	public float GetBulletDamage()
	{
		return bulletPrefab.GetComponent<Bullet>().GetBulletDamage();
	}

	public float GetRange()
	{
		return detectionRadius;
	}

	public float GetCoolTime()
	{
		return coolTime;
	}
}