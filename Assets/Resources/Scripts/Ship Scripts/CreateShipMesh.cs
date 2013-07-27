using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Creates a ship mesh based on the tiles in the ship and the relevant player and ship positions
public static class CreateShipMesh {
	public static void createShipMesh(Player player, Ship ship) {

		Quaternion rotation = Quaternion.Euler(ship.angle.x, ship.angle.y, ship.angle.z);
		
		Vector3 positionOffset = SectorToScenePosition.GetScenePosition(ship, player);
				
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
				MeshAttributes meshAttributes = CreateTileMesh.createTileVerticesTriangles(new Vector3 (tile.tilePosition.x, tile.tilePosition.y + HandlerScene.tileOffset, tile.tilePosition.z), rotation, floorTileDimensions);
				meshAttributesList.Add (meshAttributes);
			}
			
			else if (tile.type == 1) {		
				MeshAttributes meshAttributes = CreateTileMesh.createTileVerticesTriangles(new Vector3 (tile.tilePosition.x - HandlerScene.tileOffset, tile.tilePosition.y, tile.tilePosition.z), rotation, wallTileXDimensions);
				meshAttributesList.Add (meshAttributes);
			}
			
			else if (tile.type == 2) {
				MeshAttributes meshAttributes = CreateTileMesh.createTileVerticesTriangles(new Vector3 (tile.tilePosition.x + HandlerScene.tileOffset, tile.tilePosition.y, tile.tilePosition.z), rotation, wallTileXDimensions);
				meshAttributesList.Add (meshAttributes);
			}
			
			else if (tile.type == 3) {
				MeshAttributes meshAttributes = CreateTileMesh.createTileVerticesTriangles(new Vector3 (tile.tilePosition.x, tile.tilePosition.y, tile.tilePosition.z - HandlerScene.tileOffset), rotation, wallTileYDimensions);
				meshAttributesList.Add (meshAttributes);
			}
			
			else if (tile.type == 4) {
				MeshAttributes meshAttributes = CreateTileMesh.createTileVerticesTriangles(new Vector3 (tile.tilePosition.x, tile.tilePosition.y, tile.tilePosition.z + HandlerScene.tileOffset), rotation, wallTileYDimensions);
				meshAttributesList.Add (meshAttributes);
			}
		}
		
		AddMeshToShip.addMesh(meshAttributesList, shipMesh);
		ship.shipObject.transform.Translate((ship.shipObject.transform.position - ship.shipObject.rigidbody.worldCenterOfMass), Space.World);
	}
	
}
