using UnityEngine;
using System.Collections;

public class Hull {
	Vector3 a, b, c; //posittion of the points this tile is made of
	
	Vector3 normal; //normal formed from these points
	
	public Hull (Vector3 a, Vector3 b, Vector3 c) {
		this.a = a;
		this.b = b;
		this.c = c;
	}
	
	public void GetSetNormal() {
		Vector3 dir = Vector3.Cross(b - a, c - a);
        normal = Vector3.Normalize(dir);
	}
}
