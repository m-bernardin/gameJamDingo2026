using Godot;
using System;

public partial class Options : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void CreditsPressed()
	{
		GD.Print("credits pressed");
	}
	public void MasterVolumeChanged(float amnt)
	{
		GD.Print("master volume changed by: "+amnt);
	}
	public void SFXVolumeChanged(float amnt)
	{
		GD.Print("sfx volume changed by: "+amnt);
	}
	public void MusicVolumeChanged(float amnt)
	{
		GD.Print("music volume changed by: "+amnt);
	}
	public void BackPressed()
	{
		GD.Print("back pressed");
		GetTree().ChangeSceneToFile("res://Scenes/title.tscn");
	}
}
