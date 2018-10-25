using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	public float length;
	public float height;

	public Transform playerPos;
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 dir = new Vector3(playerPos.position.x, 0.0f, playerPos.position.z);
		dir = dir.normalized;
		Vector3 nPos = dir * length;
		nPos.y = height;

		transform.position = nPos;
		transform.LookAt(0.5f*playerPos.position);
	}
}
