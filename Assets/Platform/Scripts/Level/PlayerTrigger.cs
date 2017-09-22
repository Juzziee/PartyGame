using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour {

	private BlockFall block;

	void OnCollisionEnter (Collision col) {
		if (col.gameObject.tag == "Block") {
			block = col.gameObject.GetComponent<BlockFall> ();
			int blockFallChance = Random.Range (0, 100);
			if (blockFallChance >= 90) {
				block.CmdToggle ();
			}
		}
	}
}
