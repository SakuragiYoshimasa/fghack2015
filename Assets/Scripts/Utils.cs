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

	/*public static bool CheckStartGame(){
		//---------------------------------
		//check api here
		//---------------------------------
		return false;
	}*/

	public static Vector3 GetPosition(double lat,double lon){
		double x = lat * 111 * 1000;
		double z = lon * 111 * 1000;
		return new Vector3((float)x,0f,(float)z);
	}



	public static void Check(Player player,ConnectParse parse){
		//---------------------------------
		//Check Information and put that into player
		//---------------------------------
		//player.GenerateEnemy(0,Enemy.MonsterType.Dragon,0,-10);

	/*bool hit = false;
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
		});*/
		////////////////////////////////////
		////////////////////////////////////
		//player.GenerateMonster();
		////////////////////////////////////
		///////////////////////////////////////
		/// 
		/// 
		/// //35.665123,139.739511
		for(int i = 0; i < 5; i++){
			player.GenerateMonster(i,(Enemy.MonsterType)0,GetPosition(lat,lang + 0.0001 * i));
				//lat + 0.0001 * i,lang + 0.0001 * i));
			//player.GenerateMonster(i,Enemy.MonsterType.Dragon,GetPosition(1,10));
		}
	}
}
