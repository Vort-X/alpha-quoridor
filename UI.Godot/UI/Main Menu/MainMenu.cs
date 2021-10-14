using Godot;
using System;
using Quoridor.Game;

public class MainMenu : Control
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var sceneLoader = GetNode<SceneLoader>("/root/SceneLoader");
		sceneLoader.CurrentScene = this;
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
