using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetwork : MonoBehaviour {
	[SerializeField] private Transform playerCamera;
	[SerializeField] private MonoBehaviour[] playerControlSripts;
	[SerializeField] private GameObject[] playerGameObjects;

	private PhotonView photonView;
	public int playerHealth = 90;

	private void Start() {
		photonView = GetComponent<PhotonView>();
		Initialize();
	}

	private void Initialize() {
		if(photonView.isMine) {

		} else {
			// Disabling camera
			playerCamera.gameObject.SetActive(false);
			// Disabling control scripts
			foreach(MonoBehaviour m in playerControlSripts){
				m.enabled = false;
			}
			foreach(GameObject g in playerGameObjects){
				g.SetActive(false);
			}
		}
	}

	private void Update() {
		if(!photonView.isMine) {
			return;
		}

		if(Input.GetKeyDown(KeyCode.E)){
			playerHealth = playerHealth - 5;
		}
	}

	private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
		// send data
		if(stream.isWriting) {
			stream.SendNext(playerHealth);
		}

		// receive data
		else if(stream.isReading) {
			playerHealth = (int)stream.ReceiveNext();
		}
	}
}
