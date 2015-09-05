using UnityEngine;
using System.Collections;

public class Player {
	
	private Skills skill = Skills.PowerAttack; 

	public Skills Skill{
		get{return skill;}
		set{skill = value;}
	}


	//--------------------------------------------------
	//GestureAttack
	//--------------------------------------------------
	private void Attack(){

	}

	//--------------------------------------------------
	//Check
	//--------------------------------------------------
	private void Check(){

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
