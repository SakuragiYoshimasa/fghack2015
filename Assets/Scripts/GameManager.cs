using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	enum GameState{
		Register,
		WaitingStart,
		GameMode
	}
	public int Width = 1920;
	public int Height = 1080;
	public int FPS = 30;

	private GameState state;

	private Player player;
	public Player getPlayer(){
		return player;
	}

	private SettingGUI gui;
	public SettingGUI AllGUI{
		get{
			return gui;
		}
		set{
			gui = value;
		}
	}

	private string roomID = "";
	public string RoomID{
		get{return roomID;}
		set{roomID = value;}
	}

	private int teamID = 0;
	public int TeamID{
		get{return teamID;}
		set{teamID = value;}
	}

	public GameObject cameraRenderer;

	void Awake()
	{
		state = GameState.Register;
		player = new Player();
		Width = Screen.width;
		Height = Screen.height;
		DontDestroyOnLoad(gameObject);
	}
	// Use this for initialization
	void Start () {

	}

	public void WaitStartGame(){
		//Application.LoadLevel(0);
		StartCoroutine("waitingGameStart");
		state = GameState.WaitingStart;

	}

	IEnumerator waitingGameStart(){
		WaitForSeconds wait = new WaitForSeconds(0.5f);
		int i = 0;
		while(!CheckStartGame()){
			Debug.Log((i++).ToString());
			yield return wait;
			if(i > 2){
				break;
			}
		}
		AllGUI.waitingText.SetActive(false);
		StartGame();
	} 

	private bool CheckStartGame(){
		return Utils.CheckStartGame();
	}

	private void StartGame(){
		Debug.Log("started");
		state = GameState.GameMode;
		cameraRenderer.SetActive(true);
		var devices = WebCamTexture.devices;
		if (devices.Length > 0)
		{
			var webcamTexture = new WebCamTexture(Width, Height, FPS);
			cameraRenderer.GetComponent<MeshRenderer>().material.mainTexture = webcamTexture;
			webcamTexture.Play();
		}else
		{
			Debug.Log("No devices");
			return;
		}
	}

	// Update is called once per frame
	void Update () {
		if(state == GameState.GameMode){


		}
	}
}

