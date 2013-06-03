using UnityEngine;
using System.Collections;

//velocity of an object used when object is out of scene
public class ObjectVelocity {
	public double x, y, z;
	
	public ObjectVelocity() {
		x = 0; 
		y = 0; 
		z = 0;
	}
	
	public ObjectVelocity(double x, double y, double z) {
		this.x = x;
		this.y = y;
		this.z = z;
	}
}
