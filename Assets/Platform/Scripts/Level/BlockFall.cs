using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class BlockFall : NetworkBehaviour {

	private float fallSpeed = 4f;
	private int blockFallChance;
	private Color defaultColor;
	private Rigidbody rb;

	[SyncVar]
	public bool blockTriggered;


	[Command]
	public void CmdToggle(){
		RpcToggle ();
	}

	[ClientRpc]
	public void RpcToggle(){
		blockTriggered = true;
	}

	void Update(){
		if (blockTriggered) {
			beginFall ();
		}
	}

	public void beginFall(){
		Invoke ("Fall", fallSpeed);
		Renderer rend = GetComponent<Renderer> ();
		defaultColor = rend.material.color;
		StartCoroutine (ColorChange ());

	}


	IEnumerator ColorChange(){
		Renderer rend = GetComponent<Renderer> ();
		rend.material.SetColor ("_Color", Color.red);
		for (int i = 0; i < 5; i++) {
			yield return new WaitForSeconds (0.5f);
			rend.material.SetColor ("_Color", defaultColor);
			yield return new WaitForSeconds (0.5f);
			rend.material.SetColor ("_Color", Color.red);
		}
	}


	void Fall(){
		rb = GetComponent<Rigidbody> ();
		rb.isKinematic = false;
		rb.GetComponent<Collider> ().enabled = false;
		Invoke ("DestroyBlock", 5);
	}



	void DestroyBlock(){
		Destroy (this.gameObject);
	}
}
