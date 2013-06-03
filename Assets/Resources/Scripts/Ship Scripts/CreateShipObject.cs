using UnityEngine;
using System.Collections;

public class CreateShipObject {
	public void createShip(Ship ship, Player player) {
		Quaternion rotation = Quaternion.Euler(ship.angle.x, ship.angle.y, ship.angle.z);
		Vector3 position = ship.ships.sectorToScenePosition.GetScenePosition(ship, player);
		ship.shipObject = (GameObject) GameObject.Instantiate(Resources.Load("ship"), new Vector3 (position.x, position.y, position.z), rotation);
		ship.shipObject.name = ship.name;
	}
}
