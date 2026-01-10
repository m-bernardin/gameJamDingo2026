using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks.Dataflow;
using System.Xml;

public partial class Navigation : Node2D
{
	// Called when the node enters the scene tree for the first time.
	Dictionary<Planet,ArrayList> PlanetTree;
	ArrayList Planets;
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
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	private void GeneratePlanets()
	{
		Planets=new ArrayList();

		Planet Planet1=new Planet("Delta-99","add later",1,30,new[]{3},0,30);
		Planets.Add(Planet1);
		Planet Planet2=new Planet("Tanas","add later",0,40,new[]{2},1,20);
		Planets.Add(Planet2);
		Planet Planet3=new Planet("Tattooine","add later",1,0, Array.Empty<int>(), 0,0);
		Planets.Add(Planet3);
	}

	private void GenerateTree()
	{
		PlanetTree=new Dictionary<Planet,ArrayList>();
		Random r=new Random();
		int index=r.Next(Planets.Count);
		ArrayList CurrentPlanets=new ArrayList();
		CurrentPlanets.Add(Planets[index]);
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
		BestPlanet.Add(new Planet("Best","add later",1,0, Array.Empty<int>(), 0,0));
		foreach(Planet final in CurrentPlanets)
		{
			PlanetTree.Add(final,BestPlanet);
		}
	}
}
