using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CenterOfShipMass {
	//calculates the center of mass
	public void centerOfMass(Player player, Ship ship) {
		Vector3 positions = Vector3.zero;
		Vector3 tileOffset = Vector3.zero;
		float mass = 0;
		float totalMass = 0;
		int i = 0;
		foreach (Tile tile in ship.tilesList) {
			tileOffset = Vector3.zero;
			if (tile.type == 0) {
				tileOffset = ship.shipObject.transform.TransformPoint(tile.tilePosition.x, tile.tilePosition.y + player.handlerScene.tileOffset, tile.tilePosition.z);
				mass = .01f;
				i++;
			}
			
			else if (tile.type == 1) {
				tileOffset = ship.shipObject.transform.TransformPoint(tile.tilePosition.x - player.handlerScene.tileOffset, tile.tilePosition.y, tile.tilePosition.z);
				mass = .01f;
				i++;
			}
			
			else if (tile.type == 2) {
				tileOffset = ship.shipObject.transform.TransformPoint(tile.tilePosition.x + player.handlerScene.tileOffset, tile.tilePosition.y, tile.tilePosition.z);
				mass = .01f;
				i++;
			}
			
			else if (tile.type == 5) {
				tileOffset = ship.shipObject.transform.TransformPoint(tile.tilePosition.x, tile.tilePosition.y, tile.tilePosition.z - player.handlerScene.tileOffset);
				mass = .01f;
				i++;
			}
			
			else if (tile.type == 6) {
				tileOffset = ship.shipObject.transform.TransformPoint(tile.tilePosition.x, tile.tilePosition.y, tile.tilePosition.z + player.handlerScene.tileOffset);
				mass = .01f;
				i++;
			}
			positions += tileOffset;
			totalMass += mass;
		}
		
		ship.shipObject.rigidbody.centerOfMass = ship.shipObject.transform.InverseTransformPoint((positions / i));
		ship.shipObject.GetComponent<Rigidbody>().mass = totalMass;
	}
}
