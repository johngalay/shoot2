using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour {
	/* 
	public int health = 100;
	public int yOffset = 20; //we'll use this to position the label 20px above the unit on the screen
	
	private Vector3 screenPos;
	
	void Update () {
		screenPos = Camera.main.WorldToScreenPoint(transform.position);
	}
	
	void OnGUI(){
		GUI.Label(Rect(screenPos.x, (screenPos.y + yOffset), 100, 25), health.ToString());
	}*/

	Vector3 iniRot;

	void Start() {
		iniRot = transform.eulerAngles;
	}

	void Update() {
		iniRot.y = transform.eulerAngles.y; // keep current rotation about Y
      	transform.eulerAngles = iniRot; // restore original rotation with new Y
	}
 
}
