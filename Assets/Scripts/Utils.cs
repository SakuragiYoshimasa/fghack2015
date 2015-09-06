using UnityEngine;
using System.Collections;

public static class Utils {

	public static float lat = 0;
	public static float lang = 0;
	public static double alti = 0;
	public static bool getLocation = false;

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

	public static Vector3 GetPosition(float lat,float lon){
		float x = lat ;
			//* 111.000f * 1000.000f;
		float z = lon ;
			//* 111.000f * 1000.000f;
		return new Vector3((float)x,0f,(float)z);
	}



	public static void Check(Player player,ConnectParse parse){
		//---------------------------------
		//Check Information and put that into player
		//---------------------------------
		//player.GenerateEnemy(0,Enemy.MonsterType.Dragon,0,-10);
		Debug.Log("willCheck");
		//bool hit = false;
		int enemyID = 0;
		parse.Request(new CondCheck("a", true), response =>
		              {
			CondCheck.R res = response as CondCheck.R;
			//res.Running game state
			if(res.Running){
				int i = 0;
				foreach (CondCheck.EnemyInfo info in res.EnemyList)
				{
					Debug.Log("enemy id : " + info.id);
					player.GenerateMonster(info.id,(Enemy.MonsterType)info.type,GetPosition((float)info.lat,(float)info.lon),i);
					i++;
				}
				
				foreach (CondCheck.TeamInfo info in res.TeamList)
				{
					Debug.Log("team id : " + info.id);
				}


			}
		
			Debug.Log("Check:" + res.Running.ToString());
		}, error =>
		{
			Debug.LogError("Check");
		});
		////////////////////////////////////
		////////////////////////////////////
		//player.GenerateMonster();
		////////////////////////////////////
		////////////////////////////////////
		/// 
		/// 
		/// //35.665123,139.739511
		for(int i = 0; i < 5; i++){
			//player.GenerateMonster(i,(Enemy.MonsterType)0,GetPosition(lat,lang + 0.0001f * i));
				//lat + 0.0001 * i,lang + 0.0001 * i));
			//player.GenerateMonster(i,Enemy.MonsterType.Dragon,GetPosition(1,10));
		}
	}

	public static bool InAttackRange(Vector3 p0,Vector3 p1,Vector3 camAngle){
		var heading = p0 - p1;
		var distance = heading.magnitude;
		var direction = heading / distance;
	//	Debug.Log("check");
	//	Debug.Log(heading.sqrMagnitude);
	//	Debug.Log(Mathf.Abs(Vector3.Angle(Vector3.zero,heading)));
		if (heading.sqrMagnitude <  50 * 50 && Mathf.Abs(Vector3.Angle(Vector3.zero,heading) - camAngle.y) < 120.0f && Mathf.Abs(Vector3.Angle(Vector3.zero,heading) - camAngle.y) > 60.0f) {
			return true;
		}
		return false;
	}
}
