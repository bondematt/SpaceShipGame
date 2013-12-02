using UnityEngine;
using System.Collections;

public class MoveOnSurface : MonoBehaviour {

	public PlayerInput playerInput;

	public AttachToSurface attachToSurface;

	public float movementSpeed = 5f;

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
			MoveDirection(new Vector3(playerInput.moveRight, 0, playerInput.moveForward), movementSpeed);
	}
	
	public void MoveDirection (Vector3 direction, float movementSpeed) {
		direction.y = 0;
		float strength = direction.magnitude;
		strength = Mathf.Clamp(strength, -1f, 1f);
		strength = strength * movementSpeed;
		direction = direction.normalized;
		MoveX(direction.x * strength);
		MoveZ(direction.z * strength);
	}
	
	void MoveX (float strength) {
		transform.Translate(transform.right * strength * Time.deltaTime, Space.World);
	}
	
	void MoveZ (float strength) {
		transform.Translate(transform.forward * strength * Time.deltaTime, Space.World);
	}

}
