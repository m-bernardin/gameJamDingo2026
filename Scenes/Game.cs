using Godot;
using System;

public partial class Game : Node
{
	private Node _currentScene;
	private Godot.Collections.Dictionary<string, PackedScene> _loadedScenes = new();

	public override void _Ready()
	{
		// Load the initial scene
	
	}
}
