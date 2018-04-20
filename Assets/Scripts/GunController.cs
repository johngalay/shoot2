using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

	public Transform firePoint;
	[SerializeField] private float shootWaitTime = 0.5f;
	private float timeLeft = 0f;
	public AudioSource gunSound;
	public MoveTrail bullet;
	[HideInInspector] public float distanceToTravel;
	public bool isShooting = false;
	[SerializeField] private int gunDamage = 10;

	private Rigidbody2D myRigidbody;
	private Camera viewCamera;

	private PhotonView myPhotonView;
	private float lastShootTime;
	private int lastProjectileId;
	

	void Start () {
		myPhotonView = GetComponent<PhotonView>();
		myRigidbody = GetComponent<Rigidbody2D>();
		viewCamera = Camera.main;
	}
	/* 

	void LateUpdate() {
		timeLeft -= Time.deltaTime;
		if(timeLeft < 0) {
			timeLeft = 0;
		}

		if(Input.GetButton("Fire1") && timeLeft <= 0){
			Shoot();
			timeLeft = shootWaitTime;
		}
	}*/
	void Update() {
		isShooting = false;
		if(Input.GetButton("Fire1")){
			isShooting = true;
		}
	}
	void LateUpdate() {
		if(isShooting) {
			UpdateShoot();
		}
	}

	// outdated
	/*
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

		if(hit.collider != null && 
		(hit.collider.gameObject.layer == LayerMask.NameToLayer("Obstacle") || 
		hit.collider.gameObject.layer == LayerMask.NameToLayer("Target"))) {
			Debug.Log("We hit " + hit.collider.name + " Distance: " + hit.distance);
		}
	}

	void Effect() {
		MoveTrail newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as MoveTrail;
	}
	*/

	void UpdateShoot() {
		if(myPhotonView.isMine == false) {
			return;
		}
		if(isShooting == false) {
			return;
		}
		if(Time.realtimeSinceStartup - lastShootTime < shootWaitTime) {
			return;
		}
		lastProjectileId++;
		if(PhotonNetwork.offlineMode == true) {
			Debug.Log("Offline");
		}
		else {
			myPhotonView.RPC("OnShoot",
							PhotonTargets.All,
							new object[] {firePoint.position,
										firePoint.rotation,
										lastProjectileId,
										GetDirection(),
										firePoint.position,
										distanceToTravel,
										gunDamage
										});
		}	
	}

	public void CreateProjectile(Vector3 spawnPosition, Quaternion spawnRotation, double timestamp, int projectileId) {
		lastShootTime = Time.realtimeSinceStartup;
		MoveTrail newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as MoveTrail;
		newBullet.creationTime = timestamp;
		newBullet.startPosition = spawnPosition;
		newBullet.projectileId = projectileId;
	}

	public Vector2 GetDirection() {
		Vector3 pointToLook = viewCamera.ScreenToWorldPoint(Input.mousePosition);
		Vector2 direction = new Vector2(
			pointToLook.x - transform.position.x,
			pointToLook.y - transform.position.y
		);	
		return direction;
	}

	public void DetectTarget(Vector2 direction, Vector3 startRay, float distanceToTravel, int gunDamage) {
		RaycastHit2D hit = Physics2D.Raycast(startRay, direction);
		distanceToTravel = hit.distance;
		if(hit.collider != null && 
		(hit.collider.gameObject.layer == LayerMask.NameToLayer("Obstacle") || 
		hit.collider.gameObject.layer == LayerMask.NameToLayer("Target"))) {

			Debug.Log("We hit " + hit.collider.name + " Distance: " + hit.distance);

			if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Target")) {
				Debug.Log("Collider was a target");
				hit.collider.gameObject.GetComponent<PhotonView>().RPC("Damage",
																	PhotonTargets.OthersBuffered,
																	gunDamage
																	);
			}
		}
	}

	// outdated
	/*
	public void DetectTarget2() {
		Vector3 pointToLook = viewCamera.ScreenToWorldPoint(Input.mousePosition);
		Vector2 direction = new Vector2(
			pointToLook.x - transform.position.x,
			pointToLook.y - transform.position.y
		);		
		RaycastHit2D hit = Physics2D.Raycast(firePoint.position, direction);
		distanceToTravel = hit.distance;
		if(hit.collider != null && 
		(hit.collider.gameObject.layer == LayerMask.NameToLayer("Obstacle") || 
		hit.collider.gameObject.layer == LayerMask.NameToLayer("Target"))) {
			Debug.Log("We hit " + hit.collider.name + " Distance: " + hit.distance);
			if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Target")) {
				Debug.Log("Collider was a target");
				hit.collider.gameObject.GetComponent<PhotonView>().RPC("Damage",
																	PhotonTargets.AllBuffered,
																	new object[] {gunDamage
																	});
			}
		}
	}
	*/
	

	[PunRPC]
	public void OnShoot(Vector3 spawnPosition, Quaternion spawnRotation, int projectileId, 
						Vector2 direction, Vector3 startRay, float distanceToTravel, int gunDamage,
						PhotonMessageInfo info) {
		gunSound.Play();
		double timestamp = PhotonNetwork.time;
		DetectTarget(direction, startRay, distanceToTravel, gunDamage);
		CreateProjectile(spawnPosition, spawnRotation, timestamp, projectileId);
	}

	[PunRPC]
	public void Damage(int damage, PhotonMessageInfo info) {
		Debug.Log(info.sender);
		info.photonView.GetComponent<PlayerNetwork>().playerHealth -= damage;
	}

	void ChatMessage() {
		// the photonView.RPC() call is the same as without the info parameter.
		// the info.sender is the player who called the RPC.
		Debug.Log("RPC TEST!");
	}
}
