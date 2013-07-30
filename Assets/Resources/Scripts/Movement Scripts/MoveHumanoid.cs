using UnityEngine;
using System.Collections;

public class MoveHumanoid : MonoBehaviour {
	
	bool attachedToSurface = false; //Will be set when player attaches/detaches from ships
	
	bool attaching = false;
	
	public bool jetPackStabilize = true; //player toggle
	
	public float jetPackForce = 50f; //later these will be item dependant
	
	public float jetPackTorque = 50f; 
	
	public float jetPackStabilizeForce = 5f;
	
	public float jetPackStabilizeTorque = 1f;
	
	public float movementSpeed = 5f;
	
	public float mouseSensitivityX = 5f;
	
	public float mouseSensitivityY = 2f;
	
	public float rotationSensitivity = .25f; //Used to keep rotation axis in line with mouse axis
	
	public Transform camera;
	
	float rotationX = 0f;
	
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!attaching && !attachedToSurface) {
			if (jetPackStabilize) {
				JetPackStabilize();
			}
		}
	}
		
	//Public methods that are called from PlayerInput or other movement logic scripts
	//move by local axis
	public void MoveX (float strength) {
		if (attachedToSurface) {
			MoveXAttached (strength);
		} else {
			MoveXJetpack (strength);
		}
	}
	
	public void MoveY (float strength) {
		if (!attachedToSurface) {
			MoveYJetpack (strength);
		}
	}
	
	public void MoveZ (float strength) {
		if (attachedToSurface) {
			MoveZAttached (strength);
		} else {
			MoveZJetpack (strength);
		}
	}
	
	//move in direction
	public void MoveDirection (Vector3 direction) {
		if (!attaching) {
			if (attachedToSurface) {
				direction.y = 0;
				float strength = direction.magnitude;
				strength = Mathf.Clamp(strength, -1f, 1f);
				MoveXAttached (direction.normalized.x * strength);
				MoveZAttached (direction.normalized.z * strength);
			} else {
				float strength = direction.magnitude;
				strength = Mathf.Clamp(strength, -1f, 1f);
				MoveXJetpack (direction.normalized.x * strength);
				MoveYJetpack (direction.normalized.y * strength);
				MoveZJetpack (direction.normalized.z * strength);
			}
		}
	}
	
	
	public void RotateX (float strength) {
		if (!attaching) {
			if (attachedToSurface) {
				RotateXAttached (strength);
			} else {
				RotateXJetpack (strength);
			}
		}
	}
	
	public void RotateY (float strength) {
		if (!attaching) {
			if (attachedToSurface) {
				RotateYAttached (strength);
			} else {
				RotateYJetpack (strength);
			}
		}
	}
	
	public void RotateZ (float strength) {
		if (!attaching) {
			if (attachedToSurface) {
				//RotateZAttached (strength);
			} else {
				RotateZJetpack (strength);
			}
		}
	}
	
	public void ToggleJetpackStabilize () {
		if (jetPackStabilize) {
			jetPackStabilize = false;	
			Debug.Log("Jet Pack Stabilization Off");
		} else {
			jetPackStabilize = true;	
			Debug.Log("Jet Pack Stabilization On");
		}
	}
	
	public void Attached(bool state) {
		attachedToSurface = state;
		
		//reset camera position when detaching
		if (attachedToSurface == false) {
			rotationX = 0f;
			camera.localEulerAngles = new Vector3(-rotationX, camera.localEulerAngles.y, camera.localEulerAngles.z);
		}
	}
	
	public void Attaching(bool state) {
		attaching = state;
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
		rotationX += (strength * Time.deltaTime * mouseSensitivityX * StartGameMenu.mouseSensitivity);
  		rotationX = Mathf.Clamp (rotationX, -60, 60);
 
    	camera.localEulerAngles = new Vector3(-rotationX, camera.localEulerAngles.y, camera.localEulerAngles.z);
	}
	
	void RotateYAttached (float strength) {
		transform.RotateAround(transform.up, strength * Time.deltaTime * mouseSensitivityY * StartGameMenu.mouseSensitivity);
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
		rigidbody.AddTorque(-transform.right * strength * Time.deltaTime * jetPackTorque * StartGameMenu.mouseSensitivity);
	}
	
	void RotateYJetpack (float strength) {
		rigidbody.AddTorque(transform.up * strength * Time.deltaTime * jetPackTorque * StartGameMenu.mouseSensitivity);
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
