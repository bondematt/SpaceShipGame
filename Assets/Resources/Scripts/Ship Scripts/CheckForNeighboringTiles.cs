using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheckForNeighboringTiles {
	
	//re-write to remove all tiles on diffent x, y, and z planes at once.
	
	public List<Tile> checkForNeighbors(Tile shipTile, Ship ship) {
		List<Tile> shipTiles = new List<Tile>();
		List<Tile> tilesFound = new List<Tile>();
		List<Tile> tilesChecking = new List<Tile>();
		int width = 1;
		int length = 1;
		int height = 1;
		int i = 0;
		
		int type = shipTile.type;
		
		tilesFound.Add(shipTile);

	 	bool checkForward = true, checkRight = true, checkBack = true, checkLeft = true, checkUp = true, checkDown = true;
		if (type == 0) {
			checkUp = false;
			checkDown = false;
			foreach (Tile tile in ship.tilesList) {
				if (tile.type == type) {
					if (tile.tilePosition.y == shipTile.tilePosition.y)
						shipTiles.Add(tile);
				}
			}
		}
		if (type == 1 || type == 2) {
			checkRight = false;
			checkLeft = false;
			foreach (Tile tile in ship.tilesList) {
				if (tile.type == type) {
					if (tile.tilePosition.x == shipTile.tilePosition.x)
						shipTiles.Add(tile);
				}
			}
		}
		if (type == 3 || type == 4) {
			checkForward = false;
			checkBack = false;
			foreach (Tile tile in ship.tilesList) {
				if (tile.type == type) {
					if (tile.tilePosition.z == shipTile.tilePosition.z)
						shipTiles.Add(tile);
				}
			}
		}
		
		shipTiles.Remove(shipTile);
		
		while ((checkForward || checkRight || checkDown || checkBack || checkLeft || checkUp) && i < 10000) {
			if (checkForward) {
				//for every cube that we want a collider for
				foreach (Tile foundTile in tilesFound) {
					//check if it has a neighbor in the direction and add neighbor to array if it does
					foreach (Tile checkTile in shipTiles) {
						if (checkTile.tilePosition.x == foundTile.tilePosition.x && checkTile.tilePosition.y == foundTile.tilePosition.y && checkTile.tilePosition.z == (foundTile.tilePosition.z + 1))
							tilesChecking.Add(checkTile);
					}
				}
				//if cubesChecking is the correct size add it to foundCubes, if not stop checking this direction
				if (tilesChecking.Count != width && type == 0) {
					checkForward = false;
				} else if (tilesChecking.Count != height && (type == 1 || type == 2)) {
					checkForward = false;
				} else {
					tilesFound.AddRange(tilesChecking);
					foreach(Tile checkingTile in tilesChecking) {
						shipTiles.Remove(checkingTile);
					}
					length ++;
				}
				tilesChecking = new List<Tile>(); //clear array to avoid old data
			}
			
			if (checkRight) {
				//for every cube that we want a collider for
				foreach (Tile foundTile in tilesFound) {
					//check if it has a neighbor in the direction and add neighbor to array if it does
					foreach (Tile checkTile in shipTiles) {
						if (checkTile.tilePosition.x == (foundTile.tilePosition.x + 1) && checkTile.tilePosition.y == foundTile.tilePosition.y && checkTile.tilePosition.z == foundTile.tilePosition.z)
							tilesChecking.Add(checkTile);
					}
				}
				//if cubesChecking is the correct size add it to foundCubes, if not stop checking this direction
				if (tilesChecking.Count != length && type == 0) {
					checkRight = false;
				} else if (tilesChecking.Count != height && (type == 3 || type == 4)) {
					checkRight = false;
				} else {
					tilesFound.AddRange(tilesChecking);
					foreach(Tile checkingTile in tilesChecking) {
						shipTiles.Remove(checkingTile);
					}
					width ++;
				}
				tilesChecking = new List<Tile>(); //clear array to avoid old data
			}
			
			if (checkDown) {
				//for every cube that we want a collider for
				foreach (Tile foundTile in tilesFound) {
					//check if it has a neighbor in the direction and add neighbor to array if it does
					foreach (Tile checkTile in shipTiles) {
						if (checkTile.tilePosition.x == foundTile.tilePosition.x && checkTile.tilePosition.y == (foundTile.tilePosition.y - 1) && checkTile.tilePosition.z == foundTile.tilePosition.z)
							tilesChecking.Add(checkTile);
					}
				}
				//if cubesChecking is the correct size add it to foundCubes, if not stop checking this direction
				if (tilesChecking.Count != length && (type == 1 || type == 2)) {
					checkDown = false;
				} else if (tilesChecking.Count != width && (type == 3 || type == 4)) {
					checkDown = false;
				} else {
					tilesFound.AddRange(tilesChecking);
					foreach(Tile checkingTile in tilesChecking) {
						shipTiles.Remove(checkingTile);
					}
					height ++;
				}
				tilesChecking = new List<Tile>(); //clear array to avoid old data
			}
			
			if (checkBack) {
				//for every cube that we want a collider for
				foreach (Tile foundTile in tilesFound) {
					//check if it has a neighbor in the direction and add neighbor to array if it does
					foreach (Tile checkTile in shipTiles) {
						if (checkTile.tilePosition.x == foundTile.tilePosition.x && checkTile.tilePosition.y == foundTile.tilePosition.y && checkTile.tilePosition.z == (foundTile.tilePosition.z - 1))
							tilesChecking.Add(checkTile);
					}
				}
				//if cubesChecking is the correct size add it to foundCubes, if not stop checking this direction
				if (tilesChecking.Count != width && type == 0) {
					checkBack = false;
				} else if (tilesChecking.Count != height && (type == 1 || type == 2)) {
					checkBack = false;
				} else {
					tilesFound.AddRange(tilesChecking);
					foreach(Tile checkingTile in tilesChecking) {
						shipTiles.Remove(checkingTile);
					}
					length ++;
				}
				tilesChecking = new List<Tile>(); //clear array to avoid old data
			}
			
			if (checkLeft) {
				//for every cube that we want a collider for
				foreach (Tile foundTile in tilesFound) {
					//check if it has a neighbor in the direction and add neighbor to array if it does
					foreach (Tile checkTile in shipTiles) {
						if (checkTile.tilePosition.x == (foundTile.tilePosition.x - 1) && checkTile.tilePosition.y == foundTile.tilePosition.y && checkTile.tilePosition.z == foundTile.tilePosition.z)
							tilesChecking.Add(checkTile);
					}
				}
				//if cubesChecking is the correct size add it to foundCubes, if not stop checking this direction
				if (tilesChecking.Count != length && type == 0) {
					checkLeft = false;
				} else if (tilesChecking.Count != height && (type == 3 || type == 4)) {
					checkLeft = false;
				} else {
					tilesFound.AddRange(tilesChecking);
					foreach(Tile checkingTile in tilesChecking) {
						shipTiles.Remove(checkingTile);
					}
					width ++;
				}
				tilesChecking = new List<Tile>(); //clear array to avoid old data
			}
			
			if (checkUp) {
				//for every cube that we want a collider for
				foreach (Tile foundTile in tilesFound) {
					//check if it has a neighbor in the direction and add neighbor to array if it does
					foreach (Tile checkTile in shipTiles) {
						if (checkTile.tilePosition.x == foundTile.tilePosition.x && checkTile.tilePosition.y == (foundTile.tilePosition.y + 1) && checkTile.tilePosition.z == foundTile.tilePosition.z)
							tilesChecking.Add(checkTile);
					}
				}
				//if cubesChecking is the correct size add it to foundCubes, if not stop checking this direction
				if (tilesChecking.Count != length && (type == 1 || type == 2)) {
					checkUp = false;
				} else if (tilesChecking.Count != width && (type == 3 || type == 4)) {
					checkUp = false;
				} else {
					tilesFound.AddRange(tilesChecking);
					foreach(Tile checkingTile in tilesChecking) {
						shipTiles.Remove(checkingTile);
					}
					height ++;
				}
				tilesChecking = new List<Tile>(); //clear array to avoid old data
			}
		}
		i++;
		return tilesFound;
	}
	
	/*
	public List<Tile> checkForNeighbors(Tile shipTile, Ship ship) {
		List<Tile> tilesFound = new List<Tile>();
		List<Tile> tilesChecking = new List<Tile>();
		List<Tile> returnTiles = new List<Tile>();
		
		int width = 1;
		int length = 1;
		int height = 1;
		int i = 0;
		Tile valueTile;
		
		int type = shipTile.type;
		
		tilesFound.Add(shipTile);

	 	bool checkForward = true, checkRight = true, checkBack = true, checkLeft = true, checkUp = true, checkDown = true;
		if (type == 0) {
			checkUp = false;
			checkDown = false;
		}
		if (type == 1 || type == 2) {
			checkRight = false;
			checkLeft = false;
		}
		if (type == 3 || type == 4) {
			checkForward = false;
			checkBack = false;
		}
		
		while ((checkForward || checkRight || checkDown || checkBack || checkLeft || checkUp) && i < 10000) {
			if (checkForward) {
				//for every cube that we want a collider for
				foreach (Tile foundTile in tilesFound) {
					//check if it has a neighbor in the direction and add neighbor to array if it does
					if (ship.tiles.TryGetValue(ship.ships.tileKey.convertToTileKey(foundTile.tilePosition.x, foundTile.tilePosition.y, foundTile.tilePosition.z + 1, foundTile.type), out valueTile)) {
						tilesChecking.Add(valueTile);
					}
				}
				//if cubesChecking is the correct size add it to foundCubes, if not stop checking this direction
				if (tilesChecking.Count != width && type == 0) {
					checkForward = false;
				} else if (tilesChecking.Count != height && (type == 1 || type == 2)) {
					checkForward = false;
				} else {
					tilesFound.AddRange(tilesChecking);
					length ++;
				}
				tilesChecking = new List<Tile>(); //clear array to avoid old data
			}
			
			if (checkRight) {
				//for every cube that we want a collider for
				foreach (Tile foundTile in tilesFound) {
					//check if it has a neighbor in the direction and add neighbor to array if it does
					if (ship.tiles.TryGetValue(ship.ships.tileKey.convertToTileKey(foundTile.tilePosition.x + 1, foundTile.tilePosition.y, foundTile.tilePosition.z, foundTile.type), out valueTile)) {
						tilesChecking.Add(valueTile);
					}
				}
				//if cubesChecking is the correct size add it to foundCubes, if not stop checking this direction
				if (tilesChecking.Count != length && type == 0) {
					checkRight = false;
				} else if (tilesChecking.Count != height && (type == 3 || type == 4)) {
					checkRight = false;
				} else {
					tilesFound.AddRange(tilesChecking);
					width ++;
				}
				tilesChecking = new List<Tile>(); //clear array to avoid old data
			}
			
			if (checkDown) {
				//for every cube that we want a collider for
				foreach (Tile foundTile in tilesFound) {
					//check if it has a neighbor in the direction and add neighbor to array if it does
					if (ship.tiles.TryGetValue(ship.ships.tileKey.convertToTileKey(foundTile.tilePosition.x, foundTile.tilePosition.y - 1, foundTile.tilePosition.z, foundTile.type), out valueTile)) {
						tilesChecking.Add(valueTile);
					}
				}
				//if cubesChecking is the correct size add it to foundCubes, if not stop checking this direction
				if (tilesChecking.Count != length && (type == 1 || type == 2)) {
					checkDown = false;
				} else if (tilesChecking.Count != width && (type == 3 || type == 4)) {
					checkDown = false;
				} else {
					tilesFound.AddRange(tilesChecking);
					height ++;
				}
				tilesChecking = new List<Tile>(); //clear array to avoid old data
			}
			
			if (checkBack) {
				//for every cube that we want a collider for
				foreach (Tile foundTile in tilesFound) {
					//check if it has a neighbor in the direction and add neighbor to array if it does
					if (ship.tiles.TryGetValue(ship.ships.tileKey.convertToTileKey(foundTile.tilePosition.x, foundTile.tilePosition.y, foundTile.tilePosition.z - 1, foundTile.type), out valueTile)) {
						tilesChecking.Add(valueTile);
					}
				}
				//if cubesChecking is the correct size add it to foundCubes, if not stop checking this direction
				if (tilesChecking.Count != width && type == 0) {
					checkBack = false;
				} else if (tilesChecking.Count != height && (type == 1 || type == 2)) {
					checkBack = false;
				} else {
					tilesFound.AddRange(tilesChecking);
					length ++;
				}
				tilesChecking = new List<Tile>(); //clear array to avoid old data
			}
			
			if (checkLeft) {
				//for every cube that we want a collider for
				foreach (Tile foundTile in tilesFound) {
					//check if it has a neighbor in the direction and add neighbor to array if it does
					if (ship.tiles.TryGetValue(ship.ships.tileKey.convertToTileKey(foundTile.tilePosition.x - 1, foundTile.tilePosition.y, foundTile.tilePosition.z, foundTile.type), out valueTile)) {
						tilesChecking.Add(valueTile);
					}
				}
				//if cubesChecking is the correct size add it to foundCubes, if not stop checking this direction
				if (tilesChecking.Count != length && type == 0) {
					checkLeft = false;
				} else if (tilesChecking.Count != height && (type == 3 || type == 4)) {
					checkLeft = false;
				} else {
					tilesFound.AddRange(tilesChecking);
					width ++;
				}
				tilesChecking = new List<Tile>(); //clear array to avoid old data
			}
			
			if (checkUp) {
				//for every cube that we want a collider for
				foreach (Tile foundTile in tilesFound) {
					//check if it has a neighbor in the direction and add neighbor to array if it does
					if (ship.tiles.TryGetValue(ship.ships.tileKey.convertToTileKey(foundTile.tilePosition.x, foundTile.tilePosition.y + 1, foundTile.tilePosition.z, foundTile.type), out valueTile)) {
						tilesChecking.Add(valueTile);
					}
				}
				//if cubesChecking is the correct size add it to foundCubes, if not stop checking this direction
				if (tilesChecking.Count != length && (type == 1 || type == 2)) {
					checkUp = false;
				} else if (tilesChecking.Count != width && (type == 3 || type == 4)) {
					checkUp = false;
				} else {
					tilesFound.AddRange(tilesChecking);
					height ++;
				}
				tilesChecking = new List<Tile>(); //clear array to avoid old data
			}
			foreach (Tile tile in returnTiles) {
				if (tilesFound.Contains(tile)) {
					tilesFound.Remove(tile);
				}
			}
			returnTiles.AddRange(tilesFound);
			i++;
			
			//remove tiles that have been checked from tilesfound while keeping them in returnTiles and adding the newtiles.
		}
		return returnTiles;
	}*/
}
