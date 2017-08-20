using UnityEngine;
using UnityEngine.Networking;

public class LevelGen : NetworkBehaviour {

	public Transform levelStart;
	public Transform levelEnd;
	public GameObject levelTile;
	public GameObject levelWall;
	private GameObject blockParent;
	private GameObject blockSpawn;
	private Vector3 currentBlock;
	private Vector3 nextBlock;
	private float nextBlockX;
	private float nextBlockZ;

	// Use this for initialization
	void Start () {
		BuildLevel ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void BuildLevel (){

		blockParent = new GameObject ();
		currentBlock = new Vector3 (levelStart.transform.position.x, -0.9f, levelStart.transform.position.z);
		Instantiate (blockParent, currentBlock, Quaternion.identity);

		blockParent.name = "Block Parent";


		while (currentBlock.z > levelEnd.position.z) {
			while (currentBlock.x < levelEnd.position.x) {
				blockSpawn = Instantiate (levelTile, new Vector3 (currentBlock.x, currentBlock.y, currentBlock.z), transform.rotation) as GameObject;
				blockSpawn.transform.SetParent (blockParent.transform);
				Physics.IgnoreCollision (blockSpawn.GetComponent<Collider> (), levelWall.GetComponent<Collider> ());
				NetworkServer.Spawn (blockSpawn);


				nextBlockX = levelTile.transform.localScale.x + currentBlock.x;
				nextBlockZ = currentBlock.z;

				nextBlock = new Vector3 (nextBlockX, currentBlock.y, nextBlockZ);
	
				currentBlock = nextBlock;

			}
			nextBlockZ = currentBlock.z - levelTile.transform.localScale.z;

			nextBlock = new Vector3 (levelStart.position.x, currentBlock.y, nextBlockZ);
			currentBlock = nextBlock;

		}

	}
}
