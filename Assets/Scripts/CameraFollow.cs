using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	
	public float smoothSpeed = 10f;
	public Vector3 offset;
	public Vector2 panLimit;
	private Camera mainCamera;
	void Start () {
		mainCamera = FindObjectOfType<Camera>();
	}
	void FixedUpdate() {

		Vector3 pointToLook = mainCamera.ScreenToWorldPoint(Input.mousePosition);
		Vector2 direction = new Vector2(
			pointToLook.x - transform.position.x,
			pointToLook.y - transform.position.y
		);
		direction.x = Mathf.Clamp(direction.x, -panLimit.x, panLimit.x);
		direction.y = Mathf.Clamp(direction.y, -panLimit.y, panLimit.y);

		offset = direction;
		offset.z = -5;

		Vector3 desiredPosition = target.position + offset;
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
		transform.position = smoothedPosition;
	}
}
