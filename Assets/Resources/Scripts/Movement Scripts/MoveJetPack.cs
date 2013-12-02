using UnityEngine;
using System.Collections;

public class MoveJetPack : MonoBehaviour {
	public PlayerInput playerInput;
	
	public AttachToSurface attachToSurface;
	
	public float jetPackForce = 5f;
	
	void Start() {
		if (!playerInput) {
			playerInput = gameObject.GetComponent<PlayerInput>();
		}
		
		if (!attachToSurface) {
			attachToSurface = gameObject.GetComponent<AttachToSurface>();
		}
	}
	
	void Update(){
		if (!attachToSurface.attached)
			MoveDirection(new Vector3(playerInput.moveRight, playerInput.moveUp, playerInput.moveForward), jetPackForce);
	}

	public void MoveDirection (Vector3 direction,  float jetPackForce) 
	{
		float strength = direction.magnitude;
		strength = Mathf.Clamp(strength, -1f, 1f);
		strength = strength * jetPackForce;
		direction = direction.normalized;
		MoveX (direction.x * strength);
		MoveY (direction.y * strength);
		MoveZ (direction.z * strength);
	}
	
	void MoveX (float strength) {
		rigidbody.AddForce(transform.right * strength * Time.deltaTime);
	}
	
	void MoveY (float strength) {
		rigidbody.AddForce(transform.up * strength * Time.deltaTime);
	}
	
	void MoveZ (float strength) {
		rigidbody.AddForce(transform.forward * strength * Time.deltaTime);
	}
}
