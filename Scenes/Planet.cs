using Godot;
using System;
using System.Security;

//[GlobalClass]
public partial class Planet : Node2D
{
	public String PlanetName;
	String FlavourText;
	int ChallengeType;
	int Difficulty;
	int[] Stats;
	int Ressource;
	int Qty;
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

	public int MesureSurvival()
	{
		return 60;
	}

	public void SwitchScreen()
	{
		int outcome=MesureSurvival();

		if (outcome == 0)
		{
			//loss, switch screen to loss screen
		}
		else
		{
			if (outcome == 1)
			{
				//alter player stat, give feedback that a hit was taken
			}
			else if (outcome == 2){
				//give feedback landing was perfect
			}
			//display minigame screen

		}
	}
	public void PlanetPressed(){
		GD.Print(PlanetName);
		Pressed=true;
	}
	
	
}
