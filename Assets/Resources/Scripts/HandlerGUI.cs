using UnityEngine;
using System.Collections;

public class HandlerGUI : MonoBehaviour {
	
	public string printString = null;
	public GameObject player;
	string sendString;
	int i = 0;
	bool newString = true;
	Rect toolbarRect;
	HandlerBuilding handlerBuilding;
	PlayerMovement playermovement;
	

	// Use this for initialization
	void Start () {
		//REMOVE BEFORE INTENSIVE TESTING!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		//Also turn vsync on, this is used so I can see the delay scripts create
		Application.targetFrameRate = 65;
		handlerBuilding = player.GetComponent<HandlerBuilding>();
		playermovement = player.GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	int lastSelected = -1;
	public int toolbarInt = -1; //Setting this to -1 means there is nothing selected when the toolbar loads
	string[] toolbarStrings = {"Interact", "Remove", "Deck Plating", "Wall Plating", "Equipment"}; //sets the position and name for the toolbar buttons from left to right.
	//GUIContent[] toolbarTooltip = {new GUIContent("Interact", "Interact"), new GUIContent ("Remove", "Remove"), new GUIContent ("Deck Plating", "Deck Plating"), new GUIContent ("Wall Plating", "Wall Plating"), new GUIContent ("Equipment", "Equipment")};
	
	public int materialbarInt = -1; //Setting this to -1 means there is nothing selected when the toolbar loads
	string[] materialbarStrings = {"Plating", "Glass"}; //sets the position and name for the toolbar buttons from left to right.
	public int equipmentbarInt = -1; //Setting this to -1 means there is nothing selected when the toolbar loads
	string[] equipmentbarStrings = {"Engine"}; //sets the position and name for the toolbar buttons from left to right.
	
	// OnGUI is called once per frame if a GUI element updates
	void OnGUI () {

		//Returns the int of the selected item in the Toolbar starting with 0 from left to right
		
		//Creates a toolbar with the selections toolbarStrings in the bottom right corner of the screen and will change position for screen sizes
		toolbarInt = GUI.Toolbar (new Rect(Screen.width - 500, Screen.height - 90,500,60), toolbarInt, toolbarStrings);
		
		//This will keep a player from clicking scenery when over a gui element
		//Only needs to run under the repaint event
		if (Event.current.type==EventType.Repaint)
		{
			//toolbarRect needs to be recreated every frame incase player changes screen size
			toolbarRect = new Rect(Screen.width - 500, Screen.height - 90,500,60);
			//if mouse is within this rectangle then disallow clicking of gameobjects
			if (toolbarRect.Contains(Event.current.mousePosition)) {
				handlerBuilding.mouseOverGUI = true;
			}
			else {
				handlerBuilding.mouseOverGUI = false;
			}
		}
		
		if (toolbarInt == 2 || toolbarInt == 3) {
			//Creates a toolbar with the selections materialbarStrings in the bottom right corner of the screen and will change position for screen sizes
			materialbarInt = GUI.Toolbar (new Rect(Screen.width - 300, Screen.height - 160,200,60), materialbarInt, materialbarStrings);
		}
		if (toolbarInt == 4) {
			//Creates a toolbar with the selections equipmentbarStrings in the bottom right corner of the screen and will change position for screen sizes
			equipmentbarInt = GUI.Toolbar (new Rect(Screen.width - 300, Screen.height - 160,100,60), equipmentbarInt, equipmentbarStrings);
		}
		
		if (toolbarInt != lastSelected) {
			playermovement.updateBuilders = true;
			lastSelected = toolbarInt;
		}
		
		//this will inform the player if they have dampening on/off
		if (playermovement.dampen)
			GUI.Label (new Rect(Screen.width/32, Screen.height/32,400,60), "Jetpack Hold Position: ON");
		else 
			GUI.Label (new Rect(Screen.width/32, Screen.height/32,400,60), "Jetpack Hold Position: OFF");
		
		//this will inform the player if they have magnetic boots on/off
		if (playermovement.bootsOn)
			GUI.Label (new Rect(Screen.width/32, Screen.height/32 + 20,400,60), "Magnetic Boots: ON");
		else 
			GUI.Label (new Rect(Screen.width/32, Screen.height/32 + 20,400,60), "Magnetic Boots: OFF");
		
		//When a message is loaded into printString display it for a few seconds
		if (printString != null) {
			if (newString) {
				sendString = printString;
				newString = false;
			}
			GUI.Label (new Rect(Screen.width/32, Screen.height/32 + 40,400,60), sendString);
			if (i < 600)
				i = i + 1;
			else {
				i = 0;
				if (sendString == printString){
					printString = null;
					
				} else {
					newString = true;	
				}
			}
		}
		
		if (GUI.Button(new Rect(Screen.width/32, Screen.height - 80,100,40), "Create Ship")) {
			
		}
	}
	
}
