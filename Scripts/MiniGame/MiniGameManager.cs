using Godot;
using System;
using System.Collections.Generic;

public partial class MiniGameManager : Node2D
{
	private Player player;
	private int[] piecesArray;
	private List<GamePiece> piecesList;
	private List<Vector2> pieceNodes;
	public override void _Ready()
	{
		var scene1=ResourceLoader.Load<PackedScene>("res://Scenes/MiniGame/piece1.tscn");
		var piece1=scene1.Instantiate<GamePiece>();
		AddChild(piece1);
		piece1.GlobalPosition=new Vector2(800,800);
		var scene2=ResourceLoader.Load<PackedScene>("res://Scenes/MiniGame/piece2.tscn");
		var piece2=scene2.Instantiate<GamePiece>();
		AddChild(piece2);
		piece2.GlobalPosition=new Vector2(1000,800);
	}
	public override void _Process(double delta)
	{
	}
	public void GameEnd()
	{
		
	}
	public void setupPieces()
	{
		GeneratePieces();
		GeneratePieceNodes();
		for (int i = 0; i < piecesArray.Length; ++i)
		{
			var temp=ResourceLoader.Load<PackedScene>("res://Scenes/MiniGame/piece"+piecesArray[i]+".tscn");
			var piece=temp.Instantiate<GamePiece>();
			AddChild(piece);
			piecesList.Add(piece);
		}
		for (int i = 0; i < piecesList.Capacity; ++i)
		{
			GamePiece pieceMoved=piecesList[i];
			pieceMoved.GlobalPosition=pieceNodes[i];
		}
	}
	public void setupCollectionArea()
	{
		var collectionAreaTemp=ResourceLoader.Load<PackedScene>("res://Scenes/MiniGame/collectionArea.tscn");
		var collectionArea=collectionAreaTemp.Instantiate<CollectionArea>();
		AddChild(collectionArea);
		collectionArea.GlobalPosition=new Vector2(960,640);
	}
	public void GeneratePieces()
	{
		Random gen = new Random();
		for(int i = 0; i < 8; ++i)
		{
			piecesArray[i]=gen.Next(2)+1;
		}
	}
	public void GeneratePieceNodes()
	{
		pieceNodes=new List<Vector2>();
		pieceNodes.Add(new Vector2(960,100));
		pieceNodes.Add(new Vector2(960,1180));
		pieceNodes.Add(new Vector2(100,640));
		pieceNodes.Add(new Vector2(1820,640));
		pieceNodes.Add(new Vector2(100,100));
		pieceNodes.Add(new Vector2(100,1180));
		pieceNodes.Add(new Vector2(1820,100));
		pieceNodes.Add(new Vector2(1820,1180));
	}
}
