using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player {
	
	private Skills skill = Skills.PowerAttack; 

	public Skills Skill{
		get{return skill;}
		set{skill = value;}
	}

	//private Transform playerTransform; 

	public Enemy[] enemies;
	//public List<Transform> enemyTrans;
	public GameObject[] monsters;

	public GameManager manager;

	public bool attackable = false;
	public bool attacked = false;
	
	public void Init(GameManager m){
		enemies = new Enemy[5];
		//enemyTrans = new List<Transform>();
		monsters = new GameObject[5];
		//playerTransform = (GameObject.Find("Main Camera") as GameObject).transform;

		manager = m;

	}

	public void GenerateMonster(int id,Enemy.MonsterType type,Vector3 position){
		if(enemies[id] != null){
			 if(type != enemies[id].type){
				if(monsters[id] != null){
					GameObject.Destroy(monsters[id]);
				}
				monsters[id] = GameObject.Instantiate(manager.GetMonster(type));
			}
		}else{
			monsters[id] = GameObject.Instantiate(manager.GetMonster(type)) as GameObject;
		}
	
		enemies[id] = new Enemy(id,type,position.x,position.z);

	}
	

	public void Update(){

		manager.camera.transform.position = Utils.GetPosition(Utils.lat,Utils.lang);
		attackable = false;
		for(int i = 0; i < monsters.Length; i++){
			if(monsters[i] != null && enemies[i] != null){
				monsters[i].transform.position = Utils.GetPosition(Utils.lat,Utils.lang + 0.0001f * i);
					//new Vector3((float)enemies[i].X,0f,(float)enemies[i].Y);
				if(Utils.InAttackRange(manager.camera.transform.position,monsters[i].transform.position)){
					attackable = true;
				}
			}

		}
		Debug.Log("update");
		Quaternion rotRH = Input.gyro.attitude;
		Quaternion rot = new Quaternion(-rotRH.x, -rotRH.z, -rotRH.y, rotRH.w);
		//rot *= Quaternion.Euler(90f, 0f, 0f);
		
		manager.camera.transform.localRotation = rot;
		Debug.Log(rot.eulerAngles);


	}

	//--------------------------------------------------
	//GestureAttack
	//-------------------s-------------------------------
	private void Attack(){

	}

	//--------------------------------------------------
	// Skill Attack depend on skill param
	//--------------------------------------------------
	private void SkillAttack(){

		switch(Skill){
			case Skills.PowerAttack:
				break;
			case Skills.Search:
				break;
			case Skills.Jammer:
				break;
		}

	}

}
