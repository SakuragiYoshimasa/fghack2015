using UnityEngine;
using System.Collections;

public static class Utils {

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

	public static void Check(){
		//---------------------------------
		//Check Information
		//---------------------------------

	}
}
