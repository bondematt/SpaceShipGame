using UnityEngine;
using System.Collections;

public class HandlerBuilding : MonoBehaviour {
	
	public float buildTimeModifier = 1f;
	public float buildRangeMax = 2f;
	public float buildRangeMin = .5f;
	public float updateRange = 3f;
	
	public bool isBuilding = false;
	public bool updateBuilders = false;
	public bool updateBuilderFloor = false;
	public bool updateBuilderWall = false;
	public bool mouseDown = false;
	public bool mouseOverGUI = false;
	
	public Transform hitTransform;
	
	public Camera cameraPlayer;
	
	int i;
	int ii;
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (updateBuilders) {
			if (ii > 5) {
				updateBuilders = false;
				ii = 0;
			} else {
				ii += 1;
			}
		}
		
		//check if the mouse is over the UI
		GetMouseInfo();
		
		//Make it so fire1 can only be done once every 5 frames
		if (mouseDown) {
			if (i < 5) {
			 i = i + 1;	
			} else {
				i = 0;
				mouseDown = false;
			}
		}
	}
	
	//check if the mouse is over the UI
	void GetMouseInfo()
    {
        Ray ray = cameraPlayer.ScreenPointToRay(Input.mousePosition); //set ray direction
        RaycastHit hit;
		
		//if the ray hits something get its transform
        if (Physics.Raycast(ray,out hit, buildRangeMax + .5f))
        {
			hitTransform = hit.collider.transform;
		} 
		else {
			hitTransform = null;
		}
    }
}
