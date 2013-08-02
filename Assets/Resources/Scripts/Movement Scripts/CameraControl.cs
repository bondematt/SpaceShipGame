using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
	
	public Transform playerCamera;
	
	public Vector3 cameraPositionFirstPerson = new Vector3(0,0.6f,0);
	public Vector3 cameraRotationFirstPerson = new Vector3(0,0,0);
	public Vector3 cameraPositionThirdPerson = new Vector3(0,1.4f,-3.3f);
	public Vector3 cameraRotationThirdPerson = new Vector3(15,0,0);
	
	int cameraMode = 1;
	
	void FirstPersonCamera () {
		playerCamera.localPosition = cameraPositionFirstPerson;
		playerCamera.localRotation = Quaternion.Euler(cameraRotationFirstPerson);
	}
		
	void ThirdPersonCamera () {
		playerCamera.localPosition = cameraPositionThirdPerson;
		playerCamera.localRotation = Quaternion.Euler(cameraRotationThirdPerson);
	}
	
	public void ToggleCameraMode () {
		if (cameraMode == 0) {
			cameraMode = 1;
			ThirdPersonCamera();
		} else if (cameraMode == 1) {
			cameraMode = 0;
			FirstPersonCamera();
		}
	}
	
	public void RotateCamera (float angle) {
		if (cameraMode == 0)
			playerCamera.localRotation = Quaternion.Euler(angle, playerCamera.localRotation.y, playerCamera.localRotation.z);
	}
}
