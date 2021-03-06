using UnityEngine;
using System.Collections;

public class StartGameMenu : MonoBehaviour {
	
	public static int menuLevel = 0;
	
	public static float mouseSensitivity = 1f;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (transform.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public static void OpenMenu (int MenuNumber) {
		menuLevel = MenuNumber;
	}
	
	void OnGUI () {
		//Temporary label to show player's controls
		
		GUI.Label(new Rect (10 , 10, 300, 100), "Default Controls: WASD for movement \nShift and Control for up and down \nQE and Mouse to rotate \nZ toggles jetpack position holding \nX toggles magnetic boots \nESC for menu");
		
		switch (menuLevel)
		{
		case -1: {
			Time.timeScale = 1;
			break;
		}
		case 0: 
			Time.timeScale = .00001f;
			if (Application.loadedLevelName == "NewGame") {
				if (GUI.Button(new Rect(Screen.width/2 - Screen.width/8, Screen.height/2 - (Screen.height/20 * 4) - 10, Screen.width/8, Screen.height/10), "Resume Game")) {	
					menuLevel = -1;
				}
			}
	        if (GUI.Button(new Rect(Screen.width/2 - Screen.width/8, Screen.height/2 - (Screen.height/20 * 2), Screen.width/8, Screen.height/10), "Start New Game")) {
				Application.LoadLevel("NewGame");
				menuLevel = -1;
			}
			if (GUI.Button(new Rect(Screen.width/2 - Screen.width/8, Screen.height/2 + 10, Screen.width/8, Screen.height/10), "Options")) {
				menuLevel = 1;
			}
			if (GUI.Button(new Rect(Screen.width/2 - Screen.width/8, Screen.height/2 + (Screen.height/20 * 2) + 20, Screen.width/8, Screen.height/10), "Quit Game")) {
				Application.Quit();
			}
	        break;
		case 1:
			Time.timeScale = .00001f;
			mouseSensitivity = GUI.HorizontalSlider (new Rect (Screen.width/2 - Screen.width/8 , Screen.height/2 - Screen.height/10, Screen.width/8, Screen.height/10), mouseSensitivity, 0.01f, 2.00f);
			GUI.Label(new Rect (Screen.width/2 - Screen.width/8 , Screen.height/2 - Screen.height/10 + 10, Screen.width/8, Screen.height/10), "Mouse Sensitivity: " + mouseSensitivity.ToString("f2"));
			if (GUI.Button(new Rect(Screen.width/2 - Screen.width/8, Screen.height/2 + (Screen.height/20 * 2) + 20, Screen.width/8, Screen.height/10), "Main Menu")) {
				menuLevel = 0;
			}
			break;
			
		}
		
	}
}
