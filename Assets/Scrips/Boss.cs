using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

	public int maxHealth;
	public int health;
	public Material mat;

	// Use this for initialization
	void Start () {
		health = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		changeColor();
	}

	public void changeColor()
	{
		mat.color = Color.Lerp(Color.red, Color.green, (float)health / maxHealth);
	}
}
