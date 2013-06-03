using UnityEngine;
using System.Collections;

//position inside the sector
public class SectorPosition {
	public int x, y, z;
	
	public SectorPosition() {
		x = 0; 
		y = 0; 
		z = 0;
	}
	
	public SectorPosition(int x, int y, int z) {
		this.x = x;
		this.y = y;
		this.z = z;
	}
}
