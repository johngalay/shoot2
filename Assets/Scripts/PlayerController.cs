using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	private Rigidbody2D myRigidbody;
	private Vector2 moveInput;
	private Vector2 moveVelocity;

	private Camera mainCamera;

	public GunController theGun;

	//public Texture2D cursorTexture;
    //public CursorMode cursorMode = CursorMode.Auto;
    //public Vector2 hotSpot = Vector2.zero;


	void Start () {
		// Gets the component type rigidbody attached to the script.
		myRigidbody = GetComponent<Rigidbody2D>(); 
		mainCamera = FindObjectOfType<Camera>();
		//OnMouseEnter();
	}

	void Update () {
		moveInput = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));

		moveVelocity = moveInput * moveSpeed;
		characterRotation();
	}

	void FixedUpdate () {
		myRigidbody.velocity = moveVelocity;
		if(theGun.isFiring) {
			myRigidbody.velocity = GunRecoil();
		}
	}

	void characterRotation () {
		Vector3 pointToLook = mainCamera.ScreenToWorldPoint(Input.mousePosition);
		Vector2 direction = new Vector2(
			pointToLook.x - transform.position.x,
			pointToLook.y - transform.position.y
		);
		transform.up = direction;
	}

	Vector2 GunRecoil() {
		Vector3 pointToLook = mainCamera.ScreenToWorldPoint(Input.mousePosition);
		Vector2 direction = new Vector2(
			pointToLook.x - transform.position.x,
			pointToLook.y - transform.position.y
		);
		return moveVelocity = -direction * theGun.recoil;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		Debug.Log("Player in contact with something.");
	}

	/*
	void OnMouseEnter () {
		Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
	}
	*/
}
