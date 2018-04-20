using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateObjectUI : MonoBehaviour {
	private Rigidbody2D myRigidbody;
	[SerializeField] private int myHealth;
<<<<<<< HEAD
	private int prevHealth;

	private GameObject objectUI;
	private GameObject textUI;
	//private GameObject imageUI;

	private Material targetMaterial;
	//[HideInInspector] public bool isDamaged = false;


	void Start () {
		myRigidbody = this.GetComponentInParent<Rigidbody2D>();
		myHealth = myRigidbody.gameObject.GetComponent<PlayerNetwork>().playerHealth;
		targetMaterial = Resources.Load("TargetText", typeof(Material)) as Material;
		
		objectUI = new GameObject();
		textUI = new GameObject();
		//imageUI = new GameObject();

		generateObjectUI();
		//generateImageUI();
=======
	GameObject objectUI;
	GameObject textUI;

	void Start () {
		
		myRigidbody = this.GetComponentInParent<Rigidbody2D>();
		myHealth = myRigidbody.gameObject.GetComponent<PlayerNetwork>().playerHealth;
		
		objectUI = new GameObject();
		textUI = new GameObject();

		generateObjectUI();
>>>>>>> a5bc8cd111e368efae651365d862ad46dbd60750
	}
	
	void LateUpdate() {
		myHealth = myRigidbody.gameObject.GetComponent<PlayerNetwork>().playerHealth;
<<<<<<< HEAD
		//if(myHealth != prevHealth){
			//isDamaged = true;
		//}
		prevHealth = myHealth;
		textUI.GetComponent<Text>().text = myHealth.ToString();	
		
		//damagedCheck();
		//isDamaged = false;
	}

	/* 
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
	*/
=======
		textUI.GetComponent<Text>().text = myHealth.ToString();	
	}
>>>>>>> a5bc8cd111e368efae651365d862ad46dbd60750

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

		textUI.name = "HealthUI"; 
		textUI.transform.parent = objectUI.transform;

		Text health = textUI.AddComponent<Text>();
		textUI.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 3.0f);
		textUI.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 3.0f);
		health.alignment = TextAnchor.MiddleCenter;
		health.horizontalOverflow = HorizontalWrapMode.Overflow;
		health.verticalOverflow = VerticalWrapMode.Overflow;
		Font ArialFont = (Font)Resources.GetBuiltinResource (typeof(Font), "Arial.ttf");
		health.font = ArialFont;
		health.fontSize = 6;
		health.text = "100";
		health.enabled = true;
		health.color = Color.white;
<<<<<<< HEAD
		health.GetComponent<Text>().material = targetMaterial;
=======
>>>>>>> a5bc8cd111e368efae651365d862ad46dbd60750

		objectUI.name = "Text Label";
		bool bWorldPosition = false;

		objectUI.GetComponent<RectTransform>().SetParent(this.transform, bWorldPosition);
		objectUI.transform.localPosition = new Vector3(0f, 0f, 0f);
		objectUI.transform.localScale = new Vector3( 
											1.0f / this.transform.localScale.x * 0.1f,
											1.0f / this.transform.localScale.y * 0.1f, 
											1.0f / this.transform.localScale.z * 0.1f );
	}
}