using UnityEngine;
using System.Collections;

public class TitleBehavior : MonoBehaviour {
	static string code = "";
	// Use this for initialization
	void Start () {
		//wow, we can't do anything at all without some state!
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnGUI () {
		// Make a background box
		GUI.Box(new Rect(500,100,120,160), "Selection");

		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(520,140,80,20), "Generate")) {
			code = "tail";
			Application.LoadLevel(2);
		}

		// Make the second button.
		if(GUI.Button(new Rect(520,170,80,20), "Enter Code")) {
			GUI.TextArea(new Rect(20,70,80,20), "");
		}
		if(GUI.Button(new Rect(520,200,80,20), "Exit")) {
			Application.LoadLevel(3);
		}
		//GUI.
	}
}
