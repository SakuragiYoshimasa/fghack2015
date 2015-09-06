using UnityEngine;
using System.Collections;


public class GameManager : MonoBehaviour {

	enum GameState{
		Register,
		WaitingStart,
		GameMode
	}
	public int Width;
	public int Height;
	public int FPS = 30;

	private GameState state;

	private Player player;
	public Player getPlayer(){
		return player;
	}

	private SettingGUI gui;
	public SettingGUI AllGUI{
		get{return gui;}
		set{gui = value;}
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

	public GameObject zipper;
	public GameObject goblin;
	public GameObject dragon;

	public GameObject camera;

	public GameObject GetMonster(Enemy.MonsterType type){
		switch(type){
			case Enemy.MonsterType.Zipper:
				return zipper;
			case Enemy.MonsterType.Goblin:
				return goblin;
			case Enemy.MonsterType.Dragon:
				return dragon;
			default:
				return zipper;
		}
	}

	void Awake()
	{
		state = GameState.Register;
		player = new Player();
		player.Init(this);
		Width = Screen.width;
		Height = Screen.height;
		DontDestroyOnLoad(gameObject);

	}

	ConnectParse parse;
	
	void Start()
	{
		parse = new ConnectParse(this, gameObject.AddComponent<MTHandler>());
		
		System.Action configAction = () =>
		{
			parse.Request(new CondConfigSync(), response =>
			              {
				CondConfigSync.R config = response as CondConfigSync.R;
				Debug.Log("できたよ GameTime : " + config.GameTime);
			}, error =>
			{
			});
		};
		
		if (parse.IsLogIn)
		{
			Debug.Log("ログインしてるよ");
			configAction();
		}
		else
		{
			parse.SignUp(System.Guid.NewGuid().ToString(), "asdf1234", "Hack", () =>
			             {
				configAction();
			}, () =>
			{
				Debug.Log("できなかったよ");
			});
		}
	}

	public void WaitStartGame(){
		//Application.LoadLevel(0);
		StartCoroutine("waitingGameStart");
		state = GameState.WaitingStart;

	}

	IEnumerator waitingGameStart(){

		bool flag = false;

		//35.665123,139.739511

		parse.Request(new CondRegister(1234, 0,35.665123,139.739511), response => //room id ,team
			             {
			Debug.Log("Register");

			parse.Request(new CondStart(), res =>
			              {
				Debug.Log("Start");
				flag = true;
			}, error =>
			{
				Debug.LogError("Start");
			});


		}, error =>
		{
			Debug.LogError("Register");
		});
			
		while(!flag){

			yield return null;

		}
		AllGUI.StartGame();
		StartGame();
	} 

	/*private bool CheckStartGame(){
		return Utils.CheckStartGame();
	}*/

	private void StartGame(){
		Debug.Log("started");
		state = GameState.GameMode;
		//cameraRenderer.SetActive(true);
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
		StartCoroutine("Check");
	}


	 IEnumerator Check(){
		WaitForSeconds wait = new WaitForSeconds(2.0f);
		while(state == GameState.GameMode){
			yield return wait;
			Utils.Check(player,parse);
		
		}
	}

	// Update is called once per frame
	void Update () {
		if(state == GameState.GameMode){
			
			player.Update();
		}
	}
}

