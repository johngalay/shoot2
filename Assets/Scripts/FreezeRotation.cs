using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FreezeRotation : MonoBehaviour {

	private Vector3 iniRot;

	void Start() {
		iniRot = transform.eulerAngles;
	}

	void Update() {
		iniRot.y = transform.eulerAngles.y; // keep current rotation about Y
      	transform.eulerAngles = iniRot; // restore original rotation with new Y
	}
 
}
