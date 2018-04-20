using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenReaction : MonoBehaviour {
	private Rigidbody2D myRigidbody;
	[SerializeField] private int myHealth;
	private int prevHealth;

	private GameObject objectUI;
	private GameObject imageUI;

	private Material targetMaterial;
	[HideInInspector] public bool isDamaged = false;


	void Start () {
		myRigidbody = this.GetComponentInParent<Rigidbody2D>();
		myHealth = myRigidbody.gameObject.GetComponent<PlayerNetwork>().playerHealth;
		targetMaterial = Resources.Load("TargetText", typeof(Material)) as Material;
		
		objectUI = new GameObject();
		imageUI = new GameObject();

		generateObjectUI();
		generateImageUI();
	}
	
	void LateUpdate() {
		myHealth = myRigidbody.gameObject.GetComponent<PlayerNetwork>().playerHealth;
		if(myHealth != prevHealth){
			isDamaged = true;
		}
		prevHealth = myHealth;
		
		damagedCheck();
		isDamaged = false;
	}

	
	public void damagedCheck() {
		Color img = imageUI.GetComponent<Image>().color;
		if(isDamaged) {		
			img.a = Mathf.Lerp (img.a, 5.00f, Time.deltaTime);
			
		} else {
			img.a = Mathf.Lerp (img.a, 0.0f, Time.deltaTime);
		}
		imageUI.GetComponent<Image>().color = img;
	}

	void generateImageUI() {
		imageUI.name = "ImageUI";
		//imageUI.GetComponent<RectTransform>().position.z = -45;
		imageUI.transform.parent = objectUI.transform;
		Image image = imageUI.AddComponent<Image>();
		
		Color color = new Color32(50,0,0,50);
		//color.r = 25;
		//color.b = 0;
		//color.g = 0;
		//color.a = 25;
		image.color = color;
	}

	void generateObjectUI() {
		//GameObject objectUI = new GameObject();
		Canvas canvas = objectUI.AddComponent<Canvas>();
		FreezeRotation test = objectUI.AddComponent<FreezeRotation>();
		canvas.renderMode = RenderMode.WorldSpace;
		CanvasScaler cs = objectUI.AddComponent<CanvasScaler>();
		cs.scaleFactor = 10.0f;
		cs.dynamicPixelsPerUnit = 10f;
		GraphicRaycaster gr = objectUI.AddComponent<GraphicRaycaster>();
		objectUI.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 3.0f);
		objectUI.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 3.0f);

		objectUI.name = "Personal";
		bool bWorldPosition = false;

		objectUI.GetComponent<RectTransform>().SetParent(this.transform, bWorldPosition);
		objectUI.transform.localPosition = new Vector3(0f, 0f, 0f);
		objectUI.transform.localScale = new Vector3( 
											1.0f / this.transform.localScale.x * 0.1f,
											1.0f / this.transform.localScale.y * 0.1f, 
											1.0f / this.transform.localScale.z * 0.1f );
	}
}