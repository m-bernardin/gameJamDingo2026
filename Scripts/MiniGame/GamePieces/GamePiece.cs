using Godot;
using System;

public partial class GamePiece : Node2D
{
	protected int size;
	protected int resource;
	private bool collected;
	protected Vector2 TILE_SIZE=new Vector2(50,50);
	public override void _Ready()
	{
	}
	public override void _Process(double delta)
	{
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
	//Highlight methods
	private void MouseEntered()
	{
		Highlight();
	}
	private void MouseExited()
	{
		UnHighlight();
	}
	protected void UnHighlight()
	{
		GD.Print("unhighlighting block");
	}
	protected void Highlight()
	{
		GD.Print("highlighting block");
	}
	//snapping
}
