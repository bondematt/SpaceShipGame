using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	
	//Variables that can be changed on inspector
	public float playerForceForward = 100.0F;
	public float playerForceVertical = 100.0F;
	public float playerForceHorizontal = 100.0F;
	public float playerTorqueVertical = 5f;
	public float playerTorqueHorizontal = 1f;
	public float playerTorqueRotation = 1f;
	public float playerRotationDampening = 10f;
	public float playerDriftDampening = 10f;
	public float playerGroundSpeed = 2f;
	public float playerJumpPower = 15f;
	
	public bool updateBuilders = false;
	public bool shipControl = false;
	public bool notAttached = true;
	public bool dampen = false;
	public bool bootsOn = false;
		
	//Initial script-wide scope variable declarations
	public RaycastHit hit;
	Transform cameraPlayer;
	int vertical = 0;
	int rotate = 0;
	int i = 0;
	float playerImpactSide = 0;
	float yAxis = 0F;
	GameObject attachedTo;
	bool wallRight = false;
	bool wallLeft = false;
	bool wallForward = false;
	bool wallBack = false;
	
	Vector3 velocity;
	Vector3 angularVel;
	Vector3 playerVelocity;
	Vector3 playerVelocityLast = new Vector3(0,0,0);
	Vector3 playerPosCurrent;
	
	BoxCollider shipCollider;
	
	// Use this for initialization
	void Start () {
		cameraPlayer = transform.Find("playerCamera"); //Get the player's camera gameobject
	}
	
	// Update is called once per frame
	void Update () {
		playerInput(); //run method to get players input
		
		//playerControlCheck(); //apply player inputs and check for collisions
	}
	
	// FixedUpdate is called once per average frame time. Not tied to current frame.
	void FixedUpdate () {		
		//runs if player is not attached to any floorTile or is attached and has turned their boots off
		if (notAttached || (!bootsOn && !notAttached)) {
			if (rigidbody.isKinematic == true) {
				//playerNotAttached(); //run method to detach player and apply rigidbody values
			}
			playerRigidbodyControl();
		}
	}
	
	/*
	public void playerControlCheck () {	
		// player transform and rotation controls if attached to floorTile
		if (!notAttached && !shipControl) {
			playerTransformControl();
		}
		
		//Keep the players rotation level with the ground.
		if (bootsOn) {
			checkForFloor();
		}
	}
	
	
	void checkForFloor() {
			Transform hitDown = raycast (transform.TransformPoint(0,-0.45f,0), -transform.up, .2f);
			
				//checks if ray hits floortile
				if (hitDown.collider != null) {
					if (hitDown.collider.transform.name == "floorTile"){ 
						//If the ray hits a floortile
						//Check for parent
						
						if (transform.parent != hitDown.collider.transform) {
							if (notAttached) { 
								float lookDir = transform.localEulerAngles.y; //get the players rotation about the y axis
						
								//get player position local to tile 
								Vector3 posCheck = hitDown.collider.transform.InverseTransformPoint(transform.position);
				
								//check if player's centerpoint is above or below the tile
								if (posCheck.y >= 0){
									playerImpactSide = 1;
									transform.localEulerAngles  = new Vector3 (0,0,0); //Sets player rotation towards up direction of tile
								} else {
									playerImpactSide = -1;
									transform.localEulerAngles  = new Vector3 (180,0,0); //Sets player rotation towards down direction of tile
								}
								
								transform.position = hitDown.collider.transform.TransformPoint(Vector3.up * playerImpactSide); //Place playes at middle of impacted tile on appropriate side
								transform.eulerAngles = (hitDown.collider.transform.eulerAngles * playerImpactSide); //Rotates player to exactly up or down
								
								transform.parent = hitDown.collider.transform; //Set tile as parent of player, keeps them rotated and the correct height above the tile
								
								//Sets player on correct side of tile after parenting
								if (playerImpactSide == 1){
									transform.localEulerAngles  = new Vector3 (0,lookDir,0);//Sets player rotation towards up direction of tile, looking the same direction they where
								} else {
									transform.localEulerAngles  = new Vector3 (180,lookDir,0);//Sets player rotation towards down direction of tile, looking the same direction they where
								}
					
								rigidbody.isKinematic = true;
								//Destroy(collider);
								notAttached = false;
								i = 0;
							} else {
								transform.parent = hitDown.collider.transform; //Set tile as parent of player
							}
						} else {
							i = 0;
						}
					} else {
						i = i + 1;
						//Needs to return nothing 6 times to count as not attached, helps avoid accidental detachement
						if (i > 5) {
							notAttached = true; //if object hit is not floorTile then the player is not attached
							i = 0;
						}
					}
				} else {
					i = i + 1;
					// Needs to return nothing 6+ times to count as not attached, helps avoid accidental detachement
					if (i > 5) {
						notAttached = true; //if object hit is not floorTile then the player is not attached
						i = 0;
					}
				}
	}
	
	
	Transform sweepForWalls(Vector3 dir, float dist) {
		Transform sweepTransform;
		
		if (rigidbody.SweepTest(dir, out hit, dist * 2)) {
			sweepTransform = hit.collider.transform;
			return sweepTransform;
		} else {
			sweepTransform = transform;
			return sweepTransform;
		}
	}
	
	Vector3 playerMoveVector;
	Transform objectHit;
	
	void playerTransformControl() {
		//create distance to be moved based on player input
		playerMoveVector = (Vector3.forward * Input.GetAxis("Vertical") * Time.deltaTime * playerGroundSpeed) + (Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime * playerGroundSpeed);
		//check if they impact anything
			objectHit = sweepForWalls(playerMoveVector, playerMoveVector.magnitude);
		
			//if they do make them stop at the point of contact
			if (objectHit == transform) {
				transform.Translate(playerMoveVector, Space.Self);
			}		
	}*/
	
	/*
	void playerTransformControl() {
		//Checks if the player is against a wall, if they are do not allow movement in that direction
		if ((!wallForward && !wallBack) || (wallForward && Input.GetAxis("Vertical") < 0) || (wallBack && Input.GetAxis("Vertical") > 0)){
			transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * Time.deltaTime * playerGroundSpeed, Space.Self); // uses input axis directly for movement
		}
			
		//Checks if the player is against a wall, if they are do not allow movement in that direction
		if ((!wallRight && !wallLeft) || (wallRight && Input.GetAxis("Horizontal") < 0) || (wallLeft && Input.GetAxis("Horizontal") > 0)){
			transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime * playerGroundSpeed, Space.Self);
		}
		
		//check if player has hit the jump key
		if (Input.GetKeyDown(KeyCode.Space)) {
			transform.Translate(Vector3.up * Time.deltaTime * playerJumpPower, Space.Self); //allows character to jump with Space
			//This needs to be rewritten into a more convincing jump, currently just moves player up as they hold space until they become a rigidbody
		}
		
		if (Screen.lockCursor){
			float xAxis = Input.GetAxis("Mouse X"); //get mouse movement
			yAxis += Input.GetAxis("Mouse Y") * 10; //get mouse movement
			yAxis = Mathf.Clamp(yAxis, -90f, 90f); //restrict camera pitch between -90 and 90 degrees
			transform.Rotate(0,xAxis * Time.deltaTime * 400f,0,Space.Self); //rotates player left/right
			cameraPlayer.localEulerAngles = new Vector3(-yAxis, 0, 0); //rotates camera pitch
		}
	}*/
	
	/*
	void checkForWalls() {
		//Keep the player from walking through walltiles using raycasting. Uses many rays at two different heights and every corner to check for collisions.
		
		//set wall position variables to false
		wallRight = false;
		wallLeft = false;
		wallForward = false;
		wallBack = false;
		
		//check middle right
		if (Physics.Raycast(transform.position, transform.right, out hit, .26f)) {
			if (hit.collider.transform.name == "wallTileX" || hit.collider.transform.name == "wallTileZ"){
				if (hit.distance < .25)
					transform.Translate(new Vector3 (-1 * (.25f - hit.distance),0,0),Space.Self);
				wallRight = true;
			}
		}
		
		//check forward right
		if (Physics.Raycast(transform.TransformPoint(0,0,.43f), transform.right, out hit, .26f)) {
			if (hit.collider.transform.name == "wallTileX" || hit.collider.transform.name == "wallTileZ"){
				if (hit.distance < .25)
					transform.Translate(new Vector3 (-1 * (.25f - hit.distance),0,0),Space.Self);
				wallRight = true;
			}
		}
		
		//check back right
		if (Physics.Raycast(transform.TransformPoint(0,0,-.43f), transform.right, out hit, .26f)) {
			if (hit.collider.transform.name == "wallTileX" || hit.collider.transform.name == "wallTileZ"){
				if (hit.distance < .25)
					transform.Translate(new Vector3 (-1 * (.25f - hit.distance),0,0),Space.Self);
				wallRight = true;
			}
		}
	
		//check middle right high
		if (Physics.Raycast(transform.TransformPoint(0,0.3f,0), transform.right, out hit, .26f)) {
			if (hit.collider.transform.name == "wallTileX" || hit.collider.transform.name == "wallTileZ"){
				if (hit.distance < .25)
					transform.Translate(new Vector3 (-1 * (.25f - hit.distance),0,0),Space.Self);
				wallRight = true;
			}
		}
		
		//check forward right high
		if (Physics.Raycast(transform.TransformPoint(0,0.3f,.43f), transform.right, out hit, .26f)) {
			if (hit.collider.transform.name == "wallTileX" || hit.collider.transform.name == "wallTileZ"){
				if (hit.distance < .25)
					transform.Translate(new Vector3 (-1 * (.25f - hit.distance),0,0),Space.Self);
				wallRight = true;
			}
		}
		
		//check back right high
		if (Physics.Raycast(transform.TransformPoint(0,0.3f,-.43f), transform.right, out hit, .26f)) {
			if (hit.collider.transform.name == "wallTileX" || hit.collider.transform.name == "wallTileZ"){
				if (hit.distance < .25)
					transform.Translate(new Vector3 (-1 * (.25f - hit.distance),0,0),Space.Self);
				wallRight = true;
			}
		}
		
		//check middle left
		if (Physics.Raycast(transform.position, -transform.right, out hit, .26f)) {
			if (hit.collider.transform.name == "wallTileX" || hit.collider.transform.name == "wallTileZ"){
				if (hit.distance < .25)
					transform.Translate(new Vector3 (.25f - hit.distance,0,0),Space.Self);
				wallLeft = true;
			}
		}
		
		//check forward left
		if (Physics.Raycast(transform.TransformPoint(0,0,.43f), -transform.right, out hit, .26f)) {
			if (hit.collider.transform.name == "wallTileX" || hit.collider.transform.name == "wallTileZ"){
				if (hit.distance < .25)
					transform.Translate(new Vector3 (.25f - hit.distance,0,0),Space.Self);
				wallLeft = true;
			}
		}
		
		//check back left
		if (Physics.Raycast(transform.TransformPoint(0,0,-.43f), -transform.right, out hit, .26f)) {
			if (hit.collider.transform.name == "wallTileX" || hit.collider.transform.name == "wallTileZ"){
				if (hit.distance < .25)
					transform.Translate(new Vector3 (.25f - hit.distance,0,0),Space.Self);
				wallLeft = true;
			}
		}
		
		//check middle left high
		if (Physics.Raycast(transform.TransformPoint(0,0.3f,0), -transform.right, out hit, .26f)) {
			if (hit.collider.transform.name == "wallTileX" || hit.collider.transform.name == "wallTileZ"){
				if (hit.distance < .25)
					transform.Translate(new Vector3 (.25f - hit.distance,0,0),Space.Self);
				wallLeft = true;
			}
		}
		
		//check forward left high
		if (Physics.Raycast(transform.TransformPoint(0,0.3f,.43f), -transform.right, out hit, .26f)) {
			if (hit.collider.transform.name == "wallTileX" || hit.collider.transform.name == "wallTileZ"){
				if (hit.distance < .25)
					transform.Translate(new Vector3 (.25f - hit.distance,0,0),Space.Self);
				wallLeft = true;
			}
		}
		
		//check back left high
		if (Physics.Raycast(transform.TransformPoint(0,0.3f,-.43f), -transform.right, out hit, .26f)) {
			if (hit.collider.transform.name == "wallTileX" || hit.collider.transform.name == "wallTileZ"){
				if (hit.distance < .25)
					transform.Translate(new Vector3 (.25f - hit.distance,0,0),Space.Self);
				wallLeft = true;
			}
		}
		
		//check middle forward
		if (Physics.Raycast(transform.position, transform.forward, out hit, .17f)) {
			if (hit.collider.transform.name == "wallTileX" || hit.collider.transform.name == "wallTileZ"){
				if (hit.distance < .16)
					transform.Translate(new Vector3 (0,0,-1 * (.16f - hit.distance)),Space.Self);
				wallForward = true;
			}
		}
		
		//check right forward
		if (Physics.Raycast(transform.TransformPoint(.43f,0,0), transform.forward, out hit, .17f)) {
			if (hit.collider.transform.name == "wallTileX" || hit.collider.transform.name == "wallTileZ"){
				if (hit.distance < .16)
					transform.Translate(new Vector3 (0,0,-1 * (.16f - hit.distance)),Space.Self);
				wallForward = true;
			}
		}
		
		//check left forward
		if (Physics.Raycast(transform.TransformPoint(-.43f,0,0), transform.forward, out hit, .17f)) {
			if (hit.collider.transform.name == "wallTileX" || hit.collider.transform.name == "wallTileZ"){
				if (hit.distance < .16)
					transform.Translate(new Vector3 (0,0,-1 * (.16f - hit.distance)),Space.Self);
				wallForward = true;
			}
		}
		
		//check middle forward high
		if (Physics.Raycast(transform.TransformPoint(0,0.3f,0), transform.forward, out hit, .17f)) {
			if (hit.collider.transform.name == "wallTileX" || hit.collider.transform.name == "wallTileZ"){
				if (hit.distance < .16)
					transform.Translate(new Vector3 (0,0,-1 * (.16f - hit.distance)),Space.Self);
				wallForward = true;
			}
		}
		
		//check right forward high
		if (Physics.Raycast(transform.TransformPoint(.43f,0.3f,0), transform.forward, out hit, .17f)) {
			if (hit.collider.transform.name == "wallTileX" || hit.collider.transform.name == "wallTileZ"){
				if (hit.distance < .16)
					transform.Translate(new Vector3 (0,0,-1 * (.16f - hit.distance)),Space.Self);
				wallForward = true;
			}
		}
		
		//check left forward high
		if (Physics.Raycast(transform.TransformPoint(-.43f,0.3f,0), transform.forward, out hit, .17f)) {
			if (hit.collider.transform.name == "wallTileX" || hit.collider.transform.name == "wallTileZ"){
				if (hit.distance < .16)
					transform.Translate(new Vector3 (0,0,-1 * (.16f - hit.distance)),Space.Self);
				wallForward = true;
			}
		}
		
		//check middle back
		if (Physics.Raycast(transform.position, -transform.forward, out hit, .17f)) {
			if (hit.collider.transform.name == "wallTileX" || hit.collider.transform.name == "wallTileZ"){
				if (hit.distance < .16)
					transform.Translate(new Vector3 (0,0,.16f - hit.distance),Space.Self);
				wallBack = true;
			}
		}
		
		//check right back
		if (Physics.Raycast(transform.TransformPoint(.43f,0,0), -transform.forward, out hit, .17f)) {
			if (hit.collider.transform.name == "wallTileX" || hit.collider.transform.name == "wallTileZ"){
				if (hit.distance < .16)
					transform.Translate(new Vector3 (0,0,.16f - hit.distance),Space.Self);
				wallBack = true;
			}
		}
		
		//check left back
		if (Physics.Raycast(transform.TransformPoint(-.43f,0,0), -transform.forward, out hit, .17f)) {
			if (hit.collider.transform.name == "wallTileX" || hit.collider.transform.name == "wallTileZ"){
				if (hit.distance < .16)
					transform.Translate(new Vector3 (0,0,.16f - hit.distance),Space.Self);
				wallBack = true;
			}
		}
		
		//check middle back high
		if (Physics.Raycast(transform.TransformPoint(0,0.3f,0), -transform.forward, out hit, .17f)) {
			if (hit.collider.transform.name == "wallTileX" || hit.collider.transform.name == "wallTileZ"){
				if (hit.distance < .16)
					transform.Translate(new Vector3 (0,0,.16f - hit.distance),Space.Self);
				wallBack = true;
			}
		}
		
		//check right back high
		if (Physics.Raycast(transform.TransformPoint(.43f,0.3f,0), -transform.forward, out hit, .17f)) {
			if (hit.collider.transform.name == "wallTileX" || hit.collider.transform.name == "wallTileZ"){
				if (hit.distance < .16)
					transform.Translate(new Vector3 (0,0,.16f - hit.distance),Space.Self);
				wallBack = true;
			}
		}
		
		//check left back high
		if (Physics.Raycast(transform.TransformPoint(-.43f,0.3f,0), -transform.forward, out hit, .17f)) {
			if (hit.collider.transform.name == "wallTileX" || hit.collider.transform.name == "wallTileZ"){
				if (hit.distance < .16)
					transform.Translate(new Vector3 (0,0,.16f - hit.distance),Space.Self);
				wallBack = true;
			}
		}
	}*/
	
	
	/*
	public void playerNotAttached() {
		notAttached = true; //Player is not attached
		shipControl = false; //Player cannot control ship
		FloatingOrigin.addObject(transform);
	
		//set player non-kinematic and give it the velocity of the ship he is leaving
		velocity = transform.root.rigidbody.GetPointVelocity(transform.position); //capture players velocity
		transform.parent = null; //remove parent
		rigidbody.isKinematic = false;
		rigidbody.velocity = velocity; //re-apply velocity
	
		cameraPlayer.rotation = transform.rotation; //Set camera to straight ahead view
	}*/
	
	void playerInput() {
		//Allow player to lock/unlock cursor from screen
		if (Input.GetKeyUp(KeyCode.LeftAlt)){
			if (Screen.lockCursor == true){
           		Screen.lockCursor = false;
				//HandlerBuilding.updateBuilders = true;
			} else {
				Screen.lockCursor = true;
			}
		}
		
		//Player toggles dampening with R
		if (Input.GetKeyUp(KeyCode.R)){
			if (dampen) {
				dampen = false;
			} else {
				dampen = true;
			}
		}
		
		//When player presses F it toggles his boots on and off
		if (Input.GetKeyUp(KeyCode.F)){
			if (bootsOn) {
				bootsOn = false;
			} else {
				bootsOn = true;
			}
		}
		
		//When player presses V toggle on shipcontrol only if the player is attached to a floortile
		if (Input.GetKeyUp(KeyCode.V) && !notAttached){
			if (shipControl) {
				shipControl = false;
			} else {
				shipControl = true;
			}
		}
		
		//Controls up/down movement
		vertical = 0;
		if (Input.GetKey(KeyCode.LeftShift)) {
			vertical = 1;
		} 
		if (Input.GetKey(KeyCode.LeftControl)) {
			vertical = -1;
		}
	
		//Controls the left/right rotation
		rotate = 0;
		if (Input.GetKey(KeyCode.E)) {
			rotate = -1;
		} 
		if (Input.GetKey(KeyCode.Q)) {
			rotate = 1;
		}
	}
	
	void playerRigidbodyControl() {
		//Player rigidbody controls
		rigidbody.AddRelativeTorque(0,0,rotate * rigidbody.mass * playerTorqueRotation); //left/right rotation
		rigidbody.AddForce(transform.up * vertical * playerForceVertical * rigidbody.mass); //up/down movement
		rigidbody.AddForce(transform.forward * Input.GetAxis("Vertical") * playerForceForward * rigidbody.mass); //forward/back movement
		rigidbody.AddForce(transform.right * Input.GetAxis("Horizontal") * playerForceHorizontal * rigidbody.mass); //left/right movement
		
		//If the player turns on the jetpack hold then turn drag and angular drag on for the player
		if (dampen) {
			rigidbody.drag = playerDriftDampening;
			rigidbody.angularDrag = playerRotationDampening;
		} else {
			rigidbody.drag = 0;
			rigidbody.angularDrag = 0;
		}
		
		//Controls x-axis and y-axis rotation for rigidbody
		if (Screen.lockCursor){ 
			float xAxis = Input.GetAxis("Mouse X");
			float yAxis = Input.GetAxis("Mouse Y");
			rigidbody.AddRelativeTorque(0,xAxis * rigidbody.mass * playerTorqueHorizontal,0);
			rigidbody.AddRelativeTorque(-yAxis * rigidbody.mass * playerTorqueVertical,0,0);
		}
	}
	
	//method called upon to cast rays in the directions specified in the call and to return the transform hit or null
	Transform raycast (Vector3 rayPosition, Vector3 rayDirection, float rayLength) {
		Transform hitTransform;
		//If the raycast hits something return the hit transform
		if (Physics.Raycast(rayPosition, rayDirection, out hit, rayLength)) {
			hitTransform = hit.collider.transform;
			return hitTransform;
		//if nothing is hit return player transform
		} else {
			hitTransform = transform;
			return hitTransform;
		}
	}
}