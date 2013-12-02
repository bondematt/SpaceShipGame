using UnityEngine;
using System.Collections;

public class RotateJetPack : MonoBehaviour {

	public PlayerInput playerInput;
	
	public AttachToSurface attachToSurface;

	public Transform playerCamera;

	Vector3 rotationToCameraDirection;

	Quaternion quaternionToCameraDirection;

	public float pGainX;
	public float iGainX;
	public float dGainX;
	public float jetPackTorqueX = 5f;

	public float pGainY;
	public float iGainY;
	public float dGainY;
	public float jetPackTorqueY = 5f;

	public float pGainZ;
	public float iGainZ;
	public float dGainZ;
	public float jetPackTorqueZ = 5f;

	PIDController pidControllerX = new PIDController();
	PIDController pidControllerY = new PIDController();
	PIDController pidControllerZ = new PIDController();
	
	void Start() {
		if (!playerInput) {
			playerInput = gameObject.GetComponent<PlayerInput>();
		}
		
		if (!attachToSurface) {
			attachToSurface = gameObject.GetComponent<AttachToSurface>();
		}

		if (!playerCamera) {
			playerCamera = Camera.main.transform;
		}

		pidControllerX = new PIDController(pGainX, iGainX, dGainX);
		pidControllerY = new PIDController(pGainY, iGainY, dGainY);
		pidControllerZ = new PIDController(pGainZ, iGainZ, dGainZ);
	}

	void FixedUpdate () {
		if (!attachToSurface.attached)
			RotateToCameraFacing();
	}

	void RotateToCameraFacing () {
		quaternionToCameraDirection = Quaternion.Inverse(transform.rotation) * playerCamera.rotation;

		Rotate(quaternionToCameraDirection);
	}

	public void Rotate (Quaternion quat) {
		RotateX(quat.eulerAngles.x);
		RotateY(quat.eulerAngles.y);
		RotateZ(quat.eulerAngles.z);
	}

	void RotateX (float strength) {
		if (strength > 180)
			strength = strength - 360;

		float torque = pidControllerX.PidLoop(strength, 5f);

		rigidbody.AddTorque((transform.right * torque));
	}
	
	void RotateY (float strength) {
		if (strength > 180)
			strength = strength - 360;
		
		float torque = pidControllerY.PidLoop(strength, 5f);
		
		rigidbody.AddTorque((transform.up * torque));
	}
	
	void RotateZ (float strength) {
		if (strength > 180)
			strength = strength - 360;
		
		float torque = pidControllerZ.PidLoop(strength, 5f);
		
		rigidbody.AddTorque((transform.forward * torque));
	}
}
