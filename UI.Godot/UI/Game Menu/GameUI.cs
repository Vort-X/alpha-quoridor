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
	private Label _pawn1WallsLabel;
	private Label _pawn2WallsLabel;
	private Label _errorMessageLabel;
	
	public override void _Ready()
	{
		_pawn1WallsLabel = GetNode<Label>("Pawn1Walls/Label");
		_pawn2WallsLabel = GetNode<Label>("Pawn2Walls/Label");
		_errorMessageLabel = GetNode<Label>("ErrorMessage");
		
		var gameSession = GetNode<GameSession>("/root/GameSession");
		gameSession.GameStart += OnGameStart;
		gameSession.GameEnd += OnGameEnd;
	}

	private void OnGameStart()
	{
		var gameManager = GetNode<GameSession>("/root/GameSession").Game.GameManager;
		BoardPresenter = gameManager.BoardPresenter;

		gameManager.BoardUpdated += OnBoardUpdated;
		gameManager.InvalidTurn += DisplayError;

		DrawPawnWallsCount();
	}

	private void OnBoardUpdated()
	{
		_errorMessageLabel.Text = string.Empty;
		DrawPawnWallsCount();
	}

	private void OnGameEnd()
	{
		var gameManager = GetNode<GameSession>("/root/GameSession").Game.GameManager;
		
		gameManager.BoardUpdated -= OnBoardUpdated;
		gameManager.InvalidTurn -= DisplayError;
		
	}

	private void DrawPawnWallsCount()
	{
		_pawn1WallsLabel.Text = $"Walls: {BoardPresenter.Pawn1.AvailableWalls}";
		_pawn2WallsLabel.Text = $"Walls: {BoardPresenter.Pawn2.AvailableWalls}";
	}

	private async void DisplayError(string errorMessage)
	{
		_errorMessageLabel.Text = errorMessage;
		await ToSignal(GetTree().CreateTimer(5), "timeout");
		if (_errorMessageLabel.Text == errorMessage)
			_errorMessageLabel.Text = string.Empty;
	}
}
