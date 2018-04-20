using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour {
	public bool isHit = false;
	public float health = 100f;
	public float speed;
	public float stoppingDistance;
	private Transform target;

	// for being damage effects
	public Material damageMaterial;
	public Color damageColor;

	private Material originalMaterial;
	private Color originalColor;
	private SpriteRenderer sprRnd;
	private Rigidbody2D myRigidbody;
	public float knockbackForce;
	private Vector2 knockbackVelocity;
	public float knockbackTime;
	private Vector2 originalSize;
	public GunController theGun;

	void Start () {
		myRigidbody = GetComponent<Rigidbody2D>(); 
		target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		sprRnd =  GetComponent<SpriteRenderer>();;

		originalMaterial = sprRnd.material;
		originalColor = sprRnd.color;
		originalSize = transform.localScale;
	}
	
	void Update () {
		//Debug.Log(myRigidbody.velocity);
		//Debug.Log(transform.localScale);
		if(isHit) {
			myRigidbody.velocity = knockbackVelocity;
			StartCoroutine(KnockbackTimer());
			StartCoroutine(Resize(originalSize, originalSize * 1.5f, true));
			StartCoroutine(HitAnimation());
		}else{
			//transform.localScale = new Vector2(2f, 2f);
		}
		if(Vector2.Distance(transform.position, target.position) > stoppingDistance) {
			transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
		}
	}

	void LateUpdate () {
		if(!isHit) {
			myRigidbody.velocity = Vector2.zero;
		}
		if(health <= 0) {
			StartCoroutine(Death());
		}
	}

	IEnumerator Death() {
		StartCoroutine(Resize(originalSize, Vector2.zero, false));
		yield return new WaitForSeconds(.025f);
		Destroy(gameObject);
	}

	IEnumerator KnockbackTimer() {
		yield return new WaitForSeconds(knockbackTime);
		isHit = false;
	}

	IEnumerator Resize(Vector2 originalSize, Vector2 targetSize, bool returnSize) {
		float scaleUpTime = .025f;
		float scaleDownTime = .25f;
		float currentTime = 0.0f;

		while(currentTime < scaleUpTime) {
			currentTime += Time.deltaTime;
			transform.localScale = Vector2.Lerp(originalSize, targetSize, currentTime / scaleUpTime);
			yield return 0;
		}
		transform.localScale = targetSize ;
		
		currentTime = 0.0f;
		
		if(returnSize) {
			while(currentTime < scaleDownTime) {
				currentTime += Time.deltaTime;
				transform.localScale = Vector2.Lerp(transform.localScale, originalSize, currentTime / scaleDownTime);
				yield return 0;
			}
			transform.localScale = originalSize;
			//yield return 0;
		}
	}

	IEnumerator HitAnimation() {
		sprRnd.material = damageMaterial;
		sprRnd.color = damageColor;

		for(int i = 0; i < 15; i++) {
			yield return 0;
		}

		sprRnd.material = originalMaterial;
		sprRnd.color = originalColor;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if(coll.gameObject.tag == "Bullet") {
			//Debug.Log("I was hit by a " + coll.gameObject.name);
			knockbackVelocity = Knockback(coll);
			health = health - 25f;
			isHit = true;
		}
		
    	//Destroy(gameObject);
	}

	Vector2 Knockback(Collision2D coll) {
		Vector2 moveVelocity;
		Vector2 knockBackDir = (coll.transform.position - transform.position).normalized;
		//coll.transform.localRotation = ;
		return moveVelocity = -knockBackDir * knockbackForce;
	}
}
