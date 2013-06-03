using UnityEngine;
using System.Collections;

//Contains all of the players information
public class Player {
	public GameObject playerObject;
	public Sector sector;
	public SectorPosition sectorPosition;
	public Vector3 angle = new Vector3();
	public ObjectVelocity velocity = new ObjectVelocity();
	public Vector3 angularVelocity = new Vector3();
	public HandlerShips handlerShip;
	public HandlerScene handlerScene;
	
	public Player(GameObject playerObject) {
		this.playerObject = playerObject;
		sector = new Sector();
		sectorPosition = new SectorPosition();
		Vector3 angle = new Vector3(0,0,0);
		ObjectVelocity velocity = new ObjectVelocity();
	 	Vector3 angularVelocity = new Vector3(0,0,0);
		updateScriptRef();
	}
	
	public void updateScriptRef() {
		handlerShip = playerObject.GetComponent<HandlerShips>();
		handlerScene = playerObject.GetComponent<HandlerScene>();
	}	
}
