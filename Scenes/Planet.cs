using Godot;
using System;
using System.Security;
using System.Threading.Tasks.Dataflow;

//[GlobalClass]
public partial class Planet : Node2D
{
	public String PlanetName;
	public String FlavourText;
	public int ChallengeType;
	public int Difficulty;
	public int[] Stats;
	public int Ressource;
	public int Qty;
	public bool Pressed=false;
	public Planet()
	{
		
	}
	public void CustomInit(String PlanetName,String FlavourText,int ChallengeType,int Difficulty,int[] Stats,int Ressource,int Qty)
	{
		this.PlanetName=PlanetName;
		this.FlavourText=FlavourText;
		this.ChallengeType=ChallengeType;
		this.Difficulty=Difficulty;
		this.Stats=Stats;
		this.Ressource=Ressource;
		this.Qty=Qty;
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}


	public void PlanetPressed(){
		GD.Print(PlanetName);
		Pressed=!Pressed;
	}
	public void SetSprite(String Filepath)
	{
		Sprite2D Sprite=GetNode<Sprite2D>("Sprite");
		Sprite.Texture=GD.Load<Texture2D>(Filepath);
	}
	
	
}
