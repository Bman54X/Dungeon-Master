using UnityEngine;

public class PlatformMover : MonoBehaviour {
	public Transform start, end;
	Transform target;
	bool startIsTarget;
    public float waitTime;
    float counter, speed;
	bool activated;
	public bool backAndForth;

	// Use this for initialization
	void Awake() {
		activated = true;
		target = start;
		startIsTarget = true;
        speed = 5.0f;
		counter = 0.0f;
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		if (activated) {
			if (backAndForth) {
				transform.position = Vector3.MoveTowards (transform.position, target.position, speed * Time.deltaTime);
				float dist = Mathf.Abs(Vector3.Distance (target.position, transform.position));
				if (dist <= 0.05f) {
					counter += Time.deltaTime;
					if (counter >= waitTime) {
						changeTarget();
					}
				}
			} else {
				transform.position = Vector3.MoveTowards (transform.position, end.position, speed * Time.deltaTime);
			}
		}
	}

	void changeTarget() {
		counter = 0.0f;
		if (startIsTarget) {
			target = end;
		} else {
			target = start;
		}
		startIsTarget = !startIsTarget;

		transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
	}

	public void setActivation(bool active) {
		activated = active;
	}
}