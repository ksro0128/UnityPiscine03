using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] private float bulletSpeed = 5f;
	[SerializeField] private float bulletDamage = 1f;
	[SerializeField] private float turretDamage = 0.3f;
	[SerializeField] private Vector3 bulletDirection;

	private bool initailized = false;
	private float bulletLifeTime = 0.3f;


    public void Initialize(float bulletSpeed, float turretDamage, Vector3 bulletDirection)
	{
		this.bulletSpeed = bulletSpeed;
		this.turretDamage = turretDamage;
		this.bulletDirection = bulletDirection;
		transform.rotation = Quaternion.LookRotation(Vector3.forward, bulletDirection);
		initailized = true;
	}

    void Update()
    {
		if (!initailized)
			return ;
		transform.position += bulletDirection.normalized * bulletSpeed * Time.deltaTime;
		bulletLifeTime -= Time.deltaTime;
		if (bulletLifeTime <= 0)
			Destroy(gameObject);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			other.gameObject.GetComponent<EnemyController>().TakeDamage(bulletDamage + turretDamage);
			Destroy(gameObject);
		}
	}

	public float GetBulletDamage()
	{
		return bulletDamage;
	}
}
