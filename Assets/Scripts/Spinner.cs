using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour {
	float timeElapsed = 0.0f;
	const float changeDirection = 1.0f;
	float direction = 0.25f;

	// Update is called once per frame
	void Update () {
		timeElapsed += Time.deltaTime;
		if (timeElapsed >= changeDirection) {
			timeElapsed = 0;
			direction *= -1;
		}

		transform.Translate(Vector3.up * Time.deltaTime * direction, Space.World);
		transform.Rotate (new Vector3 (0, 40, 0) * Time.deltaTime);
	}
}