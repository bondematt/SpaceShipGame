using UnityEngine;
using System.Collections;

public class JetPackStabilize : MonoBehaviour {
	
	bool jetPackStabilize = true;
	
	public float jetPackStabilizeForce = 5f;
	
	public float jetPackStabilizeTorque = 1f;

	void FixedUpdate () {
		if (jetPackStabilize)
			JetPackStabilizeNormal();
	}
	
	public void ToggleJetpackStabilize () {
		if (jetPackStabilize) {
			jetPackStabilize = false;	
		} else {
			jetPackStabilize = true;	
		}
	}
	
	void JetPackStabilizeNormal () {
		rigidbody.AddForce(-rigidbody.velocity.normalized * jetPackStabilizeForce * Time.deltaTime);
		if (rigidbody.angularVelocity.magnitude > 1) {	
			rigidbody.AddTorque(-rigidbody.angularVelocity.normalized * jetPackStabilizeTorque * Time.deltaTime);
		} else {
			rigidbody.AddTorque(-rigidbody.angularVelocity * jetPackStabilizeTorque * Time.deltaTime);
		}
	}
}
