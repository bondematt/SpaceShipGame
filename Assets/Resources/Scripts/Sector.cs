using UnityEngine;
using System.Collections;

//the sector number
public class Sector {
	public byte x, y, z;
	
	public Sector() {
		x = 0; 
		y = 0; 
		z = 0;
	}
	
	public Sector(byte x, byte y, byte z) {
		this.x = x;
		this.y = y;
		this.z = z;
	}
		
}
