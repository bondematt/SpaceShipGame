using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
	
	public Transform playerCamera;

	public PlayerInput playerInput;

	public AttachToSurface attachToSurface;

	public Transform playerTransform;

	public Transform cameraReference;
	
	public Vector3 cameraPositionFirstPerson = new Vector3(0,0.6f,0);
	public Vector3 cameraRotationFirstPerson = new Vector3(0,0,0);
	public Vector3 cameraPositionThirdPerson = new Vector3(0,1.4f,-3.3f);
	public Vector3 cameraRotationThirdPerson = new Vector3(15,0,0);
	Vector3 cameraOffset;

	public float maxHeadAngle = 90;

	public float zRotateModifier = .7f;

	Vector3 cameraPosition;
	Quaternion cameraRotation;
	
	int cameraMode = 1;

	public float rotationX = 0f;

	void Start() {
		if (!playerInput) {
			playerInput = gameObject.GetComponent<PlayerInput>();
		}

		if (!playerCamera) {
			playerCamera = Camera.main.transform;
		}

		if (!attachToSurface) {
			attachToSurface = gameObject.GetComponent<AttachToSurface>();
		}

		if (cameraMode == 1)
			playerCamera.parent = null;
	}

	void Update() {
		if (cameraMode == 0) {
			cameraOffset = cameraPositionFirstPerson;
		} else {
			cameraOffset = cameraPositionThirdPerson;
		}

		cameraReference.position = playerTransform.position;
		if (attachToSurface.attached) {
			playerCamera.parent = playerTransform;

			playerCamera.rotation = playerTransform.rotation;

			if (cameraMode == 0) {
				playerCamera.position = playerTransform.TransformPoint(cameraOffset);
			} else {
				playerCamera.position = cameraReference.TransformPoint(cameraOffset);
			}

			if (cameraMode == 0){
				rotationX += (-playerInput.xRotate * StartGameMenu.mouseSensitivity);
				rotationX = Mathf.Clamp (rotationX, -maxHeadAngle, maxHeadAngle);

				RotateCameraXFirstPerson(rotationX);
			}

		} else if (!attachToSurface.attached) {
			playerCamera.parent = null;

			if (cameraMode == 0) {
				playerCamera.position = playerTransform.TransformPoint(cameraOffset);
			} else {
				playerCamera.position = cameraReference.TransformPoint(cameraOffset);
			}

			RotateCameraXThirdPerson(playerInput.xRotate);
			RotateCameraYThirdPerson(playerInput.yRotate);
			RotateCameraZThirdPerson(playerInput.zRotate);
		}
	}
	
	void FirstPersonCamera () {
		playerCamera.localPosition = cameraPositionFirstPerson;
	}
		
	void ThirdPersonCamera () {
		playerCamera.localPosition = cameraPositionThirdPerson;
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
	
	void RotateCameraXFirstPerson (float angle) {
		if (cameraMode == 0)
			playerCamera.localRotation = playerCamera.localRotation * Quaternion.Euler(angle, 0, 0);
	}

	void RotateCameraXThirdPerson (float angle) {
		playerCamera.RotateAround(cameraReference.position, cameraReference.right, angle);
	}

	void RotateCameraYThirdPerson (float angle) {
		playerCamera.RotateAround(cameraReference.position, cameraReference.up, angle);
	}

	void RotateCameraZThirdPerson (float angle) {
		playerCamera.RotateAround(cameraReference.position, -cameraReference.forward, angle * zRotateModifier);
	}

	void CameraFollow () {

	}
}
