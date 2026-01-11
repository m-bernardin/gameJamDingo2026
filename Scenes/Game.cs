using Godot;
using System;

public partial class Game : Node
{
	private Node _currentScene;
	private Godot.Collections.Dictionary<string, PackedScene> _loadedScenes = new();

	public override void _Ready()
	{
		// Load the initial scene
		LoadAndAddScene("res://Scenes/World.tscn");
	}

	public void LoadAndAddScene(string scenePath, bool unloadPrevious = true)
	{
		// Unload the previous scene if needed
		if (unloadPrevious && _currentScene != null)
		{
			_currentScene.QueueFree(); // Use QueueFree to safely remove the node
			_currentScene = null;
		}

		// Load the new scene if it hasn't been loaded before
		if (!_loadedScenes.ContainsKey(scenePath))
		{
			var packedScene = GD.Load<PackedScene>(scenePath);
			_loadedScenes[scenePath] = packedScene;
		}

		// Instantiate and add the new scene instance to the SceneManager
		var newSceneInstance = _loadedScenes[scenePath].Instantiate();
		AddChild(newSceneInstance);
		_currentScene = newSceneInstance; // Keep reference to the current active scene
	}

	// Example of a function to add a scene on top of the current one (like an inventory menu)
	public void AddOverlayScene(string scenePath)
	{
		var packedScene = GD.Load<PackedScene>(scenePath);
		var overlayInstance = packedScene.Instantiate();
		AddChild(overlayInstance);
	}
}
