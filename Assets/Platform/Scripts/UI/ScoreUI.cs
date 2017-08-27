using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class ScoreUI : NetworkBehaviour {

	public PlayerList playList;
	public  GameObject scoreContainer;

	void Start() {
		foreach (GameObject players in playList.playersList) {
			Debug.Log (players.name);
		}
	}
}
