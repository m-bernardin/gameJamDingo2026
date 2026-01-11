using Godot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

public partial class MiniGameManager : Node2D
{
	
	[Export]
	public PackedScene[] piecesRef;
	private Player player;
	private List<GamePiece> piecesList=new List<GamePiece>();
	private List<Vector2> pieceNodes;
	private CollectionArea collectionArea;
	public override void _Ready()
	{
		setupCollectionArea();
		setupPieces();
	}
	public override void _Process(double delta)
	{
	}
	public void GameEnd()
	{
		GD.Print("received game end");
		int score=0;
		GD.Print("reset score");
		GamePiece[] scoringCandidates = null;
		if (collectionArea.GetCollectedPieces().Length > 0 )
		{
			scoringCandidates = Array.ConvertAll(collectionArea.GetCollectedPieces(), item => (GamePiece) item);
			GD.Print("Overlapping objects obtained: "+scoringCandidates);
			for(int i = 0; i < scoringCandidates.Length; ++i)
			{
				bool valid=true;
				GamePiece candidate=scoringCandidates[i];
				GD.Print("Checking candidate: "+candidate);
				Area2D[] overlaps=candidate.GetOverlappingAreas().ToArray();
				GD.Print("obtained overlaps: "+overlaps);
				if(overlaps.Length>0){
					for(int j = 0; j < overlaps.Length; ++j)
					{
						Area2D overlap=overlaps[i];
						GD.Print("checking overlap: overlap");
						if (overlap.AudioBusOverride)
						{
							valid=false;
							GD.Print("");
						}
					}
				}
				if(valid)score+=candidate.GetSize();
			}
		}
		GD.Print("your score was "+score);
	}
	public void setupPieces()
	{
		GeneratePieceNodes();
		for (int i = 0; i < 8; ++i)
		{
			int index=GD.RandRange(0,piecesRef.Length-1);
			var piece=piecesRef[index].Instantiate<GamePiece>();
			AddChild(piece);
			piece.GlobalPosition=pieceNodes[i];
			piecesList.Add(piece);
		}
	}
	public void setupCollectionArea()
	{
		var collectionAreaTemp=ResourceLoader.Load<PackedScene>("res://Scenes/MiniGame/collectionArea.tscn");
		collectionArea=collectionAreaTemp.Instantiate<CollectionArea>();
		AddChild(collectionArea);
		collectionArea.GlobalPosition=new Vector2(960,640);
		var storageAreaTemp=ResourceLoader.Load<PackedScene>("res://Scenes/MiniGame/storageArea.tscn");
		StorageArea storageArea=storageAreaTemp.Instantiate<StorageArea>();
		AddChild(storageArea);
		storageArea.GlobalPosition=new Vector2(960,640);
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
