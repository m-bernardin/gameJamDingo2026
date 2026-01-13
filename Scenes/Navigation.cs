using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection.Emit;
using System.Threading.Tasks.Dataflow;
using System.Xml;
using System.Linq;
using System.Diagnostics.Tracing;

//checking that this branch works properly
public partial class Navigation : Node2D
{
	bool MinigameRunning=false;
	// Map
	Dictionary<Planet,ArrayList> PlanetTree;
	ArrayList Planets;
	Planet StartNode;
	Planet ChildNodeA;
	Planet ChildNodeB;
	Planet SelectedNode;
	bool RebootMap=false;
	bool Preexisted=false;

	//Minigame
	[Export]
	public PackedScene[] piecesRef;
	private Player player;
	private List<GamePiece> piecesList=new List<GamePiece>();
	private List<Godot.Vector2> pieceNodes;
	private CollectionArea collectionArea;

	//Map
	public override void _Ready()
	{
		if(!MinigameRunning)
		{
			GeneratePlanets();
			GenerateTree();
			foreach(KeyValuePair<Planet,ArrayList> pair in PlanetTree)
			{
				GD.Print("Key");
				GD.Print(pair.Key.PlanetName);
				GD.Print("Values");
				foreach(Planet value in pair.Value)
				{
					GD.Print(value.PlanetName);
				}
				Random r = new Random();
				StartNode.SetSprite("res://Sprites/Planet0.png");
				AddChild(StartNode);
				StartNode.Position=new Godot.Vector2(960,900);
				FindChildren();
				ChildNodeA.SetSprite("res://Sprites/Planet"+(r.Next(6)+2)+".png");
				AddChild(ChildNodeA);
				ChildNodeA.Position=new Godot.Vector2(400,400);
				ChildNodeB.SetSprite("res://Sprites/Planet"+(r.Next(6)+2)+".png");
				AddChild(ChildNodeB);
				ChildNodeB.Position=new Godot.Vector2(1520,400);
			}
		}
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		Godot.Label Description=GetNode<Godot.Label>("PlanetDescription");
		Godot.Label StatBar=GetNode<Godot.Label>("StatBar");
		Button ExploreButton=GetNode<Button>("ExplorePlanet");
		Player Alien=(Player)GetNode<Node2D>("Player");

		StatBar.Text="Oxygen: "+Alien.Stats[0]+"  Energy: "+Alien.Stats[1]+"  Weight: "+Alien.Stats[2]+"  Speed: "+Alien.Stats[3]+"  Durability: "+Alien.Stats[4];
		
		if (SelectedNode != null)
		{
			Description.Text=SelectedNode.PlanetName+"\n"+SelectedNode.FlavourText;
			Description.SetVisible(true);
			ExploreButton.SetVisible(true);
		}
		if (ChildNodeA.Pressed)
		{
			SelectedNode=ChildNodeA;
		}
		else if (ChildNodeB != null && ChildNodeB.Pressed)
		{
			SelectedNode=ChildNodeB;
		}
		else
		{
			SelectedNode=null;
			Description.SetVisible(false);
			ExploreButton.SetVisible(false);
			
		}
	}
	private void GeneratePlanets()
	{
		Planets=new ArrayList();

		var scene=ResourceLoader.Load<PackedScene>("res://Scenes/Planet.tscn");
		var Planet1=scene.Instantiate<Planet>();
		Planet1.CustomInit("Delta-99","The high levels of oxygen on this planet has allowed for the development of large, hostile life forms that must be avoided.",1,20,new[]{2},0,10);
		Planets.Add(Planet1);
		scene=ResourceLoader.Load<PackedScene>("res://Scenes/Planet.tscn");
		var Planet2=scene.Instantiate<Planet>();
		Planet2.CustomInit("Tanas","The fragile surface of this planet cracks easily, but below lies a significant energy source.",0,10,new[]{4},1,20);
		Planets.Add(Planet2);
		scene=ResourceLoader.Load<PackedScene>("res://Scenes/Planet.tscn");
		var Planet3=scene.Instantiate<Planet>();
		Planet3.CustomInit("Tattooine","This planet is fairly safe, despite the high winds and sinkholes, but it is also barren, aside from some old engine parts.",3,15, new[]{3,4}, 2,2);
		Planets.Add(Planet3);
		scene=ResourceLoader.Load<PackedScene>("res://Scenes/Planet.tscn");
		var Planet4=scene.Instantiate<Planet>();
		Planet4.CustomInit("Pandora","The alien life on this planet chases out outsiders on the back of flying mounts, but the superconductors found on the surface are valuable.",1,15, new[]{2}, 2,10);
		Planets.Add(Planet4);
		scene=ResourceLoader.Load<PackedScene>("res://Scenes/Planet.tscn");
		var Planet5=scene.Instantiate<Planet>();
		Planet5.CustomInit("Delta-99","The high levels of oxygen on this planet has allowed for the development of large, hostile life forms that must be avoided.",1,20,new[]{3},0,10);
		Planets.Add(Planet5);
		scene=ResourceLoader.Load<PackedScene>("res://Scenes/Planet.tscn");
		var Planet6=scene.Instantiate<Planet>();
		Planet6.CustomInit("Giant's Deep","This planet is quite peaceful, as long as you can withstand the storms. Pockets of breathable air are present on a few islands on the surface.",2,5,new[]{4},0,10);
		Planets.Add(Planet6);
		scene=ResourceLoader.Load<PackedScene>("res://Scenes/Planet.tscn");
		var Planet7=scene.Instantiate<Planet>();
		Planet7.CustomInit("Kavaron","The cracked surface of this planet can destabalize ships that land on it, but the rocks are rich with surdy metals.",4,20,new[]{2},0,20);
		Planets.Add(Planet7);
	}
	private void GenerateTree()
	{
		PlanetTree=new Dictionary<Planet,ArrayList>();
		Random r=new Random();
		int index=0;
		ArrayList CurrentPlanets=new ArrayList();
		CurrentPlanets.Add(Planets[index]);
		StartNode=(Planet)Planets[index];
		Planets.RemoveAt(index);
		while (Planets.Count>=CurrentPlanets.Count*2)
		{
			ArrayList NextPlanets=new ArrayList();
			foreach(Planet Node in CurrentPlanets)
			{
				ArrayList ChildNodes=new ArrayList();
				for(int i=0; i<2; i++)
				{
					index=r.Next(Planets.Count);
					ChildNodes.Add(Planets[index]);
					NextPlanets.Add(Planets[index]);
					Planets.RemoveAt(index);
				}
				PlanetTree.Add(Node,ChildNodes);
			}
			CurrentPlanets=NextPlanets;
		}
		ArrayList BestPlanet=new ArrayList();
		var scene=ResourceLoader.Load<PackedScene>("res://Scenes/Planet.tscn");
		var FinalPlanet=scene.Instantiate<Planet>();
		FinalPlanet.CustomInit("Eden","A beautiful, lush planet with bountiful water, food and air.",1,0, Array.Empty<int>(), 0,0);
		BestPlanet.Add(FinalPlanet);
		foreach(Planet final in CurrentPlanets)
		{
			PlanetTree.Add(final,BestPlanet);
		}
	}
	public void SetUpScene()
	{
		if (SelectedNode.PlanetName.Equals("Eden"))
		{
			GD.Print("You made it!!");
			GetTree().ChangeSceneToFile("res://Scenes/win_screen.tscn");
		}
		else{
			RemoveChild(StartNode);
			RemoveChild(ChildNodeA);
			RemoveChild(ChildNodeB);
			StartNode=SelectedNode;
			AddChild(StartNode);
			StartNode.Position=new Godot.Vector2(960,900);
			FindChildren();
			if (ChildNodeB != null)
			{
				Random r = new Random();
				AddChild(ChildNodeA);
				ChildNodeA.SetSprite("res://Sprites/Planet"+(r.Next(6)+2)+".png");
				ChildNodeA.Position=new Godot.Vector2(400,400);
				ChildNodeB.SetSprite("res://Sprites/Planet"+(r.Next(6)+2)+".png");
				AddChild(ChildNodeB);
				ChildNodeB.Position=new Godot.Vector2(1520,400);
			}
			else
			{
				ChildNodeA.SetSprite("res://Sprites/Planet1.png");
				AddChild(ChildNodeA);
				ChildNodeA.Position=new Godot.Vector2(960,400);
				Sprite2D BG=GetNode<Sprite2D>("Background");
				BG.Texture=GD.Load<Texture2D>("res://Sprites/BGFinal.jpeg");
			}
		}
	}
	private void FindChildren()
	{
		ArrayList Children=PlanetTree[StartNode];
		ChildNodeA=(Planet)Children[0];
		if (Children.Count > 1)
		{
			ChildNodeB=(Planet)Children[1];
		}
		else
		{
			ChildNodeB=null;
		}
	}

	private void RunChallenge()
	{
		Player Alien=(Player)GetNode<Node2D>("Player");
		int Odds=Alien.GetOdds(SelectedNode.Stats,SelectedNode.ChallengeType);
		Godot.Label Feedback=GetNode<Godot.Label>("SuccessFeedback");
		String FeedbackMessage="";
		GD.Print(Odds);
		GD.Print(SelectedNode.Difficulty);
		Odds=Odds-SelectedNode.Difficulty;
		GD.Print(Odds);
		Random r=new Random();
		int roll=r.Next(100);
		if (roll <= Odds+30)
		{
			GD.Print("Pass!!");
			GD.Print(roll);
			GD.Print(Odds);
			Alien.Stats[SelectedNode.Ressource]=Alien.Stats[SelectedNode.Ressource]+SelectedNode.Qty;
			Alien.Stats[0]=Alien.Stats[0]-10;
			Alien.Stats[1]=Alien.Stats[1]-10;
			if (roll > Odds)
			{
				FeedbackMessage+="The ship took a hit in this landing";
				if (SelectedNode.Stats.Contains(2))
				{
					FeedbackMessage+=", the thrusters are damaged";
				}
				if (SelectedNode.Stats.Contains(3))
				{
					FeedbackMessage+=", its heavier now ig";
				}
				if (SelectedNode.Stats.Contains(4))
				{
					FeedbackMessage+=", the outer shell is damaged";
				}
				FeedbackMessage+=".\n";
				GD.Print("landing successfull but ship took a hit :(");
				int Mod=20/SelectedNode.Stats.Length;
				foreach(int AffectedStat in SelectedNode.Stats)
				{
					Alien.Stats[AffectedStat]=Alien.Stats[AffectedStat]-Mod;
					if (Alien.Stats[AffectedStat] <= 0)
					{
						GD.Print("ship was damadged beyond repair");
						GD.Print(roll);
						GD.Print(Odds);
						GetTree().ChangeSceneToFile("res://Scenes/lose_screen.tscn");
					}
				}
			}
			else
			{
				FeedbackMessage+="This landing was a great success!! \n";
			}
			foreach(int stat in Alien.Stats)
			{
				GD.Print(stat);
			}
			String[]Ressources=new[]{"oxygen","energy","speed","weight","durability"};
			FeedbackMessage+="Your "+Ressources[SelectedNode.Ressource]+" has been increased.";
			Feedback.Text=FeedbackMessage;
			HideMap();
			SetUpMinigame();
			SetUpScene();
		}
		else
		{
			GD.Print("sorry you lose");
			GD.Print(roll);
			GD.Print(Odds);
			GetTree().ChangeSceneToFile("res://Scenes/lose_screen.tscn");
		}
	}

	private void ExplorePressed()
	{
		RunChallenge();
		Godot.Label Description=GetNode<Godot.Label>("PlanetDescription");
		Button ExploreButton=GetNode<Button>("ExplorePlanet");
		Description.SetVisible(false);
		ExploreButton.SetVisible(false);
	}
	private void HideMap()
	{
		StartNode.SetVisible(false);
		ChildNodeA.SetVisible(false);
		ChildNodeB.SetVisible(false);
		Godot.Label Description=GetNode<Godot.Label>("PlanetDescription");
		Godot.Button ExploreButton=GetNode<Godot.Button>("ExplorePlanet");
		Description.SetVisible(false);
		ExploreButton.SetVisible(false);
	}
	private void SetUpMap()
	{
		StartNode.SetVisible(true);
		ChildNodeA.SetVisible(true);
		ChildNodeB.SetVisible(true);
	}

//Minigame
public void SetUpMinigame()
	{
		setupCollectionArea();
		setupPieces();
	}
	public void GameEnd()
	{
		int score=0;
		GamePiece[] scoringCandidates = null;
		if (collectionArea.GetCollectedPieces().Length > 0 )
		{
			scoringCandidates = Array.ConvertAll(collectionArea.GetCollectedPieces(), item => (GamePiece) item);
			for(int i = 0; i < scoringCandidates.Length; ++i)
			{
				bool valid=true;
				GamePiece candidate=scoringCandidates[i];
				Area2D[] overlaps=candidate.GetOverlappingAreas().ToArray();
				if(overlaps.Length>0){
					for(int j = 0; j < overlaps.Length; ++j)
					{
						Area2D overlap=overlaps[j];
						if (overlap.AudioBusOverride)
						{
							valid=false;
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
		collectionArea.GlobalPosition=new Godot.Vector2(960,640);
		var storageAreaTemp=ResourceLoader.Load<PackedScene>("res://Scenes/MiniGame/storageArea.tscn");
		StorageArea storageArea=storageAreaTemp.Instantiate<StorageArea>();
		AddChild(storageArea);
		storageArea.GlobalPosition=new Godot.Vector2(960,640);
	}
	public void GeneratePieceNodes()
	{
		pieceNodes=new List<Godot.Vector2>();
		pieceNodes.Add(new Godot.Vector2(960,100));
		pieceNodes.Add(new Godot.Vector2(960,1180));
		pieceNodes.Add(new Godot.Vector2(100,640));
		pieceNodes.Add(new Godot.Vector2(1820,640));
		pieceNodes.Add(new Godot.Vector2(100,100));
		pieceNodes.Add(new Godot.Vector2(100,1180));
		pieceNodes.Add(new Godot.Vector2(1820,100));
		pieceNodes.Add(new Godot.Vector2(1820,1180));
	}
}
