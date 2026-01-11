using Godot;
using System;
using System.Linq;

public partial class GamePiece : Area2D
{
	protected int size;
	private bool collected;
	private bool beingDragged;
	private bool mouseInside;
	private Vector2 originalPosition;
	public override void _Ready()
	{
		InputPickable=true;
		AudioBusOverride=false;
	}
	public override void _Process(double delta)
	{
		Dragging();
	}
	//setters
	public void SetSize(int size)
	{
		this.size=size;
	}
	public void SetCollected(bool collected)
	{
		this.collected=collected;
	}
	//getters
	public int GetSize()
	{
		return size;
	}
	public bool GetCollected()
	{
		return collected;
	}
	//dragging methods
	private void MouseEntered()
	{
		mouseInside=true;
	}
	private void MouseExited()
	{
		mouseInside=false;
	}
	public void Dragging()
	{
		if(!beingDragged)BeginDrag();
		if(beingDragged)
		{
			GlobalPosition=GetGlobalMousePosition();
			EndDrag(CheckValidity());
		}
	}
	public void BeginDrag()
	{
		if (Input.IsMouseButtonPressed(MouseButton.Left)&&mouseInside)
		{
			originalPosition=GlobalPosition;
			GD.Print("original position: "+originalPosition);
			beingDragged=true;
		}
	}
	public void EndDrag(bool validMove)
	{
		if (Input.IsMouseButtonPressed(MouseButton.Right))
		{
			GD.Print("valid move: "+validMove);
			if (!validMove)
			{
				GD.Print("invalid move detected, moving to: "+originalPosition);
				GD.Print("(current position: "+GlobalPosition+")");
				GlobalPosition=originalPosition;
				GD.Print("piece moved, new position: "+GlobalPosition);
			}
			beingDragged=false;
		}
	}
	public bool CheckValidity()
	{
		Area2D[] overlaps=GetOverlappingAreas().ToArray();
		bool valid=true;
			for(int i=0;i<overlaps.Length;++i)
			{
				if(overlaps[i].InputPickable==true)valid=false;
			}
		return valid;
	}
}
