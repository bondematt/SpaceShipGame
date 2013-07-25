using UnityEngine;
using System.Collections;
using System.Collections.Generic;



//class that contains the array of all ship objects
public class Ships {
	
	//list of all ships
	public List<Ship> shipList = new List<Ship>();
	
	//List of classes that all ships need access too, but only one of each class needs to exist
	
	public CombineMeshes combineMeshes = new CombineMeshes();
	
	public CreateShipMesh createShipMesh = new CreateShipMesh();
	
	public CreateTileMesh createTileMesh = new CreateTileMesh();
	
	public AddMeshToShip addMeshToShip = new AddMeshToShip();
	
	public CenterOfShipMass centerOfShipMass = new CenterOfShipMass();
	
	public CheckForNeighboringTiles checkForNeighboringTiles = new CheckForNeighboringTiles();
	
	public CreateShipCollider createShipCollider = new CreateShipCollider();
	
	public SectorToScenePosition sectorToScenePosition = new SectorToScenePosition();
	
	public CreateShipObject createShipObject = new CreateShipObject();
	
	public TileKey tileKey = new TileKey();
	
	public PositionToTile positionToTile = new PositionToTile();
	
	//default constructor
	public Ships() {
		shipList = new List<Ship>();
	}
	
	//create new ship with just a single tile
	public Ship newShip(string name, Sector sector, SectorPosition sectorPosition, Tile tile) {
		Ship ship = new Ship(name, this, sector, sectorPosition, tile);
		this.shipList.Add(ship);
		return ship;
	}
	
	//create new ship with a list of tiles
	public Ship newShip(string name, Sector sector, SectorPosition sectorPosition, List<Tile> tileList) {
		Ship ship = new Ship(name, this, sector, sectorPosition, tileList);
		this.shipList.Add(ship);
		return ship;
	}
	
	//remove ship from shipList
	public void destroyShip(Ship ship) {
		shipList.Remove(ship);
	}
}
