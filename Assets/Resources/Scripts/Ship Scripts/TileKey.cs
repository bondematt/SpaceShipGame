using UnityEngine;
using System.Collections;

public class TileKey {
	
	public int convertToTileKey (Tile tile) {
		int tileKey = tile.tilePosition.x * 10000000 + tile.tilePosition.y * 10000 + tile.tilePosition.z * 10 + tile.type;
		return tileKey;
	}
	
	public int convertToTileKey (int x, int y, int z, int type) {
		int tileKey = x * 10000000 + y * 10000 + z * 10 + type;
		return tileKey;
	}
	
}
