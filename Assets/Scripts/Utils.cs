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

	public static void Check(Player player){
		//---------------------------------
		//Check Information and put that into player
		//---------------------------------
		player.GenerateEnemy(0,Enemy.MonsterType.Dragon,0,-10);

	}
}
