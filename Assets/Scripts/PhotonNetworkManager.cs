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

<<<<<<< HEAD
	[Header ("X Spawn Range")]
	public float xMin;
	public float xMax;

	// the range of y
	[Header ("Y Spawn Range")]
	public float yMin;
	public float yMax;

	public Vector2 GetSpawnPosition() {
		Vector2 pos = new Vector2 (Random.Range (xMin, xMax), Random.Range (yMin, yMax));
		return pos;
	}

=======
>>>>>>> a5bc8cd111e368efae651365d862ad46dbd60750
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
<<<<<<< HEAD
		PhotonView photonView = PhotonNetwork.Instantiate(player.name, GetSpawnPosition(), spawnPoint.rotation, 0).GetComponent<PhotonView>();
=======
		PhotonView photonView = PhotonNetwork.Instantiate(player.name, spawnPoint.position, spawnPoint.rotation, 0).GetComponent<PhotonView>();
>>>>>>> a5bc8cd111e368efae651365d862ad46dbd60750
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
<<<<<<< HEAD
	public void Respawn(PhotonView photonView, float timer) {
		StartCoroutine(CoRespawn(photonView, timer));
	}
	public IEnumerator CoRespawn(PhotonView photonView, float timer) {
		Debug.Log("Passed 1");
		//while(true) {
		yield return new WaitForSeconds (timer);
		Vector2 pos = GetSpawnPosition();

		photonView.gameObject.transform.position = pos;
		photonView.gameObject.GetComponent<PlayerNetwork>().playerHealth = 100;

		photonView.gameObject.SetActive(true);
		Debug.Log("Passed 2 and Owner: " + photonView.ownerId);
			//PhotonNetwork.Instantiate(player.name, GetSpawnPosition(), spawnPoint.rotation, 0).GetComponent<PhotonView>();

		//}
	}
=======
>>>>>>> a5bc8cd111e368efae651365d862ad46dbd60750
}
