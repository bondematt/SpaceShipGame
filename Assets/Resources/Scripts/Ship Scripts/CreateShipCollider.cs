using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class CreateShipCollider {	
	
	//run this on a ship when every tile needs to be checked and loaded
	static public void createColliders(Ship ship) {
		List<Tile> doneTiles = new List<Tile>();
		List<Tile> returnTiles = new List<Tile>();
		float maxX = 0;
		float minX = 0;
		float maxY = 0;
		float minY = 0;
		float maxZ = 0;
		float minZ = 0;
		GameObject collider;
		Vector3 tileOffset = new Vector3();
		Vector3 size = new Vector3();
		
		//find tiles next to each other, group them into arrays, figure out how big the collider needs to be, create it, center it, set ship as parent
		foreach (Tile tile in ship.tilesList) {
			returnTiles = new List<Tile>();
			
			if (!doneTiles.Contains(tile)) {
				
				returnTiles = CheckForNeighboringTiles.checkForNeighbors(tile, ship);
								
				//initialize collider size
				maxX = returnTiles[0].tilePosition.x;
				minX = returnTiles[0].tilePosition.x;
				maxY = returnTiles[0].tilePosition.y;
				minY = returnTiles[0].tilePosition.y;
				maxZ = returnTiles[0].tilePosition.z;
				minZ = returnTiles[0].tilePosition.z;
				
				//figure out the size the collider will be
				foreach (Tile sizeTile in returnTiles) {
					if (sizeTile.tilePosition.x > maxX)
						maxX = sizeTile.tilePosition.x;
					if (sizeTile.tilePosition.x < minX)
						minX = sizeTile.tilePosition.x;
					if (sizeTile.tilePosition.y > maxY)
						maxY = sizeTile.tilePosition.y;
					if (sizeTile.tilePosition.y < minY)
						minY = sizeTile.tilePosition.y;
					if (sizeTile.tilePosition.z > maxZ)
						maxZ = sizeTile.tilePosition.z;
					if (sizeTile.tilePosition.z < minZ)
						minZ = sizeTile.tilePosition.z;
				}
				
				if (tile.type == 0) {
					size = new Vector3(maxX - minX + 1.1f, .1f, maxZ - minZ + 1.1f);
					tileOffset = ship.shipObject.transform.TransformPoint(((maxX + minX) / 2), (maxY- .5f), ((maxZ + minZ) / 2));
					collider = (GameObject) GameObject.Instantiate(Resources.Load("collider"), tileOffset, Quaternion.Euler(ship.angle.x, ship.angle.y, ship.angle.z));
					collider.GetComponent<BoxCollider>().size = size;
					collider.transform.parent = ship.shipObject.transform;
					collider.name = "FloorTileCollider";
					collider = null;
				}
				
				if (tile.type == 1) {
					
					size = new Vector3(.1f, maxY - minY + 1.1f, maxZ - minZ + 1.1f);
					
					tileOffset = ship.shipObject.transform.TransformPoint((maxX + .5f), ((maxY + minY) / 2), ((maxZ + minZ) / 2));
					collider = (GameObject) GameObject.Instantiate(Resources.Load("collider"), tileOffset, Quaternion.Euler(ship.angle.x, ship.angle.y, ship.angle.z));
					collider.GetComponent<BoxCollider>().size = size;
					collider.transform.parent = ship.shipObject.transform;
					collider.name = "WallTileZCollider";
					collider = null;
				}
				
				if (tile.type == 2) {
					
					
					size = new Vector3(.1f, maxY - minY + 1.1f, maxZ - minZ + 1.1f);
					
					tileOffset = ship.shipObject.transform.TransformPoint((maxX - .5f), ((maxY + minY) / 2), ((maxZ + minZ) / 2));
					collider = (GameObject) GameObject.Instantiate(Resources.Load("collider"), tileOffset, Quaternion.Euler(ship.angle.x, ship.angle.y, ship.angle.z));
					collider.GetComponent<BoxCollider>().size = size;
					collider.transform.parent = ship.shipObject.transform;
					collider = null;
				}
				
				if (tile.type == 3) {

					size = new Vector3(maxX - minX + 1.1f, maxY - minY + 1.1f, .1f);
					
					tileOffset = ship.shipObject.transform.TransformPoint(((maxX + minX) / 2), ((maxY + minY) / 2), (maxZ + .5f));
					collider = (GameObject) GameObject.Instantiate(Resources.Load("collider"), tileOffset, Quaternion.Euler(ship.angle.x, ship.angle.y, ship.angle.z));
					collider.GetComponent<BoxCollider>().size = size;
					collider.transform.parent = ship.shipObject.transform;
					collider.name = "WallTileXCollider";
					collider = null;
				}
				
				if (tile.type == 4) {
					
					size = new Vector3(maxX - minX + 1.1f, maxY - minY + 1.1f, .1f);
					
					tileOffset = ship.shipObject.transform.TransformPoint(((maxX + minX) / 2), ((maxY + minY) / 2), (maxZ - .5f));
					collider = (GameObject) GameObject.Instantiate(Resources.Load("collider"), tileOffset, Quaternion.Euler(ship.angle.x, ship.angle.y, ship.angle.z));
					collider.GetComponent<BoxCollider>().size = size;
					collider.transform.parent = ship.shipObject.transform;
					collider = null;
				}
				foreach (Tile returnedTile in returnTiles) {
					doneTiles.Add(returnedTile);
				}
			}
	
		}
	}
}
