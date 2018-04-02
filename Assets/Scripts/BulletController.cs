using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	public float speed;
	private Sprite defaultSprite;
	public Sprite muzzleFlash;
	public int framesToFlash = 3;
	public float destroyTime = 3;
	private SpriteRenderer spriteRend;

	void Start () {
		spriteRend = GetComponent<SpriteRenderer>();
		defaultSprite = spriteRend.sprite;

		StartCoroutine(FlashMuzzleFlash());
		StartCoroutine(TimedDestruction());
	}

	IEnumerator FlashMuzzleFlash() {
		spriteRend.sprite = muzzleFlash;

		for(int i = 0; i < framesToFlash; i++) {
			yield return 0;
		}

		spriteRend.sprite = defaultSprite;
	}

	IEnumerator TimedDestruction() {
		yield return new WaitForSeconds(destroyTime);
		Destroy(gameObject);
		//Destroy(gameObject,2f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.up * speed * Time.deltaTime);
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		Debug.Log(coll.gameObject.name);
    	Destroy(gameObject);
	}
	

}
