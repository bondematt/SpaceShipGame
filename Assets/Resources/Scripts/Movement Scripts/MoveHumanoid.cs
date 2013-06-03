using UnityEngine;
using System.Collections;

public class MoveHumanoid : MonoBehaviour {
	
	bool AttachedToSurface = false; //Will be set when player attaches/detaches from ships
	
	public bool jetPackStabilize = true; //player toggle
	
	public float jetPackForce = 50f; //later these will be item dependant
	
	public float jetPackTorque = 50f; 
	
	public float jetPackStabilizeForce = 5f;
	
	public float jetPackStabilizeTorque = 1f;
	
	public float movementSpeed = 5f;
	
	public float mouseSensitivity = 5f; //will be set by player later
	
	public float rotationSensitivity = .25f; //Used to keep rotation axis in line with mouse axis

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (jetPackStabilize) {
			JetPackStabilize();
		}
	}
		
	//Public methods that are called from PlayerInput or other movement logic scripts
	//move by local axis
	public void MoveX (float strength) {
		if (AttachedToSurface) {
			MoveXAttached (strength);
		} else {
			MoveXJetpack (strength);
		}
	}
	
	public void MoveY (float strength) {
		if (!AttachedToSurface) {
			MoveYJetpack (strength);
		}
	}
	
	public void MoveZ (float strength) {
		if (AttachedToSurface) {
			MoveZAttached (strength);
		} else {
			MoveZJetpack (strength);
		}
	}
	
	//move in direction
	public void MoveDirection (Vector3 direction) {
		
		if (AttachedToSurface) {
			direction.y = 0;
			float strength = direction.magnitude;
			MoveXAttached (direction.normalized.x * strength);
			MoveZAttached (direction.normalized.z * strength);
		} else {
			float strength = direction.magnitude;
			MoveXJetpack (direction.normalized.x * strength);
			MoveYJetpack (direction.normalized.y * strength);
			MoveZJetpack (direction.normalized.z * strength);
		}
	}
	
	
	public void RotateX (float strength) {
		if (AttachedToSurface) {
			RotateXAttached (strength);
		} else {
			RotateXJetpack (strength);
		}
	}
	
	public void RotateY (float strength) {
		if (AttachedToSurface) {
			RotateYAttached (strength);
		} else {
			RotateYJetpack (strength);
		}
	}
	
	public void RotateZ (float strength) {
		if (AttachedToSurface) {
			RotateZAttached (strength);
		} else {
			RotateZJetpack (strength);
		}
	}
	
	public void ToggleJetpackStabilize () {
		if (jetPackStabilize) {
			jetPackStabilize = false;	
		} else {
			jetPackStabilize = true;	
		}
	}
	
	public void Attach() {
		AttachedToSurface = true;
	}
	
	public void Detach() {
		AttachedToSurface = false;
	}
	
	//Private Methods that handle the actual movement
	
	//Movement while a child of an object, translates accross flat ground.
	
	void MoveXAttached (float strength) {
		transform.Translate(transform.right * strength * Time.deltaTime * movementSpeed, Space.World);
	}
	
	void MoveZAttached (float strength) {
		transform.Translate(transform.forward * strength * Time.deltaTime * movementSpeed, Space.World);
	}
	
	void RotateXAttached (float strength) {
		
	}
	
	void RotateYAttached (float strength) {
		
	}
	
	void RotateZAttached (float strength) {
		
	}
	
	//movement while humanoid is a rigidbody with a jetpack attached, without one there is no movement possible.
	
	void MoveXJetpack (float strength) {
		rigidbody.AddForce(transform.right * strength * Time.deltaTime * jetPackForce);
	}
	
	void MoveYJetpack (float strength) {
		rigidbody.AddForce(transform.up * strength * Time.deltaTime * jetPackForce);
	}
	
	void MoveZJetpack (float strength) {
		rigidbody.AddForce(transform.forward * strength * Time.deltaTime * jetPackForce);
	}
	
	void RotateXJetpack (float strength) {
		rigidbody.AddTorque(-transform.right * strength * Time.deltaTime * jetPackTorque * mouseSensitivity);
	}
	
	void RotateYJetpack (float strength) {
		rigidbody.AddTorque(transform.up * strength * Time.deltaTime * jetPackTorque * mouseSensitivity);
	}
	
	void RotateZJetpack (float strength) {
		rigidbody.AddTorque(-transform.forward * strength * Time.deltaTime * jetPackTorque * rotationSensitivity);
	}
	
	void JetPackStabilize () {
		rigidbody.AddForce(-rigidbody.velocity.normalized * jetPackStabilizeForce * Time.deltaTime);
		if (rigidbody.angularVelocity.magnitude > 1) {	
			rigidbody.AddTorque(-rigidbody.angularVelocity.normalized * jetPackStabilizeTorque * Time.deltaTime);
		} else {
			rigidbody.AddTorque(-rigidbody.angularVelocity * jetPackStabilizeTorque * Time.deltaTime);
		}
	}
}
