// FloatingOrigin.cs
// Written by Peter Stirling
// 11 November 2010
// Uploaded to Unify Community Wiki on 11 November 2010
// URL: http://www.unifycommunity.com/wiki/index.php?title=Floating_Origin
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
 
public class floatingOrigin : MonoBehaviour
{
    public float threshold = 1000.0f;
    public float physicsThreshold = 10000.0f; // Set to zero to disable
    public float defaultSleepVelocity = 0.14f;
    public float defaultAngularVelocity = 0.14f;
	GameObject player;
	public Object[] objects;
	Object[] objectsParticle;
	Object[] objectsRigidbody;
	PlayerMovement playerMovement;
	List<Object> objectsList = new List<Object>();
	
	Vector3 cameraPosition;
	Vector3 cameraRotation;
	
	void Start() {
		player = GameObject.Find("player");
		playerMovement = player.GetComponent<PlayerMovement>();
		//Invoke("updateObjects", .5f);
		updateObjects();
	}
	
	//Get all objects in the scene
	void updateObjects() {
		objects = FindObjectsOfType(typeof(Transform));
		//objectsParticle = FindObjectsOfType(typeof(ParticleEmitter)); //unused
		objectsRigidbody = FindObjectsOfType(typeof(Rigidbody));
	}
	
	public void removeObject(Object o) {
		objectsList = objects.ToList();
		objectsList.Remove(o);
		objects = objectsList.ToArray();
	}
	
	public void addObject(Object o) {
		objectsList = objects.ToList();
		if (!(objectsList.Contains(o))) {
			objectsList.Add(o);
			objects = objectsList.ToArray();
		}
	}
	
    void LateUpdate()
    {
		moveToOrigin();
    }
	
	void FixedUpdate()
	{
		moveToOrigin();
	}

	void moveToOrigin() {
		cameraPosition = transform.root.rigidbody.worldCenterOfMass; //capture position of player's or player parent's center of mass
		if (cameraPosition.magnitude > threshold) {
			//cameraRotation = transform.root.eulerAngles; //capture rotation of player or player's parent
			transform.root.rigidbody.angularVelocity = Vector3.zero;
			
			//transform.root.eulerAngles = new Vector3 (0,0,0); //set player's or player parent's rotation to zero
				
			//transform.root.rigidbody.velocity = Quaternion.Euler(-cameraRotation.x, -cameraRotation.y, -cameraRotation.z) * transform.root.rigidbody.velocity; //modify the velocity of the rigidbody so they continue in the same direction after rotating them
			
			//Move the player or their parent back to the origin taking the world with them
	        foreach(Object o in objects)
	        {
				if (o != null) {
	                Transform t = (Transform)o;
					
	                if (t.parent == null) //we only want to move objects that have no parent
	                {
	                    t.position -= cameraPosition; //move
						/*if (t != transform.root) { //if the object is not the player rotate them around the player(who is now at the origin) by the amount the player had rotated before being set back to zero
							t.RotateAround(Vector3.zero, transform.root.right, -cameraRotation.x);
							t.RotateAround(Vector3.zero, transform.root.up, -cameraRotation.y);
							t.RotateAround(Vector3.zero, transform.root.forward, -cameraRotation.z);
							if (t.rigidbody != null) {
								t.root.rigidbody.velocity = Quaternion.Euler(-cameraRotation.x, -cameraRotation.y, -cameraRotation.z) * t.root.rigidbody.velocity; //modify the velocity of the rigidbody so they continue in the same direction after rotating them
								t.root.rigidbody.angularVelocity = Vector3.zero;
							}
						}*/
						
	                } else {
						removeObject(o); //remove objects that we don't want
					}
				} else {
					removeObject(o); //remove objects that we don't want
				}
	        }
		}
 
		/* unused
        if (physicsThreshold >= 0f)
        {
            foreach (Object o in objectsRigidbody)
            {
				if (o != null) {
                    Rigidbody r = (Rigidbody)o;
					
					
                    if (r.gameObject.transform.position.magnitude > physicsThreshold)
                    {
                        r.sleepAngularVelocity = float.MaxValue;
                        r.sleepVelocity = float.MaxValue;
                    }
                    else
                    {
                        r.sleepAngularVelocity = defaultSleepVelocity;
                        r.sleepVelocity = defaultAngularVelocity;
                    }
				} else {
					removeObject(o);
				}

            }
        }*/
	}
}