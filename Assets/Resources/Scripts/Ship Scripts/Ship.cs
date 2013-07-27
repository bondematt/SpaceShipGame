using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Ship class that contains all information it will need
public class Ship {
	public string name;
	public Ships ships;
	public GameObject shipObject;
	public Sector sector = new Sector();
	public SectorPosition sectorPosition = new SectorPosition();
	public Vector3 angle = new Vector3();
	public ObjectVelocity velocity = new ObjectVelocity();
	public Vector3 angularVelocity = new Vector3();
	//public Dictionary<int, Tile> tiles = new Dictionary<int, Tile>();
	public List<Tile> tilesList = new List<Tile>();
	//acceleration or power/weight
	//turn rates
	//any other parts that aren't in cubes
		
	public Ship() {
		name = null;
		ships = null;
		sector = new Sector();
		sectorPosition = new SectorPosition();
		ObjectVelocity velocity = new ObjectVelocity();
		List<Tile> tiles = new List<Tile>();
	}
	
	public Ship(string name, Ships ships, Sector sector, SectorPosition sectorPosition, Vector3 angle, Tile tile) {
		this.name = name;
		this.ships = ships;
		this.sector = sector;
		this.sectorPosition = sectorPosition;
		this.angle = angle;
		this.tilesList.Add(tile);
	}
	
	public Ship(string name, Ships ships, Sector sector, SectorPosition sectorPosition, Vector3 angle, List<Tile> tileList) {
		this.name = name;
		this.ships = ships;
		this.sector = sector;
		this.sectorPosition = sectorPosition;
		this.angle = angle;
		foreach (Tile tile in tileList) {
			this.tilesList.Add(tile);
			tile.ship = this;
		}
	}	
}
