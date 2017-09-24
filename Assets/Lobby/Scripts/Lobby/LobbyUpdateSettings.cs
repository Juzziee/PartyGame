using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
 

public class LobbyUpdateSettings : MonoBehaviour {
		
	public void SetupLevel(string levelScene, GameObject playerPrefab){
		this.GetComponent<NetworkLobbyManager> ().playScene = levelScene;
		this.GetComponent<NetworkLobbyManager> ().gamePlayerPrefab = playerPrefab;
		//lobbyManager.GetComponent<NetworkLobbyManager> ().playScene = levels[selectedLevel].scene;
		//lobbyManager.GetComponent<NetworkLobbyManager> ().gamePlayerPrefab = levels [selectedLevel].playerPrefab;
	}
}
