using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.IO;


//Currently used to generate a ship for the player to interact with
public class HandlerShips : MonoBehaviour {
	
	public GameObject playerObject;
	
	public Player player;
	
	public Ships ships = new Ships();
	
	// Use this for initialization
	void Start () {		
				
		List<Tile> tiles = new List<Tile>();
		int[] tilesList = new int[] {0, 1, 3 };
		
		for (int i = 1; i < 10; i++) {
			for (int j = 1; j < 10; j ++) {
				for (int k = 1; k < 10; k++) {
					foreach (int tileType in tilesList) {
						Tile tile = new Tile( i,  j,  k, tileType,  50);
						tiles.Add (tile);
					}
				}
			}
		}
		
		/*
		for (int i = 1; i < 6; i++) {
				for (int k = 1; k < 10; k++) {
					Tile tile = new Tile( i,  1,  k, 0,  50);
					tiles.Add (tile);
				}
		}
		
		for (int i = 1; i < 6; i++) {
				for (int k = 1; k < 10; k++) {
					Tile tile = new Tile( i,  3,  k, 0,  50);
					tiles.Add (tile);
				}
		}
		
		for (int i = 1; i < 6; i++) {
				for (int k = 1; k < 3; k++) {
					Tile tile = new Tile( i,  k,  9, 3,  50);
					tiles.Add (tile);
				}
		}
		
		for (int i = 1; i < 6; i++) {
				for (int k = 1; k < 3; k++) {
					Tile tile = new Tile( i,  k,  0, 3,  50);
					tiles.Add (tile);
				}
		}
		
		for (int i = 1; i < 10; i++) {
				for (int k = 1; k < 3; k++) {
					Tile tile = new Tile( 0,  k,  i, 1,  50);
					tiles.Add (tile);
				}
		}
		
		for (int i = 1; i < 10; i++) {
				for (int k = 1; k < 3; k++) {
					Tile tile = new Tile( 5,  k,  i, 1,  50);
					tiles.Add (tile);
				}
		}*/

		Ship ship = ships.newShip("Spartacus", new Sector(0,0,0), new SectorPosition(0,0,10), new Vector3(15,230,171), tiles);
		player = new Player(playerObject);
				
		float startMesh = 0;
		float endMesh = 0;
		float endColliders = 0;
		float endCoM = 0;
		
		CreateShipObject.createShip(ship, player);
		
		startMesh = Time.realtimeSinceStartup;
				
		CreateShipMesh.createShipMesh(player, ship);
		
		endMesh = Time.realtimeSinceStartup;
		
		//CreateShipCollider.createColliders(ship);
		
		//Add mesh collider
		//set mesh collider to same as ship mesh
		//see how it runs. Put out fires when done
		
		MeshCollider shipCollider = ship.shipObject.AddComponent<MeshCollider>();
		shipCollider.sharedMesh = ship.shipObject.GetComponent<MeshFilter>().mesh;
		
		
		endColliders = Time.realtimeSinceStartup;
		
		CenterOfShipMass.centerOfMass(player, ship);
		
		ship.shipObject.rigidbody.sleepVelocity = 0;
		ship.shipObject.rigidbody.sleepAngularVelocity = 0;
		
		ship.shipObject.rigidbody.inertiaTensor = new Vector3(1000,1000,1000);
		
		endCoM = Time.realtimeSinceStartup;
		
		Debug.Log ("Start Mesh: " + startMesh + ", Start Colliders: " + endMesh + ", Start CoM: " + endColliders + ", End: " + endCoM);
		
		ship.shipObject.SetActive(true);
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}











