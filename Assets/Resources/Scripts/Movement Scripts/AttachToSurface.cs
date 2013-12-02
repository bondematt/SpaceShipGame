using UnityEngine;
using System.Collections;

public class AttachToSurface : MonoBehaviour {
	
	public bool attach = false;
	
	public float moveSpeed = 1f;
	
	public float rotateSpeed = 1f;
	
	public bool attached = false;

	bool hitSurface = false;
	
	public bool attaching = false;
	
	Transform rootParent = null; //initialize rootParent
	
	Transform surfaceToAttach;
	
	RaycastHit hitInfo;
	
	RaycastHit surfaceHitInfo;
	
	Collider hitCollider;
	
	Collider attachedTo;
	
	Vector3 hitPosition = new Vector3();
	
	Vector3 normalOfSurfaceEuler = new Vector3(); //initialize normalOfSurface
	
	Vector3 normalOfSurfaceVector = new Vector3();
	
	Vector3 playerFinalPosition = new Vector3();
	
	float facingDirection = 0f;
	
	GameObject normalPoint;
	
	Velocity velocity;
	
	// Use this for initialization
	void Start () {
		velocity = transform.root.GetComponentInChildren<Velocity>();
		normalPoint = new GameObject();
		normalPoint.name = "Reference Point";
		normalPoint.SetActive(false);
	}
	
	void FixedUpdate () {
		//Check if the humanoid should be attached
		checkForAttach();
		
		//If still attaching to an object continue doing so
		if (attaching) {
			RotateToNormalLookAt();
		}
	}
	
	public void checkForAttach () {
		//Check if the humanoid should be attached
		if (attach && !attached) {
			//We are searching for a surface
			surfaceHitInfo = Sweeptests.Sweeptest(rigidbody, -transform.up, .1f);
			
			if (surfaceHitInfo.collider != null) {
				//we found a surface and need to attachhitSurface = false;
				Attach(surfaceHitInfo);
			}
		} else if (!attach && attached) {
			//We no longer want to be attached to a surface
			Detach();
		} else if (attach && attached) {
			//check if we are still on the same surface
			if (attaching) {
				surfaceHitInfo = Sweeptests.Sweeptest(rigidbody, -transform.up, .2f);
			} else {
				surfaceHitInfo = Sweeptests.Sweeptest(rigidbody, -transform.up, .1f);
			}
			if (surfaceHitInfo.collider != null) {
				//check if we are on same surface
				
			} else {
				//No longer on a surface
				Detach();
			}
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
	
	void Attach (RaycastHit surfaceHit) {
		rootParent = surfaceHit.collider.transform.root; //get highest level transform of the tile we hit
		attached = true;
		attaching = true;
		rigidbody.isKinematic = true;
		rigidbody.detectCollisions = false;
		transform.parent = rootParent; //Set parent to the rootParent, so the humanoid now moves with the ship.
		if (surfaceHit.collider.GetType() == typeof(BoxCollider)) 
		{
			normalOfSurfaceVector = PositionToTile.PositionToNormalVector(surfaceHit.point, surfaceHit.collider);
			hitCollider = (BoxCollider) surfaceHit.collider;
			hitPosition = surfaceHit.point;
		}
		if (surfaceHit.collider.GetType() == typeof(MeshCollider)) 
		{
			normalOfSurfaceVector = Normals.Normal(surfaceHit);
			hitCollider = (MeshCollider) surfaceHit.collider;
			hitPosition = surfaceHit.point;
		}
	}
	
	void RotateToNormalLookAt () 
	{
		attaching = RotateAndPositions.RotateAndPositionFacingForward(transform, normalPoint, hitPosition, normalOfSurfaceVector, moveSpeed, rotateSpeed);
		if (!attaching) {
			attachedTo = (Collider) hitCollider;
		}
	}
	
	void Detach () {
		attached = false;
		attaching = false;
		transform.parent = null;
		rootParent = null;
		rigidbody.isKinematic = false;
		rigidbody.detectCollisions = true;
		rigidbody.velocity = velocity.velocity;
	}
}
