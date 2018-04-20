using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

<<<<<<< HEAD
	public float moveSpeed = 3;
<<<<<<< HEAD
	private float startMoveSpeed;
	[SerializeField] private bool isRunning = false;
	[SerializeField] private bool isMoving = false;
=======
>>>>>>> a5bc8cd111e368efae651365d862ad46dbd60750

=======
	public float moveSpeed;
>>>>>>> parent of 6df1021... Multiplayer
	private Rigidbody2D myRigidbody;
	private Vector2 moveInput;
	private Vector2 moveVelocity;

	private Camera mainCamera;

	public GunController theGun;

	//public Texture2D cursorTexture;
    //public CursorMode cursorMode = CursorMode.Auto;
    //public Vector2 hotSpot = Vector2.zero;


	void Start () {
<<<<<<< HEAD
<<<<<<< HEAD
		startMoveSpeed = moveSpeed;
=======
>>>>>>> a5bc8cd111e368efae651365d862ad46dbd60750
		myRigidbody = GetComponent<Rigidbody2D>();
		viewCamera = Camera.main;
=======
		// Gets the component type rigidbody attached to the script.
		myRigidbody = GetComponent<Rigidbody2D>(); 
		mainCamera = FindObjectOfType<Camera>();
		//OnMouseEnter();
>>>>>>> parent of 6df1021... Multiplayer
	}

	void Update () {
<<<<<<< HEAD
<<<<<<< HEAD
		
		if(isRunning) {
			moveSpeed = 7.5f;
		} else {
			moveSpeed = startMoveSpeed;
=======
		moveInput = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));

		moveVelocity = moveInput * moveSpeed;
		characterRotation();
	}

	void FixedUpdate () {
		myRigidbody.velocity = moveVelocity;
		if(theGun.isFiring) {
			myRigidbody.velocity = GunRecoil();
>>>>>>> parent of 6df1021... Multiplayer
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

<<<<<<< HEAD
=======
		Vector3 pointToLook = viewCamera.ScreenToWorldPoint(Input.mousePosition);
		Vector2 direction = new Vector2(
			pointToLook.x - transform.position.x,
			pointToLook.y - transform.position.y
		);
		transform.up = direction; 
		velocity = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical")).normalized * moveSpeed;
	}

	void FixedUpdate() {
		myRigidbody.MovePosition(myRigidbody.position + velocity * Time.fixedDeltaTime);
	}

	public Transform firePoint;
	[SerializeField] private float shootWaitTime = 0.5f;
	private float timeLeft = 0f;
	public AudioSource gunSound;
	public MoveTrail bullet;
	[HideInInspector] public float distanceToTravel;

	void LateUpdate() {
		timeLeft -= Time.deltaTime;
		if(timeLeft < 0) {
			timeLeft = 0;
		}

		if(Input.GetButton("Fire1") && timeLeft <= 0){
			Shoot();
			timeLeft = shootWaitTime;
		}
	}

	void Shoot() {
		Vector3 pointToLook = viewCamera.ScreenToWorldPoint(Input.mousePosition);
		Vector2 direction = new Vector2(
			pointToLook.x - transform.position.x,
			pointToLook.y - transform.position.y
		);

		gunSound.Play();
		
		RaycastHit2D hit = Physics2D.Raycast(firePoint.position, direction);

		Effect();

		distanceToTravel = hit.distance;
		if(hit.collider != null) {
			Debug.Log("We hit " + hit.collider.name + " Distance: " + hit.distance);
		}
	}

	void Effect() {
		MoveTrail newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as MoveTrail;
	}
>>>>>>> a5bc8cd111e368efae651365d862ad46dbd60750
=======
	/*
	void OnMouseEnter () {
		Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
	}
	*/
>>>>>>> parent of 6df1021... Multiplayer
}
