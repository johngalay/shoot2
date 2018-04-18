using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonNetworkManager : Photon.MonoBehaviour {
	[SerializeField] private Text connectText;
	[SerializeField] private GameObject mainCamera;
	private CameraFollow cameraFollow;
	[SerializeField] private GameObject player;
	[SerializeField] private Transform spawnPoint;
	[SerializeField] private GameObject lobbyCamera;

	// Use this for initialization
	void Start () {
		// connect to server
		PhotonNetwork.ConnectUsingSettings("0.1"); // game version 
	}

	// callback; base class inhereted from Photon.MonoBehaviour,
	public virtual void OnJoinedLobby() {
		Debug.Log("Connected to Lobby");
		// create room or join if one exists
		// RoomOptions roomOptions = new RoomOptions();
		PhotonNetwork.JoinOrCreateRoom("RoomName0", null, null);
	}

	public virtual void OnJoinedRoom() {
		// deactivate lobby cam
		lobbyCamera.SetActive(false);
		
		// spawn player
		PhotonView photonView = PhotonNetwork.Instantiate(player.name, spawnPoint.position, spawnPoint.rotation, 0).GetComponent<PhotonView>();
		// activate main cam
		
		PhotonView photonView2 = PhotonNetwork.Instantiate(mainCamera.name, lobbyCamera.transform.position, lobbyCamera.transform.rotation, 0).GetComponent<PhotonView>();
		/*
		cameraFollow = mainCamera.GetComponent<CameraFollow>();
		cameraFollow.smoothSpeed = 15f;
		cameraFollow.target =  photonView.transform;
		*/
		photonView2.ownerId = photonView.ownerId;
		photonView2.gameObject.GetComponent<CameraFollow>().target  = photonView.transform;
		photonView2.gameObject.SetActive(true);
		//lobbyCamera.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		connectText.text = PhotonNetwork.connectionStateDetailed.ToString();
	}
}
