using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[RequireComponent(typeof(NetworkTransform))]
[RequireComponent(typeof(Rigidbody))]
public class Player : NetworkBehaviour {

	private float speed = 3;
	private float jump = 250;
	private Rigidbody rb;
	private CharacterController charControl;

	public BlockFall block;

	// Network Syncvar
	[SyncVar]
	public string playerName;
	[SyncVar]
	public Color color;
	[SyncVar]
	public int score;
	[SyncVar]
	public bool isDead;



	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		Renderer rend = GetComponent<Renderer> ();
		rend.material.SetColor ("_Color", color);
	}

	// Update is called once per frame
	void FixedUpdate () {

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
