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
	public RotationAxes axes = RotationAxes.MouseX;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;

	public float minimumX = -360F;
	public float maximumX = 360F;

	float minimumY = 20F;
	float maximumY = 60F;

	float rotationY = 0F;
    bool canLook;

	void Update () {
        if (canLook) {
            if (axes == RotationAxes.MouseX) {
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
            } else {
				transform.Rotate(Input.GetAxis("Mouse Y") * -sensitivityY, 0, 0);

				//Vector3 tempTransform = transform.localEulerAngles;
				if (transform.localEulerAngles.x <= minimumY) {
					transform.localEulerAngles = new Vector3 (minimumY, -115.7f, -193.4f);
				} else if (transform.localEulerAngles.x >= maximumY) {
					transform.localEulerAngles = new Vector3 (maximumY, -135.18f, -206.9f);
				}

				//transform.localEulerAngles = new Vector3(transform.localEulerAngles.y, transform.localEulerAngles.y, 0);
				//ransform.localEulerAngles = tempTransform;
            }
        }
	}
	
	void Awake() {
        canLook = true;
		// Make the rigid body not change rotation
		if (GetComponent<Rigidbody> ()) {
			GetComponent<Rigidbody> ().freezeRotation = true;
		}
	}

	public void changeCanLook(bool change) {
		canLook = change;
	}
}