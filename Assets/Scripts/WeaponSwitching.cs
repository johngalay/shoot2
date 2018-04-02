using UnityEngine;

public class WeaponSwitching : MonoBehaviour {

	public GunController selectedGun;
	public int selectedWeapon = 0;

	// Use this for initialization
	void Start () {
		//selectedGun = gameObject.transform.GetChild(0).GetComponent<GunController>();
		SelectWeapon();
	}
	
	// Update is called once per frame
	void Update () {

		int previousSelectedWeapon = selectedWeapon;

		if(Input.GetAxis("Mouse ScrollWheel") > 0f) {
			if(selectedWeapon >= transform.childCount - 1)
				selectedWeapon = 0;
			else
				selectedWeapon++;
		}
		if(Input.GetAxis("Mouse ScrollWheel") < 0f) {
			if(selectedWeapon <= 0)
				selectedWeapon = transform.childCount - 1;
			else
				selectedWeapon--;
		}

		if(previousSelectedWeapon != selectedWeapon ) {
			SelectWeapon();
		}

	}

	void SelectWeapon() {
		int i = 0;
		foreach(Transform weapon in transform) {
			if(i == selectedWeapon) {
				weapon.gameObject.SetActive(true);
				this.transform.parent.GetComponent<PlayerController>().theGun = weapon.GetComponent<GunController>();
			}else{
				weapon.gameObject.SetActive(false);
			}
			i++;
		}
	}
}
