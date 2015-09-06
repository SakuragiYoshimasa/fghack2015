using UnityEngine;
using System.Collections;

public class Enemy{

	public enum MonsterType{
		Zipper,
		Goblin,
		Dragon
	}

	public MonsterType type;

	private double x;
	private double y;

	public double X{
		get{return x;}
		set{x = value;}
	}
	
	public double Y{
		get{return y;}
		set{y = value;}
	}

	private int id;
	public int ID{
		get{return id;}
		set{id = value;}
	}

	public Enemy(int id,MonsterType type,double px, double py){
		this.id = id;
		this.type = type;
		X = px;
		Y = py;
	}
}
