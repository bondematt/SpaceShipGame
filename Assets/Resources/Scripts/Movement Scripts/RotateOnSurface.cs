using UnityEngine;
using System.Collections;

public class RotateOnSurface : MonoBehaviour {
	
	public PlayerInput playerInput;
	
	public AttachToSurface attachToSurface;

	void Start() {
		if (!playerInput) {
			playerInput = gameObject.GetComponent<PlayerInput>();
		}
		
		if (!attachToSurface) {
			attachToSurface = gameObject.GetComponent<AttachToSurface>();
		}
	}
	
	void Update(){
		if (attachToSurface.attached)
			RotateY(playerInput.yRotate);
	}

	void RotateY (float angle) {
		transform.RotateAround(transform.position, transform.up, angle * StartGameMenu.mouseSensitivity);
	}
}
