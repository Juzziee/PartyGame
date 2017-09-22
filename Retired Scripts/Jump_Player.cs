using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(NetworkTransform))]
[RequireComponent(typeof(Rigidbody))]
public class Jump_Player : NetworkBehaviour {

	private float speed = 3;
	private float jump = 250;
	private Rigidbody rb;
	private CharacterController charControl;

	// Network Syncvar
	[SyncVar]
	public string playerName;
	[SyncVar]
	public Color color;
	[SyncVar(hook = "ScoreUpdate")]
	public int score;
	[SyncVar]
	public bool isDead;

	//hard to control WHEN Init is called (networking make order between object spawning non deterministic)
	//so we call init from multiple location (depending on what between spaceship & manager is created first).
	protected bool _wasInit = false;

	protected TextMeshProUGUI _scoreText;

	public GameObject scoreGO;


	void Awake (){
		NetworkGameManager.sPlayers.Add (this);
	}

	void Start () {
		rb = GetComponent<Rigidbody> (); 
		Renderer rend = GetComponent<Renderer> ();
		rend.material.SetColor ("_Color", color);

		if (NetworkGameManager.sInstance != null) {
			Debug.Log (playerName + " Try Init");
			Init ();
		}
	}

	public void Init (){
		if (_wasInit) {
			return;
		}
		GameObject _score = Instantiate (scoreGO, new Vector3 (0, 0, 0), Quaternion.identity);
		_score.transform.SetParent (NetworkGameManager.sInstance.scoreContainer.transform, false);
		_scoreText = _score.GetComponent<TextMeshProUGUI>();

		_wasInit = true;
		UpdateScoreText ();
	}

	// Update is called once per frame
	void FixedUpdate () {

		if (!isLocalPlayer) {
			return;
		}

		var x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;


		transform.Translate(x, 0, 0);

		if(Input.GetButtonDown("Jump")){
			rb.AddForce (transform.up * jump);
		}
	}

	void ScoreUpdate(int newValue){
		score = newValue;
		UpdateScoreText ();
	}

	void UpdateScoreText (){
		if (_scoreText != null) {
			_scoreText.text = playerName + ": " + score;
			_scoreText.color = color;
		}
	}
}
