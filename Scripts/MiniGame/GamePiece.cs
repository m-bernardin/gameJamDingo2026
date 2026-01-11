using Godot;
using System;

public partial class GamePiece : Area2D
{
	private int size;
	private int resource;
	private bool collected;
	private bool beingDragged;
	private bool mouseInside;
	private Vector2 originalPosition;
	public override void _Ready()
	{
		//var collisionBox=new CollisionBox();
		//collisionBox.MouseEntered+=()=>MouseEntered();
		//collisionBox.MouseExited+=()=>MouseExited();
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
	public void SetResource(int resource)
	{
		this.resource=resource;
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
	public int GetResource()
	{
		return resource;
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
			EndDrag(!HasOverlappingAreas());
			GlobalPosition=GetGlobalMousePosition();
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
				GlobalPosition=originalPosition;
			}
			beingDragged=false;
		}
	}
}
