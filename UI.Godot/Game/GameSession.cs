using Godot;
using System;
using Quoridor.Board;
using Quoridor.Model;
using Quoridor.Model.Abstract;

public class GameSession : Node
{
	private static PackedScene _gameSessionScene = ResourceLoader.Load<PackedScene>("res://Game/GameSession.tscn");

	public static GameSession CreateGameSession(Game game)
	{
		var gameSession = _gameSessionScene.Instance<GameSession>();
		gameSession.Game = game;
		return gameSession;
	}
	
	public Game Game { get; set; }

	public override void _Ready()
	{
		GD.Print("GameSession is ready.");
		GD.Print(Game == null);
		var gameUi = GetNode<GameUI>("GameUI");
		gameUi.BoardPresenter = Game.GameManager.BoardPresenter;
		var board = GetNode<Board>("GameUI/Board");
		board.BoardPresenter = Game.GameManager.BoardPresenter;
	}
	
}
