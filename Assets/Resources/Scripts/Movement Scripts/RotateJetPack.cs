using UnityEngine;
using System.Collections;

public class RotateJetPack : MonoBehaviour {
	
	public void Rotate (Vector3 rotation, float jetPackTorqueX, float jetPackTorqueY, float jetPackTorqueZ) {
		rotation = rotation.normalized;
		RotateX (rotation.x * jetPackTorqueX);
		RotateY (rotation.y * jetPackTorqueY);
		RotateZ (rotation.z * jetPackTorqueZ);
	}
	
	void RotateX (float strength) {
		rigidbody.AddTorque(-transform.right * strength * Time.deltaTime);
	}
	
	void RotateY (float strength) {
		rigidbody.AddTorque(transform.up * strength * Time.deltaTime);
	}
	
	void RotateZ (float strength) {
		rigidbody.AddTorque(-transform.forward * strength * Time.deltaTime);
	}
}
