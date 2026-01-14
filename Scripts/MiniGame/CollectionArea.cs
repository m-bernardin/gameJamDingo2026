using Godot;
using System;
using System.Formats.Asn1;
using System.Linq;
using System.Collections;
using System.Collections.Immutable;

public partial class CollectionArea : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		InputPickable=false;
		AudioBusOverride=false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public Area2D[] GetCollectedPieces()
	{
		
		Area2D[] Overlaps=GetOverlappingAreas().ToArray();
		ArrayList ValidPieces = new ArrayList();
		foreach(Area2D Overlap in Overlaps)
		{
			if(Overlap is GamePiece){
				ValidPieces.Add(Overlap);
			}
		}
		return ValidPieces.ToArray().Cast<Area2D>().ToArray();
	}
}
