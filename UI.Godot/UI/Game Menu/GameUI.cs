using Godot;
using System;
using Quoridor.Model.Abstract;

public class GameUI : Control
{
	private static PackedScene _gameUiScene = ResourceLoader.Load<PackedScene>("res://UI/Game Menu/GameUI.tscn");

	public static GameUI CreateGameUi()
	{
		return _gameUiScene.Instance<GameUI>();
	}
	
	public IBoardPresenter BoardPresenter { get; set; }
	
	
	public override void _Ready()
	{
		BoardPresenter = GetNode<GameSession>("/root/GameSession").Game.GameManager.BoardPresenter;
	}
}
