using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	public GameObject scene;

	bool moving;
	float startTime;
	float start;
	float end;
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!moving)
		{
			if (Input.GetAxisRaw("RightStickHorizontal") == -1)
			{
				if (scene.transform.eulerAngles.y == -180)
					start = 180;
				else
					start = scene.transform.eulerAngles.y;

				end = start - 90;

				startTime = Time.time;
				moving = true;
			}
			else if (Input.GetAxisRaw("RightStickHorizontal") == 1)
			{
				if (scene.transform.eulerAngles.y == 180)
					start = -180;
				else
					start = scene.transform.eulerAngles.y;

				end = start + 90;

				startTime = Time.time;
				moving = true;
			}
		}
		else
		{
			float step = Time.time - startTime;
			float y = Mathf.Lerp(start, end, step*2);
			scene.transform.eulerAngles = new Vector3(0.0f, y, 0.0f);
			if (y == end)
				moving = false;
		}
	}
}
