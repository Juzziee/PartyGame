using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prototype.NetworkLobby;
using UnityEngine.Networking;

public class NetworkLobbyHook : LobbyHook {
	public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
	{
		LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
		Player player = gamePlayer.GetComponent<Player>();

		player.name = lobby.playerName;
		player.playerName = lobby.playerName;
		player.color = lobby.playerColor;
		player.score = 0;
		player.isDead = false;
	}

}
