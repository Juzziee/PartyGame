using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(NetworkTransform))]
[RequireComponent(typeof(Rigidbody))]
public class Player : NetworkBehaviour {

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
	public bool _scoreLong;
	public GameObject scoreGO;


	void Awake (){
		NetworkGameManager.sPlayers.Add (this);
	}

	void Start () {
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
		
	void ScoreUpdate(int newValue){
		score = newValue;
		UpdateScoreText ();
	}

	void UpdateScoreText (){
		if (_scoreText != null) {
			if (_scoreLong == true) {
				_scoreText.text = playerName + ": " + score;
			} else {
				_scoreText.text = "" + score;
			}
			_scoreText.color = color;
		}
	}
}
