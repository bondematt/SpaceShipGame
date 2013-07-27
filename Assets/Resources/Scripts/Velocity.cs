using UnityEngine;
using System.Collections;

public class Velocity : MonoBehaviour {
	
	Vector3 lastPosition;
		
	Vector3 newPosition;
	
	private Vector3 _velocity;
	public Vector3 velocity
	{
	  get { return this._velocity; }
	  set { }
	}
	

	// Use this for initialization
	void Start () {
		lastPosition = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		newPosition = transform.position;
		_velocity = (newPosition - lastPosition)/Time.deltaTime;
		lastPosition = newPosition;
	}
}
