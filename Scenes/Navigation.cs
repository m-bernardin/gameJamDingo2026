using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Numerics;
using System.Threading.Tasks.Dataflow;
using System.Xml;

public partial class Navigation : Node2D
{
	// Called when the node enters the scene tree for the first time.
	Dictionary<Planet,ArrayList> PlanetTree;
	ArrayList Planets;
	Planet StartNode;
	Planet ChildNodeA;
	Planet ChildNodeB;
	Planet SelectedNode;
	bool RebootMap=false;
	public override void _Ready()
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
			AddChild(StartNode);
			StartNode.Position=new Godot.Vector2(1000,1000);
			FindChildren();
			AddChild(ChildNodeA);
			ChildNodeA.Position=new Godot.Vector2(400,400);
			AddChild(ChildNodeB);
			ChildNodeB.Position=new Godot.Vector2(1520,400);
		}
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (ChildNodeA.Pressed)
		{
			SelectedNode=ChildNodeA;
			SetUpScene();
		}
		else if (ChildNodeB != null && ChildNodeB.Pressed)
		{
			SelectedNode=ChildNodeB;
			SetUpScene();
		}
	}
	private void GeneratePlanets()
	{
		Planets=new ArrayList();

		var scene=ResourceLoader.Load<PackedScene>("res://Scenes/Planet.tscn");
		var Planet1=scene.Instantiate<Planet>();
		Planet1.CustomInit("Delta-99","add later",1,30,new[]{3},0,10);
		Planets.Add(Planet1);
		scene=ResourceLoader.Load<PackedScene>("res://Scenes/Planet.tscn");
		var Planet2=scene.Instantiate<Planet>();
		Planet2.CustomInit("Tanas","add later",0,40,new[]{2},1,50);
		Planets.Add(Planet2);
		scene=ResourceLoader.Load<PackedScene>("res://Scenes/Planet.tscn");
		var Planet3=scene.Instantiate<Planet>();
		Planet3.CustomInit("Tattooine","add later",1,0, Array.Empty<int>(), 0,0);
		Planets.Add(Planet3);
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
		FinalPlanet.CustomInit("Best","add later",1,0, Array.Empty<int>(), 0,0);
		BestPlanet.Add(FinalPlanet);
		foreach(Planet final in CurrentPlanets)
		{
			PlanetTree.Add(final,BestPlanet);
		}
	}
	public void SetUpScene()
	{
		if (SelectedNode.PlanetName.Equals("Best"))
		{
			GD.Print("You made it!!");
		}
		else{
			RemoveChild(StartNode);
			RemoveChild(ChildNodeA);
			RemoveChild(ChildNodeB);
			StartNode=SelectedNode;
			AddChild(StartNode);
			StartNode.Position=new Godot.Vector2(1000,1000);
			FindChildren();
			if (ChildNodeB != null)
			{
				AddChild(ChildNodeA);
				ChildNodeA.Position=new Godot.Vector2(400,400);
				AddChild(ChildNodeB);
				ChildNodeB.Position=new Godot.Vector2(1520,400);
			}
			else
			{
				AddChild(ChildNodeA);
				ChildNodeA.Position=new Godot.Vector2(1000,400);
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
}
