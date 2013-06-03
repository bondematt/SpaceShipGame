using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//call with player class
public class CreateShipMesh {
	public void createShipMesh(Player player, Ship ship) {

		Quaternion rotation = Quaternion.Euler(ship.angle.x, ship.angle.y, ship.angle.z);
		
		Vector3 positionOffset = ship.ships.sectorToScenePosition.GetScenePosition(ship, player);
				
		List<Tile> shipTiles = new List<Tile>();
						
		Vector3 cubeOffset = new Vector3 ();
		
		//MeshAttributes meshAttributes = new MeshAttributes();
		
		Mesh shipMesh = ship.shipObject.GetComponent<MeshFilter>().mesh;
		
		MeshAttributes[] AllMeshAttributes;
		
		List<MeshAttributes> meshAttributesList = new List<MeshAttributes>();
		
		Vector3 floorTileDimensions = new Vector3 (.55f,.05f,.55f);
		Vector3 wallTileXDimensions = new Vector3 (.05f,.55f,.55f);
		Vector3 wallTileYDimensions = new Vector3 (.55f,.55f,.05f);
		
		foreach (Tile tile in ship.tilesList) {
			if (tile.type == 0) {
				MeshAttributes meshAttributes = ship.ships.createTileMesh.createTileVerticesTriangles(new Vector3 (tile.tilePosition.x, tile.tilePosition.y + player.handlerScene.tileOffset, tile.tilePosition.z), rotation, floorTileDimensions);
				meshAttributesList.Add (meshAttributes);
			}
			
			else if (tile.type == 1) {		
				MeshAttributes meshAttributes = ship.ships.createTileMesh.createTileVerticesTriangles(new Vector3 (tile.tilePosition.x - player.handlerScene.tileOffset, tile.tilePosition.y, tile.tilePosition.z), rotation, wallTileXDimensions);
				meshAttributesList.Add (meshAttributes);
			}
			
			else if (tile.type == 2) {
				MeshAttributes meshAttributes = ship.ships.createTileMesh.createTileVerticesTriangles(new Vector3 (tile.tilePosition.x + player.handlerScene.tileOffset, tile.tilePosition.y, tile.tilePosition.z), rotation, wallTileXDimensions);
				meshAttributesList.Add (meshAttributes);
			}
			
			else if (tile.type == 3) {
				MeshAttributes meshAttributes = ship.ships.createTileMesh.createTileVerticesTriangles(new Vector3 (tile.tilePosition.x, tile.tilePosition.y, tile.tilePosition.z - player.handlerScene.tileOffset), rotation, wallTileYDimensions);
				meshAttributesList.Add (meshAttributes);
			}
			
			else if (tile.type == 4) {
				MeshAttributes meshAttributes = ship.ships.createTileMesh.createTileVerticesTriangles(new Vector3 (tile.tilePosition.x, tile.tilePosition.y, tile.tilePosition.z + player.handlerScene.tileOffset), rotation, wallTileYDimensions);
				meshAttributesList.Add (meshAttributes);
			}
		}
		
		ship.ships.addMeshToShip.addMesh(meshAttributesList, shipMesh);
		ship.shipObject.transform.Translate((ship.shipObject.transform.position - ship.shipObject.rigidbody.worldCenterOfMass), Space.World);
	}
	
}
