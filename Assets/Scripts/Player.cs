using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player {
	
	private Skills skill = Skills.PowerAttack; 

	public Skills Skill{
		get{return skill;}
		set{skill = value;}
	}

	private Transform playerTransform; 

	public List<Enemy> enemies;
	public List<Transform> enemyTrans;
	public List<GameObject> monsters;
	
	public void Init(){
		enemies = new List<Enemy>(128);
		enemyTrans = new List<Transform>(128);
		monsters = new List<GameObject>(128);
		playerTransform = (GameObject.Find("Main Camera") as GameObject).transform;
	}

	public void GenerateEnemy(int id,Enemy.MonsterType type,double px,double py){

	}

	public void Update(){
		//playerTransform.position = new Vector3(Utils.lat,Utils.lang,0);

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
