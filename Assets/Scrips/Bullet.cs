using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float speed;
	Vector3 direction;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		direction = transform.position;
		direction.y = 0.0f;
		direction = direction.normalized;
		gameObject.GetComponent<Rigidbody>().MovePosition(transform.position + direction * speed * Time.deltaTime);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			Player p = other.GetComponent<Player>();
			p.health -= 1;
			Destroy(gameObject);
		}
		else if(other.CompareTag("Wall"))
			Destroy(gameObject);
	}
}
