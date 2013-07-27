using UnityEngine;
using System.Collections;

public static class Raycasts {
	public static RaycastHit Raycast (Vector3 position, Vector3 angle, float length) {
		RaycastHit hitInfo;
		if (Physics.Raycast(position, angle, out hitInfo, length)) {
			return hitInfo;
		} else {
			return hitInfo;	//returns a blank struct, essentially a null value.
		}
	}
}
