using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour {
 
	public bool isFiring;

	public float damage = 10f;
	public float recoil = 2f;
	public float range = 50f;

	public float spreadAngle = 0f;

	public int totalAmmo = 60; // total gun ammo
	public int maxAmmo = 8; // max magazine ammo
	private int currentAmmo;
	public float reloadTime = 1f;
	private bool isReloading = false;
	private bool isEmpty = false;

	public LayerMask whatToHit;

	public BulletController bullet;
	public float bulletSpeed;

	public float shootWaitTime = 0.5f;
	private float timeLeft = 0f;
	public Transform firePoint;

	public AudioSource gunSound;
	public AudioSource gunReload;

	public Text currentAmmoText;
	public Text totalAmmoText;

	// Use this for initialization
	void Start () {
		currentAmmo = maxAmmo;
		
	}

	void OnEnable() {
		isReloading = false;
		isFiring = false;
	}
	
	// Update is called once per frame
	void Update () {

		currentAmmoText.text = currentAmmo.ToString();
		totalAmmoText.text = totalAmmo.ToString();

		if(isEmpty) {
			//Debug.Log("No Ammo"); 
			return;
		}

		if(isReloading) {
			return;
		}

		if(currentAmmo <= 0) {
			StartCoroutine(Reload());
			return;
		}

		timeLeft -= Time.deltaTime;
		if(timeLeft < 0) {
			timeLeft = 0;
		}

		if(Input.GetButton("Fire1") && timeLeft <=	 0) {
			Shoot();
			timeLeft = shootWaitTime;
		}else {
			isFiring = false;
		}
	}
	
	void Shoot() {
		isFiring = true;
		currentAmmo--;
		
		gunSound.Play();

		BulletController newBullet = Instantiate(bullet, firePoint.position, bulletRotation(spreadAngle)) as BulletController;
		newBullet.speed = bulletSpeed;
	}

	// coroutine
	IEnumerator Reload() {
		
		isReloading = true;
		isFiring = false;

		

		if(totalAmmo <= 0) {
			isEmpty = true;
			Debug.Log("Failed to reload..."); // for debugging
		} else {
			Debug.Log("Reloading..."); // for debugging

			gunReload.Play();

			yield return new WaitForSeconds(reloadTime - 0.25f);
			
			if(totalAmmo >= maxAmmo) {
				totalAmmo -= maxAmmo;
				currentAmmo = maxAmmo;
			} else {
				currentAmmo = totalAmmo;
				totalAmmo = 0;
			}
		
			yield return new WaitForSeconds(0.25f);
			
		}

		isReloading = false;	
		
	}

	Quaternion bulletRotation(float spreadAngle) {
		spreadAngle = spreadAngle / 2;
		Quaternion bulletRotation;
		Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
		Vector3 direction = (Input.mousePosition - sp).normalized;
				
		// Calculate the angle
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    	//float spread = Random.Range(-90, -90);  // no angle
		//float spread = Random.Range(-45, -135); // 90 degree angle
		float spread = Random.Range(-90 + spreadAngle, -90 - spreadAngle);
		//Debug.Log(spread + 90); // for debugging
		return bulletRotation =  Quaternion.Euler(new Vector3(0, 0, angle + spread));
	}
}


