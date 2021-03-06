using UnityEngine;
using System.Collections;

//Takes a position and returns the tile or side of tile that is at the position
public static class PositionToTile {
	
	static float colliderSkin = .99f;
	
	static public Vector3 PositionToTilePosition () {
		Vector3 tilePosition = new Vector3();
		return tilePosition;
	}
	
	static public Vector3 PositionToNormalEuler (Vector3 position, Collider collider) {
		Vector3 normal = new Vector3();
		Vector3 localPosition = collider.transform.InverseTransformPoint(position); //Put the position in the colliders space
		Vector3 size = collider.GetComponent<BoxCollider>().size;
		
		if (localPosition.x > colliderSkin * size.x/2) {normal = new Vector3(0,0,270);}
		else if (localPosition.x < colliderSkin * -size.x/2) {normal = new Vector3(0,0,90);}
		else if (localPosition.y > colliderSkin * size.y/2) {normal = new Vector3(0,0,0);}
		else if (localPosition.y < colliderSkin * -size.y/2) {normal = new Vector3(0,0,180);}
		else if (localPosition.z > colliderSkin * size.z/2) {normal = new Vector3(90,0,0);}
		else if (localPosition.z < colliderSkin * -size.z/2) {normal = new Vector3(270,0,0);}
		else {Debug.Log ("Could not get normal. localPosition x: " + localPosition.x + ", y: " + localPosition.y + ", z: " + localPosition.z);}
		
		return normal;
	}
	
	static public Vector3 PositionToNormalVector (Vector3 position, Collider collider) {
		Vector3 normal = new Vector3();
		Vector3 localPosition = collider.transform.InverseTransformPoint(position); //Put the position in the colliders space
		Vector3 size = collider.GetComponent<BoxCollider>().size;
		
		if (localPosition.x > colliderSkin * size.x/2) {normal = collider.transform.right;}
		else if (localPosition.x < colliderSkin * -size.x/2) {normal = -collider.transform.right;}
		else if (localPosition.y > colliderSkin * size.y/2) {normal = collider.transform.up;}
		else if (localPosition.y < colliderSkin * -size.y/2) {normal = -collider.transform.up;}
		else if (localPosition.z > colliderSkin * size.z/2) {normal = collider.transform.forward;}
		else if (localPosition.z < colliderSkin * -size.z/2) {normal = -collider.transform.forward;}
		else {Debug.Log ("Could not get normal. localPosition x: " + localPosition.x + ", y: " + localPosition.y + ", z: " + localPosition.z);}
		
		return normal;
	}
}
