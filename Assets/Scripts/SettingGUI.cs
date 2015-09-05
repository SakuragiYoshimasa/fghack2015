using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SettingGUI : MonoBehaviour {
	

	public InputField roomField;
	public Toggle[] teamToggles;
	public Toggle[] skillToggles;
	public Button startButton;

	public GameManager manager;

	public GameObject waitingText;


	// Use this for initialization
	void Start () {
	
		manager = Utils.GetComponent<GameManager>("GameManager");
		manager.AllGUI = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void roomEdit(){
		manager.RoomID = roomField.text;
	}

	public void selectTeam(){

		for(int i = 0; i < teamToggles.Length; i++){
			if(teamToggles[i].isOn == true){
				manager.TeamID = i;
				break;
			}
		}
	}

	public void selectSkill(){
		for(int i= 0; i < skillToggles.Length; i++){
			if(skillToggles[i].isOn == true){
				manager.getPlayer().Skill = (Skills)i;
			}
		}
	}

	public void StartGame(){
		if(manager.RoomID != null && manager.TeamID != null && manager.getPlayer().Skill != null){

			startButton.gameObject.SetActive(false);
			roomField.gameObject.SetActive(false);
			foreach(Toggle toggle in teamToggles){
				toggle.gameObject.SetActive(false);
			}
			
			foreach(Toggle toggle in skillToggles){
				toggle.gameObject.SetActive(false);
			}

			waitingText.SetActive(true);
			manager.WaitStartGame();
		}
	}

}
