using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float moveSpeed = 3;

	private Rigidbody2D myRigidbody;
	private Camera viewCamera;
	private Vector2 velocity;


	void Start () {
		myRigidbody = GetComponent<Rigidbody2D>();
		viewCamera = Camera.main;
	}
	
	void Update () {
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
}
