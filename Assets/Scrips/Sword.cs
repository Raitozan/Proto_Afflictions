using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Boss"))
		{
			Boss b = other.GetComponent<Boss>();
			b.health -= 1;
		}
		if (other.CompareTag("BossWeakpoint"))
		{
			Boss b = other.GetComponentInParent<Boss>();
			b.health -= 5;
		}
	}
}
