using Godot;
using System;
using System.Collections;
using System.Threading.Tasks.Dataflow;

public partial class Player : Node2D
{
	public int[] Stats=new[]{100,100,60,70,50};
	int[] items;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public int GetOdds(int[] StatsNeeded,int ChallengeType)
	{
		int average=0;
		foreach(int i in StatsNeeded)
		{
			average+=Stats[i];
		}
		if (StatsNeeded.Length > 0)
		{
			average=average/StatsNeeded.Length;
			return average;
		}
		return 100;
	}
}
