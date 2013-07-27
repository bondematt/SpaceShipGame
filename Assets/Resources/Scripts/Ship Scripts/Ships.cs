using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//class that contains the array of all ship objects
public class Ships {
	
	//list of all ships
	public List<Ship> shipList = new List<Ship>();
	
	//default constructor
	public Ships() {
		shipList = new List<Ship>();
	}
	
	//create new ship with just a single tile
	public Ship newShip(string name, Sector sector, SectorPosition sectorPosition, Vector3 angle, Tile tile) {
		Ship ship = new Ship(name, this, sector, sectorPosition, angle, tile);
		this.shipList.Add(ship);
		return ship;
	}
	
	//create new ship with a list of tiles
	public Ship newShip(string name, Sector sector, SectorPosition sectorPosition, Vector3 angle, List<Tile> tileList) {
		Ship ship = new Ship(name, this, sector, sectorPosition, angle, tileList);
		this.shipList.Add(ship);
		return ship;
	}
	
	//remove ship from shipList
	public void destroyShip(Ship ship) {
		shipList.Remove(ship);
	}
}
