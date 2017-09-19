using UnityEngine;
using UnityEngine.Networking;
using Prototype.NetworkLobby;
using System.Collections;
using System.Collections.Generic;

public class NetworkGameManager : NetworkBehaviour {

	static public List<Player> sPlayers = new List<Player>();
	static public NetworkGameManager sInstance = null;

	public GameObject scoreContainer;

	public Font uiScoreFont;

	// Use this for initialization
	void Awake () {
		sInstance = this;
	}

	void Start(){
		for (int i = 0; i < sPlayers.Count; ++i) {
			sPlayers [i].Init ();
		}
	}
}
