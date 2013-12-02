using UnityEngine;
using System.Collections;

public class PIDController {
	public float pGain; // adjust the individual gains
	public float iGain; // iGain better to be 0 in this case
	public float dGain;
	float pError;
	float iError;
	float dError;
	float torque;
	float lastPError = 0f;

	public PIDController() {

	}

	public PIDController(float pGain, float iGain, float dGain) {
		this.pGain = pGain;
		this.iGain = iGain;
		this.dGain = dGain;
	}

	//send error in degrees with correct direction sign.
	public float PidLoop(float pError, float maxTorque){
		//pError = desiredAngle - currentAngle; // calculate the proportional error... already set
		iError += pError * Time.deltaTime; // the integral error
		dError = (pError - lastPError)/ Time.deltaTime; // and the differential error
		lastPError = pError;
		// the torque is the sum of all errors weighted by their gains:
		torque = pGain*pError+iGain*iError+dGain*dError;
		// return the torque to the object
		torque = Mathf.Clamp(torque, -maxTorque, maxTorque);
		return torque;
	}
}
