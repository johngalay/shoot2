using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrail : MonoBehaviour {

	[SerializeField] private int moveSpeed = 60;
	private Vector3 previousPos;

<<<<<<< HEAD
	public double creationTime;
	public Vector3 startPosition;
	public int projectileId;

=======
>>>>>>> a5bc8cd111e368efae651365d862ad46dbd60750
	void Start() {
		previousPos = transform.position;
		StartCoroutine(TimedDestruction());
	}
	
<<<<<<< HEAD
	void Update() {

		float timePassed = (float)(PhotonNetwork.time - creationTime);

		previousPos = transform.position;
		
		transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
=======
	void Update () {

		previousPos = transform.position;

		transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

>>>>>>> a5bc8cd111e368efae651365d862ad46dbd60750
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
<<<<<<< HEAD
=======
			//Debug.Log("Distance Travelled: " + Vector3.Distance(startPos, transform.position));
>>>>>>> a5bc8cd111e368efae651365d862ad46dbd60750
		}
	}
}
