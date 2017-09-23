using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using TMPro;

public class LobbyLevelList : NetworkBehaviour {

	public GameObject lobbyManager;

	public GameObject modeTitle;
	public GameObject modeImg;
	public GameObject modeDesc;

	[System.Serializable]
	public struct Levels {
		public string scene;
		public GameObject playerPrefab;
		public string name;
		public string desc;
		public Sprite image;
	}
		
	public Levels[] levels;

	[SyncVar]
	public int levelCounter;

	// Use this for initialization
	void Start () {
		levelCounter = 0;
		UpdateUI ();
	}

	void UpdateUI(){
		modeTitle.GetComponent<TextMeshProUGUI> ().text = levels [levelCounter].name;
		modeImg.GetComponent<Image>().sprite = levels [levelCounter].image;
		modeDesc.GetComponent<TextMeshProUGUI>().text = levels [levelCounter].desc;
	}
	
	public void SelectButton(){
		Debug.Log (levels[levelCounter].name + " Level Selected");

		//CmdLevelSetup (levelCounter);

		lobbyManager.GetComponent<NetworkLobbyManager> ().playScene = levels[levelCounter].scene;
		lobbyManager.GetComponent<NetworkLobbyManager> ().gamePlayerPrefab = levels [levelCounter].playerPrefab;

	}

	[Command]
	void CmdLevelSetup (int selectedLevel){
		RpcLevelSetup (selectedLevel);
	}

	[ClientRpc]
	void RpcLevelSetup(int selectedLevel){
		Debug.Log ("Setting up level on client");
		lobbyManager.GetComponent<NetworkLobbyManager> ().playScene = levels[selectedLevel].scene;
		lobbyManager.GetComponent<NetworkLobbyManager> ().gamePlayerPrefab = levels [selectedLevel].playerPrefab;
	}

	public void NextLevel(){
		levelCounter++;

		if (levelCounter > levels.Length - 1) {
			levelCounter = 0;
		}
		UpdateUI ();
	}

	public void PreviousLevel(){
		levelCounter--;

		if (levelCounter < 0) {
			levelCounter = levels.Length;
		}

		UpdateUI ();
	}
}
