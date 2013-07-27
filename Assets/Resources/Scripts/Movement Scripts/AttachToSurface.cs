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
	
	Collider attachedTo;
	
	Velocity velocity;
	
	public float moveSpeed = 1f;
	
	public float rotateSpeed = 1f;
	
	// Use this for initialization
	void Start () {
		moveHumanoid = transform.root.GetComponentInChildren<MoveHumanoid>();
		velocity = transform.root.GetComponentInChildren<Velocity>();
		normalPoint = new GameObject();
		normalPoint.name = "Reference Point";
		normalPoint.SetActive(false);
	}
	
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
			if (attaching) {
				surfaceHitInfo = CheckForSurface(hitCollider.transform.TransformDirection(-normalOfSurfaceVector), 2f);
			} else {
				surfaceHitInfo = CheckForSurface();
			}
			if (hitSurface) {
				//check if we are on same surface
				
			} else {
				//No longer on a surface
				Detach();
			}
		}
		
		//If still attaching to an object continue doing so
		if (attaching) {
			RotateToNormalLookAt();
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
	
	RaycastHit CheckForSurface (Vector3 angle, float length) {
		hitSurface = false;
		if (Physics.Raycast(transform.position, angle, out hitInfo, length)) {
			hitSurface = true;
			return hitInfo;
		} else {
			return hitInfo;	//returns a blank struct, essentially a null value.
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
		moveHumanoid.Attached(true);
		moveHumanoid.Attaching(true);
		attached = true;
		attaching = true;
		rigidbody.isKinematic = true;
		transform.parent = rootParent; //Set parent to the rootParent, so the humanoid now moves with the ship.
		if (surfaceHit.collider.GetType() == typeof(BoxCollider)) 
		{
			normalOfSurfaceEuler = PositionToTile.PositionToNormalEuler(surfaceHit.point, surfaceHit.collider);
			normalOfSurfaceVector = PositionToTile.PositionToNormalVector(surfaceHit.point, surfaceHit.collider);
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
	void RotateToNormalLookAt () 
	{
		if (rootParent != null) 
		{
			playerFinalPosition = hitPosition + (normalOfSurfaceVector * .9f); //Get position where we want the player to be

			transform.position = Vector3.MoveTowards(transform.position, playerFinalPosition, moveSpeed * Time.deltaTime);
			
			//set reference object at position of player with the rotation of the normal
			normalPoint.SetActive(true);
			
			normalPoint.transform.parent = rootParent;
			normalPoint.transform.position = transform.position;
			normalPoint.transform.localRotation = Quaternion.Euler (normalOfSurfaceEuler);
			normalPoint.transform.parent = null;
			
			Vector3 pointDirection = transform.forward + transform.position; //world position of point 1 meter in front of player
			
			Vector3 offsetLocal = normalPoint.transform.InverseTransformPoint(pointDirection); //get point in reference point's local space
			
			offsetLocal.y = 0f; //set height in local space to same as player's
			
			pointDirection = normalPoint.transform.TransformPoint(offsetLocal); //put back into world space
			
			normalPoint.transform.LookAt(pointDirection, normalPoint.transform.up); //have reference point look at pointDirection with the correct up normal
			
			normalPoint.transform.parent = rootParent;
			
			Quaternion finalRotation = normalPoint.transform.localRotation;
			
			transform.localRotation =  Quaternion.RotateTowards(transform.localRotation ,finalRotation, rotateSpeed);
			
			normalPoint.transform.parent = null;

			if (Quaternion.Angle(transform.localRotation, finalRotation) < .1f && (playerFinalPosition - transform.position).magnitude <= 0.001f) {
				attaching = false; //stop attaching as we are now attached
				moveHumanoid.Attaching(false);
				normalPoint.SetActive(false); //deactivate reference point as it is no longer needed
			}
		} else 
		{
			attaching = false;
			moveHumanoid.Attaching(false);
			normalOfSurfaceEuler = new Vector3();
			normalOfSurfaceVector = new Vector3();
			attachedTo = (Collider) hitCollider;
		}
	}
	
	void Detach () {
		attached = false;
		attaching = false;
		moveHumanoid.Attached(false);
		transform.parent = null;
		rootParent = null;
		rigidbody.isKinematic = false;
		rigidbody.velocity = velocity.velocity;
	}
}
