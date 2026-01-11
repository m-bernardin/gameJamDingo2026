using Godot;
using System;
using System.Collections.Generic;

public partial class MiniGameManager : Node2D
{
	
	[Export]
	public PackedScene[] piecesRef;
	private Player player;
	private List<GamePiece> piecesList=new List<GamePiece>();
	private List<Vector2> pieceNodes;
	public override void _Ready()
	{
		/**
		var scene1=ResourceLoader.Load<PackedScene>("res://Scenes/MiniGame/piece1.tscn");
		var piece1=scene1.Instantiate<GamePiece>();
		AddChild(piece1);
		piece1.GlobalPosition=new Vector2(800,800);
		var scene2=ResourceLoader.Load<PackedScene>("res://Scenes/MiniGame/piece2.tscn");
		var piece2=scene2.Instantiate<GamePiece>();
		AddChild(piece2);
		piece2.GlobalPosition=new Vector2(1000,800);
		**/
		setupPieces();
		setupCollectionArea();
	}
	public override void _Process(double delta)
	{
	}
	public void GameEnd()
	{
		
	}
	public void setupPieces()
	{
		GeneratePieceNodes();
		for (int i = 0; i < 8; ++i)
		{
			int index=GD.RandRange(0,piecesRef.Length-1);
			var piece=piecesRef[index].Instantiate<GamePiece>();
			GD.Print("piece "+i+" created, position: "+piece.GlobalPosition);
			piece.GlobalPosition=pieceNodes[i];
			GD.Print("piece "+i+" placed, position: "+piece.GlobalPosition);
			piecesList.Add(piece);
		}
	}
	public void setupCollectionArea()
	{
		var collectionAreaTemp=ResourceLoader.Load<PackedScene>("res://Scenes/MiniGame/collectionArea.tscn");
		var collectionArea=collectionAreaTemp.Instantiate<CollectionArea>();
		AddChild(collectionArea);
		collectionArea.GlobalPosition=new Vector2(960,640);
	}
	public void GeneratePieceNodes()
	{
		GD.Print("generating nodes");
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
