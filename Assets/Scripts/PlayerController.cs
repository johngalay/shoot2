using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float moveSpeed = 3;
	private float startMoveSpeed;
	[SerializeField] private bool isRunning = false;
	[SerializeField] private bool isMoving = false;

	private Rigidbody2D myRigidbody;
	private Camera viewCamera;
	private Vector2 velocity;


	void Start () {
		startMoveSpeed = moveSpeed;
		myRigidbody = GetComponent<Rigidbody2D>();
		viewCamera = Camera.main;
	}
	
	void Update () {
		
		if(isRunning) {
			moveSpeed = 7.5f;
		} else {
			moveSpeed = startMoveSpeed;
		}

		Vector3 pointToLook = viewCamera.ScreenToWorldPoint(Input.mousePosition);
		Vector2 direction = new Vector2(
			pointToLook.x - transform.position.x,
			pointToLook.y - transform.position.y
		);
		transform.up = direction; 
		velocity = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical")).normalized * moveSpeed;
		if(Mathf.Approximately(velocity.x, 0) && Mathf.Approximately(velocity.y, 0)) {
			isMoving = false;
		} else {
			isMoving = true;
		}
		
		if(isRunning && !isMoving) {
			isRunning = false;
		}
		if(Input.GetKey(KeyCode.LeftShift)) {
			isRunning = true;
		}
	}

	void FixedUpdate() {
		myRigidbody.MovePosition(myRigidbody.position + velocity * Time.fixedDeltaTime);
	}


}
