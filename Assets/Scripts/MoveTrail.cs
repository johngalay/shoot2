using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrail : MonoBehaviour {

	[SerializeField] private int moveSpeed = 60;
	private Vector3 previousPos;

	void Start() {
		previousPos = transform.position;
		StartCoroutine(TimedDestruction());
	}
	
	void Update () {

		previousPos = transform.position;

		transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

		// normalized, gives a directional value
		RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, 
			(transform.position - previousPos).normalized,
			(transform.position - previousPos).magnitude);
		for(int i = 0; i < hits.Length; i++) {
			OnCollisionRay(hits[i]);
		}
	}

	IEnumerator TimedDestruction() {
		yield return new WaitForSeconds(1);
		Destroy(gameObject);
	}

	void OnCollisionRay(RaycastHit2D hit) {
		if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Obstacle") || 
		hit.collider.gameObject.layer == LayerMask.NameToLayer("Target")) {
			Destroy(gameObject,.01f);
			//Debug.Log("Distance Travelled: " + Vector3.Distance(startPos, transform.position));
		}
	}
}
