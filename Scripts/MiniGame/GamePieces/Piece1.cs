using Godot;
using System;
using System.Drawing;

public partial class Piece1 : GamePiece
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		size=4;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Dragging();
	}
}
