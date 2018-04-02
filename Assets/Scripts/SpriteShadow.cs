using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteShadow : MonoBehaviour {

	public Vector2 offset = new Vector2(-3, -3);

	private SpriteRenderer sprRndCaster;
	private SpriteRenderer sprRndShadow;
	private Transform transCaster;
	private Transform transShadow;

	public Material shadowMaterial;
	public Color shadowColor;

	void Start() {

		transCaster = transform;
		// create new game object and assigning game object transform to transShadow.
		transShadow = new GameObject().transform;
		transShadow.parent = transCaster;
		transShadow.gameObject.name = "shadow";
		transShadow.localRotation = Quaternion.identity;

		// sprite renderer on the main object script is attached to.
		sprRndCaster = GetComponent<SpriteRenderer>();
		sprRndShadow = transShadow.gameObject.AddComponent<SpriteRenderer>();

		sprRndShadow.material = shadowMaterial;
		sprRndShadow.color = shadowColor;
		sprRndShadow.sortingLayerName = sprRndCaster.sortingLayerName;
		sprRndShadow.sortingOrder = sprRndCaster.sortingOrder - 1;
	}

	// runs after Update()
	void LateUpdate() {
		transShadow.position = new Vector2(transCaster.position.x + offset.x, 
			transCaster.position.y + offset.y);
		sprRndShadow.sprite = sprRndCaster.sprite;
	}
}
