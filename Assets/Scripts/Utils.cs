using UnityEngine;
using System.Collections;

public static class Utils {

	public static double lat = 0;
	public static double lang = 0;
	public static double alti = 0;

	public static Type GetComponent<Type>(string name){
		return GameObject.Find(name).GetComponent<Type>();
	}

	public static void Register(string roomID, int teamID, Skills skill){
		//---------------------------------
		//Register here and wait gameStart
		//---------------------------------

	}

	public static bool CheckStartGame(){
		//---------------------------------
		//check api here
		//---------------------------------
		return false;
	}



	public static void Check(Player player,ConnectParse parse){
		//---------------------------------
		//Check Information and put that into player
		//---------------------------------
		//player.GenerateEnemy(0,Enemy.MonsterType.Dragon,0,-10);

		bool hit = false;
		int enemyID = 0;
		parse.Request(new CondCheck(hit, enemyID), response =>
		              {
			CondCheck.R res = response as CondCheck.R;
			foreach (CondCheck.EnemyInfo info in res.EnemyList)
			{
				Debug.Log("enemy id : " + info.id);
			}
			
			foreach (CondCheck.TeamInfo info in res.TeamList)
			{
				Debug.Log("team id : " + info.id);
			}
			
			Debug.Log("Check");
		}, error =>
		{
			Debug.LogError("Check");
		});
		
	}
}
