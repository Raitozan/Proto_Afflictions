using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

	public int maxHealth;
	public int health;
	public Material mat;

	int minBulletNb = 1;
	int maxBulletNb = 10;

	float minBulletRadius = 0.2f;
	float maxBulletRadius = 1f;

	float minBulletSpeed = 3.0f;
	float maxBulletSpeed = 7.0f;

	float minReloadTime = 2.0f;
	float maxReloadTime = 0.5f;
	float reloadTimer;

	public GameObject bulletPrefab;

	bool rotateClockwise;
	float minRotateTime = 1f;
	float maxRotateTime = 5f;
	float rotateTimer;

	float rotateSpeed = 50f;

	// Use this for initialization
	void Start () {
		health = maxHealth;

		reloadTimer = Random.Range(maxReloadTime - 0.5f, maxReloadTime + 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		reloadTimer -= Time.deltaTime;
		if(reloadTimer <= 0.0f)
		{
			int bulletNb = GetIntParameterScaling(minBulletNb, maxBulletNb);
			for (int i=0; i<bulletNb; i++)
			{
				Vector3 bPos = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f));
				bPos = bPos.normalized * 4.5f;
				bPos.y = 0.75f;
				GameObject bullet = Instantiate(bulletPrefab, bPos, Quaternion.identity, GameObject.Find("Scene").transform);

				float bulletRadius = GetFloatParameterScaling(minBulletRadius, maxBulletRadius);
				bullet.transform.localScale = new Vector3(bulletRadius, bulletRadius, bulletRadius);

				float bulletSpeed = GetFloatParameterScaling(minBulletSpeed, maxBulletSpeed);
				bullet.GetComponent<Bullet>().speed = bulletSpeed;
			}
			float reloadTime = GetFloatParameterScaling(minReloadTime, maxReloadTime);
			reloadTimer = Random.Range(reloadTime - 0.25f, reloadTime + 0.25f);
		}
		rotateTimer -= Time.deltaTime;
		if(rotateTimer <= 0.0f)
		{
			if (rotateClockwise)
				rotateClockwise = false;
			else
				rotateClockwise = true;
			rotateTimer = Random.Range(minRotateTime, maxRotateTime);
		}
		Rotate();
		changeColor();
	}

	public void changeColor()
	{
		mat.color = Color.Lerp(Color.red, Color.green, (float)health / maxHealth);
	}

	public float GetFloatParameterScaling(float min, float max)
	{
		return (float)((((min - max) * health) / maxHealth) + max);
	}
	public int GetIntParameterScaling(int min, int max)
	{
		return (int)((((min - max) * health) / maxHealth) + max);
	}

	public void Rotate()
	{
		Vector3 rotation = Vector3.up;
		if (!rotateClockwise)
			rotation = -rotation;

		transform.Rotate(rotation * rotateSpeed * Time.deltaTime);
	}
}
