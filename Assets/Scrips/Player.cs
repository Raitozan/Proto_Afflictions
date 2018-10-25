using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public Rigidbody rb;
	Vector3 direction;
	public float speed = 5.0f;
	
	public GameObject weapon;
	public BoxCollider blade;
	public TrailRenderer bladeTrail;
	float startTime;
	float startX, startY;
	float endX, endY;
	public float attackReloadTime;
	float attackReloadTimer;
	bool attacking;

	public TrailRenderer dashTrail;
	public CapsuleCollider hitbox;
	public float dashReloadTime;
	float dashReloadTimer;
	public float dashTime;
	float dashTimer;
	float trailTimer;
	bool dashing;


	public int maxHealth;
	public int health;
	public Material mat;

	private void Start()
	{
		health = maxHealth;
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		move();
		dash();
		attack();
		changeColor();
	}

	public void move()
	{
		float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");

		Vector3 newX = -transform.position.normalized;
		newX.y = 0.0f;
		Debug.Log(newX);
		Vector3 newZ = Quaternion.Euler(0, 90, 0) * newX;

		Vector3 dir = x*newZ + z*newX;

		if (!(x == 0.0f && z == 0.0f))
		{
			rb.MoveRotation(Quaternion.LookRotation(dir));
			if(!Input.GetKey(KeyCode.Joystick1Button4))
				rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
		}
	}

	public void dash()
	{
		if (!dashing)
		{
			if (dashReloadTimer >= 0.0f)
				dashReloadTimer -= Time.deltaTime;
			else if (Input.GetAxisRaw("LeftTrigger") == 1)
			{
				trailTimer = dashTime*4;
				dashTimer = dashTime;
				dashReloadTimer = dashReloadTime;
				speed = speed * 4;
				hitbox.enabled = false;
				dashTrail.enabled = true;
				dashing = true;
			}
		}
		else
		{
			dashTimer -= Time.deltaTime;
			if (dashTimer <= 0.0f)
			{
				speed = speed / 4;
				dashing = false;
			}
		}

		if (trailTimer > 0.0f)
		{
			trailTimer -= Time.deltaTime;
			if (trailTimer <= 0.0f)
			{
				hitbox.enabled = true;
				dashTrail.enabled = false;
			}
		}
	}

	public void changeColor()
	{
		mat.color = Color.Lerp(Color.red, Color.green, (float)health / maxHealth);
	}

	public void attack()
	{
		if (!attacking)
		{
			if (attackReloadTimer >= 0.0f)
				attackReloadTimer -= Time.deltaTime;
			else if (Input.GetAxisRaw("RightTrigger") == 1)
			{
				attackReloadTimer = attackReloadTime;
				if (weapon.transform.localEulerAngles.y == 315)
				{
					startX = 20;
					startY = -45;
					endX = -20;
					endY = 45;
				}
				else
				{
					startX = -20;
					startY = 45;
					endX = 20;
					endY = -45;
				}
				startTime = Time.time;
				blade.enabled = true;
				bladeTrail.enabled = true;
				attacking = true;
			}
		}
		else
		{
			float step = Time.time - startTime;
			float x = Mathf.Lerp(startX, endX, step * 10);
			float y = Mathf.Lerp(startY, endY, step * 10);
			weapon.transform.localEulerAngles = new Vector3(x, y, 20.0f);
			if (y == endY)
			{
				blade.enabled = false;
				bladeTrail.enabled = false;
				attacking = false;
			}
		}
	}
}
