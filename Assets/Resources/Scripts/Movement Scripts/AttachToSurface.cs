using UnityEngine;
using System.Collections;

public class AttachToSurface : MonoBehaviour {
	
	bool attached = false;
	bool hitSurface = false;
	public bool attach = false;
	bool attaching = false;
	
	Transform rootParent = null; //initialize rootParent
	
	MoveHumanoid moveHumanoid;
	
	Transform surfaceToAttach;
	
	RaycastHit hitInfo;
	
	RaycastHit surfaceHitInfo;
	
	BoxCollider hitCollider;
	
	Vector3 hitPosition = new Vector3();
	
	Vector3 normalOfSurfaceEuler = new Vector3(); //initialize normalOfSurface
	
	Vector3 normalOfSurfaceVector = new Vector3();
	
	Vector3 playerFinalPosition = new Vector3();
	
	float facingDirection = 0f;
	
	GameObject normalPoint;
	
	// Use this for initialization
	void Start () {
		moveHumanoid = transform.root.GetComponentInChildren<MoveHumanoid>();
		normalPoint = new GameObject();
		normalPoint.name = "Reference Point";
		normalPoint.SetActive(false);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		//Check if the humanoid should be attached
		if (attach && !attached) {
			//We are searching for a surface
			surfaceHitInfo = CheckForSurface();
			if (hitSurface) {
				//we found a surface and need to attach
				hitSurface = false;
				Attach(surfaceHitInfo);
			}
		} else if (!attach && attached) {
			//We no longer want to be attached to a surface
			Detach();
		} else if (attach && attached) {
			//check if we are still on the same surface
			surfaceHitInfo = CheckForSurface();
			if (hitSurface) {
				//check if we are on same surface
				
			} else {
				//No longer on a surface
				Detach();
			}
		}
		
		//If still attaching to an object continue doing so
		if (attaching) {
			RotateToNormal();
		}
	}
	
	public void ToggleAttach () {
		if (attach == true) 
		{
			attach = false;
			Debug.Log ("Magnetic Boots Off");
		}
		else 
		{
			attach = true;
			Debug.Log ("Magnetic Boots On");
		}
	}
	
	RaycastHit CheckForSurface () {
		hitSurface = false;
		if (Physics.Raycast(transform.position, -transform.up, out hitInfo, 1f)) {
			hitSurface = true;
			return hitInfo;
		} else {
			return hitInfo;	//returns a blank struct, essentially a null value.
		}
	}
		
	void Attach (RaycastHit surfaceHit) {
		rootParent = surfaceHit.collider.transform.root; //get highest level transform of the tile we hit
		moveHumanoid.Attached();
		attached = true;
		attaching = true;
		rigidbody.isKinematic = true;
		transform.parent = rootParent; //Set parent to the rootParent, so the humanoid now moves with the ship.
		if (surfaceHit.collider.GetType() == typeof(BoxCollider)) 
		{
			normalOfSurfaceEuler = gameObject.GetComponent<HandlerShips>().ships.positionToTile.PositionToNormalEuler(surfaceHit.point, surfaceHit.collider);
			normalOfSurfaceVector = gameObject.GetComponent<HandlerShips>().ships.positionToTile.PositionToNormalVector(surfaceHit.point, surfaceHit.collider);
			hitCollider = (BoxCollider) surfaceHit.collider;
			hitPosition = surfaceHit.point;
		}
	}
	
	/* Move player towards his final position determined by hit point and normal of surface
	 * 
	 * Get point in world space 1 meter in front of humanoid
	 * Move that point up to the players 'height' above the collider.
	 * 	- use collider.transform.InverseTransformPoint(pointDirection) 
	 *  - Modify that new vector3.y to set the height to the same as the players
	 *  - Then put that back into world space
	 * rotate player to look at that new point with normal as up vector
	 */
	void RotateToNormal () 
	{
		if (rootParent != null) 
		{
			playerFinalPosition = hitPosition + (normalOfSurfaceVector * .9f); //Get position where we want the player to be
			
			transform.position = Vector3.Lerp(transform.position, playerFinalPosition, .05f); //Gradually move player towards this point
			
			//set reference object at position of player with the rotation of the normal
			normalPoint.SetActive(true);
			
			normalPoint.transform.parent = rootParent;
			normalPoint.transform.position = transform.position;
			normalPoint.transform.localRotation = Quaternion.Euler (normalOfSurfaceEuler);
			normalPoint.transform.parent = null;
			
			Vector3 pointDirection = transform.forward + transform.position; //world position of point 1 meter in front of player
			
			Vector3 offsetLocal = normalPoint.transform.InverseTransformPoint(pointDirection); //get point in reference point's local space
			
			offsetLocal.y = Mathf.Lerp(offsetLocal.y, 0f, .05f); //set height in local space to same as player's
			
			pointDirection = normalPoint.transform.TransformPoint(offsetLocal); //put back into world space
			
			transform.LookAt(pointDirection, normalPoint.transform.up); //have player look at point with the correct up normal
			
			if (offsetLocal.y == 0f && playerFinalPosition == transform.position) {
				attaching = false; //stop attaching as we are now attached
				normalPoint.SetActive(false); //deactivate reference point as it is no longer needed
			}
		} else 
		{
			attaching = false;
			normalOfSurfaceEuler = new Vector3();
		}
	}
	
	void Detach () {
		attached = false;
		moveHumanoid.Detached();
		transform.parent = null;
		rigidbody.isKinematic = false;
	}
}
