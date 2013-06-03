using UnityEngine;
using System.Collections;

//Takes a position and returns the tile or side of tile that is at the position
public class PositionToTile : MonoBehaviour {
	public Vector3 PositionToTilePosition () {
		Vector3 tilePosition = new Vector3();
		return tilePosition;
	}
	
	public Vector3 PositionToNormal (Vector3 position, Collider collider) {
		Vector3 normal = new Vector3();
		Vector3 localPosition = collider.transform.InverseTransformPoint(position); //Put the position in the colliders space
		Vector3 size = collider.GetComponent<BoxCollider>().size;
		
		if (localPosition.x > size.x/2) {normal = new Vector3(0,0,270); Debug.Log ("local > x");}
		else if (localPosition.x < -size.x/2) {normal = new Vector3(0,0,90); Debug.Log ("local < -x");}
		else if (localPosition.y > size.y/2) {normal = new Vector3(0,0,0); Debug.Log ("local > y");}
		else if (localPosition.y < -size.y/2) {normal = new Vector3(0,0,180); Debug.Log ("local < -y");}
		else if (localPosition.z > size.z/2) {normal = new Vector3(90,0,0); Debug.Log ("local > z");}
		else if (localPosition.z < -size.z/2) {normal = new Vector3(270,0,0); Debug.Log ("local < -z");}
		
		return normal;
	}
}
