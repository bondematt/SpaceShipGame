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
	

	
	public float xRotationSpeed = 5f;


	public float moveForward = 0f;
	public float moveRight = 0f;
	public float moveUp = 0f;
	public float zRotate = 0f;
	public float yRotate = 0f;
	public float xRotate = 0f;
	public float rotationX = 0f;
	
	// Use this for initialization
	void Start () {
		Screen.lockCursor = true;

		if (!cameraControl) {
			cameraControl = gameObject.GetComponent<CameraControl>();
		}
		if (!attachToSurface) {
			attachToSurface = gameObject.GetComponent<AttachToSurface>();
		}

		if (!jetPackStabilize) {
			jetPackStabilize = gameObject.GetComponent<JetPackStabilize>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		GetInput();
	}
	
	void GetInput () {
		moveForward = Input.GetAxis("Forward/Back");
		moveRight = Input.GetAxis("Left/Right");
		moveUp = Input.GetAxis("Up/Down");
		zRotate = Input.GetAxis("Rotation");
		yRotate = Input.GetAxisRaw("Mouse X");
		xRotate = Input.GetAxisRaw("Mouse Y");

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
}
