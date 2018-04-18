using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateObjectUI : MonoBehaviour {
	private Rigidbody2D myRigidbody;
	[SerializeField] private int myHealth;
	GameObject objectUI;
	GameObject textUI;

	void Start () {
		
		myRigidbody = this.GetComponentInParent<Rigidbody2D>();
		myHealth = myRigidbody.gameObject.GetComponent<PlayerNetwork>().playerHealth;
		
		objectUI = new GameObject();
		textUI = new GameObject();

		generateObjectUI();
	}
	
	void LateUpdate() {
		myHealth = myRigidbody.gameObject.GetComponent<PlayerNetwork>().playerHealth;
		textUI.GetComponent<Text>().text = myHealth.ToString();	
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