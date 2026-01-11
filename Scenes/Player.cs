using Godot;
using System;
using System.Collections;
using System.Threading.Tasks.Dataflow;

public partial class Player : Node2D
{
	int[] Stats=new int[5];
	int[] items;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		for(int i = 0;i < Stats.Length;i++)
		{
			Stats[i]=30;
		}
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
		average=average/StatsNeeded.Length;
		return average;
	}
}
