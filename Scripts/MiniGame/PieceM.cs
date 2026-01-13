using Godot;
using System;

public partial class PieceM : GamePiece
{
	public override void _Ready()
	{
		base._Ready();
		size=5;
	}
	public override void _Process(double delta)
	{
		Dragging();
	}
}
