using UnityEngine;
using System.Collections;

public static class Sweeptests {
	public static RaycastHit Sweeptest (Rigidbody _rigidbody, Vector3 direction, float distance) {
		RaycastHit hitInfo;
		if (_rigidbody.SweepTest(direction, out hitInfo,distance)) {
			return hitInfo;
		} else {
			return hitInfo;
		}
	}
}
