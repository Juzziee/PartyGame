using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class PlayerList : NetworkBehaviour {

	public GameObject[] playersList;
	public Transform playerContainer;

	// Use this for initialization
	void Start () {
		Invoke ("AddPlayer", 5f);
	}
	
	// Update is called once per frame
	void AddPlayer () {
		playersList = GameObject.FindGameObjectsWithTag ("Player");

		foreach (GameObject players in playersList) {
			players.transform.SetParent (playerContainer);
		}
	}

	public void CheckAlive(){
		int playersDead = 0;
		foreach (GameObject players in playersList) {
			if (players.gameObject.GetComponent<Player> ().isDead == true) {
				playersDead++;
			}
			if (playersDead == playersList.Length - 1) {
				RpcIncreaseScore ();
			}
		}
	}

	[ClientRpc]
	public void RpcIncreaseScore(){
		foreach (GameObject players in playersList) {
			if (players.gameObject.GetComponent<Player> ().isDead == false) {
				players.gameObject.GetComponent<Player> ().score++;
			}
		}
	}
}
