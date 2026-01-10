using Godot;
using System;
using System.Collections.Generic;

public partial class Title : Area2D
{
	Play play;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		play=new Play();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		CheckMouseInput();
	}

	private void CheckMouseInput()
	{
	}

	private void PlayPressed()
	{
		GD.Print("play pressed");
	}
	private void OptionsPressed()
	{
		GD.Print("options pressed");
	}
	private void QuitPressed()
	{
		GD.Print("quit pressed");
	}
}
