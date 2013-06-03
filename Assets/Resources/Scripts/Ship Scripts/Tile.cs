using UnityEngine;
using System.Collections;

//tile class that holds it's type(wall,floor) and health
public class Tile : Part {
	//public TilePosition position = new TilePosition();
	public int type = 0; //0 will be floor; 1-2 : wallX; 3-4 : wallZ
	public Ship ship = new Ship();
	public TilePosition tilePosition = new TilePosition();
	
	public Tile() {
		byte type = 0;
		byte health = 0;
	}
	
	public Tile(byte x, byte y, byte z, byte type, byte health) {
		this.type = type;
		this.health = health;
		tilePosition = new TilePosition(x, y, z);
	}
	
	public Tile(int x, int y, int z, int type, int health) {
		this.type = type;
		this.health = health;
		tilePosition = new TilePosition(x, y, z);
	}
	
	//things attached to tile
}
