using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildUI : MonoBehaviour {

	//public GameObject targetPlayer;
	public GameObject scorePanel;
	public GameObject scoreText;
	public GameObject[] playersList;
	//public GameObject playerContainer;

	private GameObject playerScore;


	void Start(){
		playersList = GameObject.FindGameObjectsWithTag ("Player");

		foreach (GameObject player in playersList) {
			playerScore = Instantiate (scoreText, new Vector3 (-528f, 332f, 0), transform.rotation) as GameObject;
			//playerScore.transform.SetParent (scorePanel.transform);
				
		}
	}
}
