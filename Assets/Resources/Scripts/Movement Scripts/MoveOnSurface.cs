using UnityEngine;
using System.Collections;

public class MoveOnSurface : MonoBehaviour {
	
	public void MoveDirection (Vector3 direction, float movementSpeed) {
		direction.y = 0;
		float strength = direction.magnitude;
		strength = Mathf.Clamp(strength, -1f, 1f);
		strength = strength * movementSpeed;
		MoveX (direction.normalized.x * strength);
		MoveZ (direction.normalized.z * strength);
	}
	
	void MoveX (float strength) {
		transform.Translate(transform.right * strength * Time.deltaTime, Space.World);
	}
	
	void MoveZ (float strength) {
		transform.Translate(transform.forward * strength * Time.deltaTime, Space.World);
	}

}
