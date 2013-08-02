using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {
	
	public AttachToSurface attachToSurface;
	public CameraControl cameraControl;
	public MoveOnSurface moveOnSurface;
	public MoveJetPack moveJetPack;
	public RotateOnSurface rotateOnSurface;
	public RotateJetPack rotateJetPack;
	public JetPackStabilize jetPackStabilize;
	
	public float movementSpeed = 5f;
	
	public float jetPackForce = 5f;
	
	public float jetPackTorqueX = 1f;
	
	public float jetPackTorqueY = 1f;
	
	public float jetPackTorqueZ = 1f;
	
	public float xRotationSpeed = 5f;
	
	public float xRotationSpeedAttached = 30f;
	
	public float yRotationSpeed = 2f;

	float moveForward = 0f;
	float moveRight = 0f;
	float moveUp = 0f;
	float zRotate = 0f;
	float yRotate = 0f;
	float xRotate = 0f;
	
	float rotationX = 0f;
	
	// Use this for initialization
	void Start () {
		Screen.lockCursor = true;
	}
	
	// Update is called once per frame
	void Update () {
		GetInput();
	}
	
	void FixedUpdate () {
		ApplyFixedInput();
	}
	
	void GetInput () {
		//if(Input.GetButton("Forward/Back") || Input.GetButton("Left/Right") || Input.GetButton("Up/Down")) {
			//movePlayer.MoveDirection(new Vector3(Input.GetAxis("Left/Right"), Input.GetAxis("Up/Down"), Input.GetAxis("Forward/Back")));
		moveForward = Input.GetAxis("Forward/Back");
		moveRight = Input.GetAxis("Left/Right");
		moveUp = Input.GetAxis("Up/Down");
		zRotate = Input.GetAxis("Rotation");
		yRotate = Input.GetAxis("Mouse X");
		xRotate = Input.GetAxis("Mouse Y");
		
		rotationX += (-xRotate * Time.deltaTime * xRotationSpeedAttached * StartGameMenu.mouseSensitivity);
  		rotationX = Mathf.Clamp (rotationX, -60, 60);
		
		//}
		if(Input.GetButtonUp("Jetpack Stabilizer Toggle")) {
			jetPackStabilize.ToggleJetpackStabilize();
		}
		if(Input.GetButtonUp("Magnetic Boots Toggle")) {
			attachToSurface.ToggleAttach();
		}
		if(Input.GetButtonUp("Camera Toggle")) {
			cameraControl.ToggleCameraMode();
		}
		
		//Toggle opening/closing of menu with escape
		if(Input.GetKeyUp(KeyCode.Escape)) {
			if (StartGameMenu.menuLevel == -1) {
				StartGameMenu.OpenMenu(0);
			} else {
				StartGameMenu.OpenMenu(-1);
			}
		}
	}
	
	void ApplyFixedInput() {
		if (!attachToSurface.attaching) {
			if (attachToSurface.attached) {
				if (moveOnSurface != null)
					moveOnSurface.MoveDirection(new Vector3(moveRight, 0, moveForward), movementSpeed);
				
				if (rotateOnSurface != null)
					rotateOnSurface.Rotate(yRotate * yRotationSpeed);
				
				if (cameraControl != null)
					cameraControl.RotateCamera(rotationX);
				
			} else if (!attachToSurface.attached) {
				if (moveJetPack != null)
					moveJetPack.MoveDirection(new Vector3(moveRight, moveUp, moveForward) * StartGameMenu.mouseSensitivity, jetPackForce);
				
				if (rotateJetPack != null)
					rotateJetPack.Rotate(new Vector3(xRotate, yRotate, zRotate) * StartGameMenu.mouseSensitivity, jetPackTorqueX, jetPackTorqueY, jetPackTorqueZ);
				
				if (moveJetPack != null && rotateJetPack != null && jetPackStabilize) {
					
				}
			}
		}
	}
	

}
