using UnityEngine;
using System.Collections;

/// MouseLook rotates the transform based on the mouse delta.
/// Minimum and Maximum values can be used to constrain the possible rotation

/// To make an FPS style character:
/// - Create a capsule.
/// - Add the MouseLook script to the capsule.
///   -> Set the mouse look to use LookX. (You want to only turn character but not tilt it)
/// - Add FPSInputController script to the capsule
///   -> A CharacterMotor and a CharacterController component will be automatically added.

/// - Create a camera. Make the camera a child of the capsule. Reset it's transform.
/// - Add a MouseLook script to the camera.
///   -> Set the mouse look to use LookY. (You want the camera to tilt up and down like a head. The character already turns.)
[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLook : MonoBehaviour {
	public enum RotationAxes { MouseX, MouseY }
	public RotationAxes axes;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;

	float minimumX = -360F;
	float maximumX = 360F;

	public float minimumY = 20F;
	public float maximumY = 60F;

	float rotationY = 37.6F;
    bool canLook;

	void Update () {
        if (canLook) {
            if (axes == RotationAxes.MouseX) {
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
            } else {
				rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
				rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

				transform.localEulerAngles = new Vector3(rotationY, transform.localEulerAngles.y, transform.localEulerAngles.z);
            }
        }
	}
	
	void Awake() {
		if (gameObject.tag == "CrossbowCamera") {
			canLook = true;
		}
		// Make the rigid body not change rotation
		if (GetComponent<Rigidbody>()) {
			GetComponent<Rigidbody>().freezeRotation = true;
		}
	}

	public void changeCanLook(bool change) {
		canLook = change;
	}
}