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
		Vector3 localEulerRotationUp = gameObject.GetComponent<HandlerShips>().ships.positionToTile.PositionToNormal(surfaceHit.point, surfaceHit.collider);
		transform.parent = rootParent;
		transform.localEulerAngles = localEulerRotationUp; //Currently rotates the player to a specific facing, make it use the way they are currently facing, but with the correct "up"
		transform.position = surfaceHit.point + (transform.up * 0.9f); //need to make this use the humanoids feet
		attached = true;
		moveHumanoid.Attach();
		rigidbody.isKinematic = true;
	}
	
	void Detach () {
		
	}
}
