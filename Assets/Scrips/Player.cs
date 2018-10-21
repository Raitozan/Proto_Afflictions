using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public Rigidbody rb;
	Vector3 direction;
	public float speed = 5.0f;

	bool attacking;
	public GameObject weapon;
	float startTime;
	float start;
	float end;

	public float dashReloadTime;
	float dashReloadTimer;
	public float dashTime;
	float dashTimer;
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
		changeColor();
		if (Input.GetAxisRaw("RightTrigger") == 1 || attacking)
			attack();
	}

	public void move()
	{
		float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");

		if (!(x == 0.0f && z == 0.0f))
		{
			rb.MoveRotation(Quaternion.LookRotation(new Vector3(x, 0.0f, z)));
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
				dashTimer = dashTime;
				dashReloadTimer = dashReloadTime;
				speed = speed * 4;
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
	}

	public void changeColor()
	{
		mat.color = Color.Lerp(Color.red, Color.green, (float)health / maxHealth);
	}

	public void attack()
	{
		if (!attacking)
		{
			if (weapon.transform.localEulerAngles.y == 315)
			{
				start = -45;
				end = 45;
			}
			else
			{
				start = 45;
				end = -45;
			}
			startTime = Time.time;
			weapon.SetActive(true);
			attacking = true;
		}
		else
		{
			float step = Time.time - startTime;
			float y = Mathf.Lerp(start, end, step*10);
			weapon.transform.localEulerAngles = new Vector3(0.0f, y, 0.0f);
			if (y == end)
			{
				weapon.SetActive(false);
				attacking = false;
			}
		}
	}
}
