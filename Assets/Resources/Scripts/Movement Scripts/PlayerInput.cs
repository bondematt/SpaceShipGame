using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {
	
	MoveHumanoid movePlayer;
	AttachToSurface attachToSurface;
	
	float moveForward = 0f;
	float moveRight = 0f;
	float moveUp = 0f;
	float zRotate = 0f;
	float yRotate = 0f;
	float xRotate = 0f;
	
	// Use this for initialization
	void Start () {
		movePlayer = transform.root.GetComponentInChildren<MoveHumanoid>();
		attachToSurface = transform.root.GetComponentInChildren<AttachToSurface>();
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
		
		//}
		/*if(Input.GetAxis("Rotation") != 0) {
			movePlayer.RotateZ(Input.GetAxis("Rotation"));
		}
		if(Input.GetAxis("Mouse X") != 0) {
			movePlayer.RotateY(Input.GetAxis("Mouse X"));
		}
		if(Input.GetAxis("Mouse Y") != 0) {
			movePlayer.RotateX(Input.GetAxis("Mouse Y"));
		}*/
		if(Input.GetButtonUp("Jetpack Stabilizer Toggle")) {
			movePlayer.ToggleJetpackStabilize();
		}
		if(Input.GetButtonUp("Magnetic Boots Toggle")) {
			attachToSurface.ToggleAttach();
		}
	}
	
	void ApplyFixedInput() {
		movePlayer.MoveDirection(new Vector3(moveRight, moveUp, moveForward));
		movePlayer.RotateZ(zRotate);
		movePlayer.RotateY(yRotate);
		movePlayer.RotateX(xRotate);
	}
}
