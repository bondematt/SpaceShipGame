using UnityEngine;
using System.Collections;

//class to hold the position of the cube in the ships custom space
public struct TilePosition {
	public int x, y, z;
		
	
	public TilePosition(int x, int y, int z) {
		this.x =  x;
		this.y =  y;
		this.z =  z;
	}
}
