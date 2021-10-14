using Godot;
using System;
using Quoridor.Game;
using Quoridor.UI;

public class ButtonExit : Node2D
{

	private Clickable _clickable;

	public override void _Ready()
	{
		_clickable = GetNode<Clickable>("Clickable");
		_clickable.OnMouseClickLeft += ExitToMainMenu;
	}

	public void ExitToMainMenu()
	{
		var mainMenu = GetNode<Control>("/root/MainMenu");
		var sceneLoader = GetNode<SceneLoader>("/root/SceneLoader");
		var gameSession = GetNode<GameSession>("/root/GameSession");

		gameSession.Game = null;
		sceneLoader.GotoScene(mainMenu);
	}
}
