using UnityEngine;
using System.Collections;

public class MoveJetPack : MonoBehaviour {

	public void MoveDirection (Vector3 direction,  float jetPackForce) 
	{
		float strength = direction.magnitude;
		strength = Mathf.Clamp(strength, -1f, 1f);
		strength = strength * jetPackForce;
		MoveX (direction.normalized.x * strength);
		MoveY (direction.normalized.y * strength);
		MoveZ (direction.normalized.z * strength);
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
