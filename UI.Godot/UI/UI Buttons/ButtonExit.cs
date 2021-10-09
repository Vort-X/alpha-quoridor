using Godot;
using System;
using Quoridor.Game;
using Quoridor.UI;

public class ButtonExit : Node2D
{

	private Clickable _clickable;
	private PackedScene _mainMenuScene = ResourceLoader.Load<PackedScene>("res://UI/Main Menu/MainMenu.tscn");
	
	public override void _Ready()
	{
		_clickable = GetNode<Clickable>("Clickable");
		_clickable.OnMouseClickLeft += ExitToMainMenu;
	}

	public void ExitToMainMenu()
	{
		var mainMenu = _mainMenuScene.Instance<Control>();
		var sceneLoader = GetNode<SceneLoader>("/root/SceneLoader");
		sceneLoader.GotoScene(mainMenu);
	}
}
