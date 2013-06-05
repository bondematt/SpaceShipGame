using UnityEngine;
using System.Collections;

public class AttachToSurface : MonoBehaviour {
	
	bool attached = false;
	bool hitSurface = false;
	public bool attach = false;
	
	MoveHumanoid moveHumanoid;
	
	Transform surfaceToAttach;
	
	RaycastHit hitInfo;
	
	RaycastHit surfaceHitInfo;
	
	// Use this for initialization
	void Start () {
		moveHumanoid = transform.root.GetComponentInChildren<MoveHumanoid>();
	}
	
	// Update is called once per frame
	void Update () {
		if (attach && !attached) {
			surfaceHitInfo = CheckForSurface();
			if (hitSurface) {
				hitSurface = false;
				Attach(surfaceHitInfo);
			}
		} else if (!attach && attached) {
			Detach();
		} else if (attach && attached) {
			//check if we are still on a surface
		}
		Vector3 localEulerAngles = transform.localEulerAngles;
		Vector3 eulerAngles = transform.eulerAngles;
		//Debug.Log ("localEulerAngles: " + localEulerAngles.x + ", " + localEulerAngles.y + ", " + localEulerAngles.z + ", eulerAngles: "  +  + eulerAngles.x + ", " + eulerAngles.y + ", " + eulerAngles.z);
	}
	
	public void DoAttach () {
		attach = true;
	}
	
	public void CancelAttach () {
		attach = false;
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
	
	//set angle to upright from surface, set position to point of impact, set parent to root parent of surface hit
	void Attach (RaycastHit surfaceHit) {
		Transform rootParent = surfaceHit.collider.transform.root;
		Vector3 normalOfSurface = gameObject.GetComponent<HandlerShips>().ships.positionToTile.PositionToNormal(surfaceHit.point, surfaceHit.collider);
		Vector3 storeEuler = transform.localEulerAngles;
		transform.parent = rootParent;
		float playerFacingDirection = CorrectFacing(normalOfSurface);
		transform.localEulerAngles = normalOfSurface;
		transform.Rotate(Vector3.up, playerFacingDirection, Space.Self);
		Debug.Log ("Rotate by: " + playerFacingDirection + ", Original Y: " + storeEuler.y);
		transform.position = surfaceHit.point + (transform.up * 0.9f); //need to make this use the humanoids feet
		
		//apply rotation player had before attaching
		attached = true;
		moveHumanoid.Attach();
		rigidbody.isKinematic = true;
	}
	
	float CorrectFacing (Vector3 eulerRotation) {
		float facingRotation = 0f;
		
		if (eulerRotation == new Vector3(0,0,0)) 
		{
			facingRotation = transform.localEulerAngles.y;
		}
		else if (eulerRotation == new Vector3(0,0,180))
		{
			facingRotation = transform.localEulerAngles.y;
		}
		else if (eulerRotation == new Vector3(0,0,90))
		{
			facingRotation = 360 - transform.localEulerAngles.x;
		}
		else if (eulerRotation == new Vector3(0,0,270))
		{
			//Debug.Log("transform.localEulerAngles.x: " + transform.localEulerAngles.x);
			facingRotation = transform.localEulerAngles.x;
		}
		else if (eulerRotation == new Vector3(270,0,0))
		{
			//Debug.Log("transform.localEulerAngles.z: " + transform.localEulerAngles.z);
			facingRotation = 360 - transform.localEulerAngles.z;
		}
		else if (eulerRotation == new Vector3(90,0,0))
		{
			//Debug.Log("transform.localEulerAngles.z: " + transform.localEulerAngles.z);
			facingRotation = transform.localEulerAngles.z;
		}
		return facingRotation;
	}
	
	void Detach () {
		attached = false;
		moveHumanoid.Detach();
		rigidbody.isKinematic = false;
	}
}
