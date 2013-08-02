using UnityEngine;
using System.Collections;

public class RotateOnSurface : MonoBehaviour {
	
	public void Rotate (float yRotate) {
		rotateY (yRotate);
	}
	
	void rotateY (float angle) {
		transform.RotateAround(transform.up, angle * Time.deltaTime);
	}
}
