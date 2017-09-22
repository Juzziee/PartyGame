using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Controls_TopDown : NetworkBehaviour {

	public int speed;
	public int jump;
	private Rigidbody rb;

	void Start(){
		rb = this.GetComponent<Rigidbody> ();
	}

	void FixedUpdate() {
		if (!isLocalPlayer) {
			return;
		}

		var x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * speed;


		transform.Translate(x, 0, z);

		if(Input.GetButtonDown("Jump")){
			rb.AddForce (transform.up * jump);
		}
	}
}
