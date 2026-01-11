using Godot;
using System;

public partial class MiniGameManager : Node2D
{
	private int difficulty;
	private Player player;
	public override void _Ready()
	{
		var scene1=ResourceLoader.Load<PackedScene>("res://Scenes/MiniGame/piece1.tscn");
		var piece1=scene1.Instantiate<GamePiece>();
		piece1.Position=new Vector2(550,400);
		var scene2=ResourceLoader.Load<PackedScene>("res://Scenes/MiniGame/piece1.tscn");
		var piece2=scene2.Instantiate<GamePiece>();
		piece2.Position=new Vector2(700,800);
	}
	public override void _Process(double delta)
	{
	}
}
