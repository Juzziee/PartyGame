using UnityEngine;

public class KillFloor : MonoBehaviour {

	private GameObject player;
	public PlayerList playerList;

	void OnCollisionEnter (Collision tar) {
		if (tar.gameObject.tag == "Player") {
			tar.gameObject.GetComponent<Player> ().isDead = true;
			tar.gameObject.SetActive (false);
			playerList.CheckAlive ();
		} else {
			Destroy (tar.gameObject);
		}
	}
}
