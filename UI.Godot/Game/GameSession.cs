using Godot;
using System;
using System.Linq;
using Quoridor.Board;
using Quoridor.Game;
using Quoridor.Model;
using Quoridor.Model.Abstract;
using Quoridor.Model.PlayerTypes;
using Quoridor.Model.TurnFactories;


public class GameSession : Node, ITurnProvider
{
	private static PackedScene _gameSessionScene = ResourceLoader.Load<PackedScene>("res://Game/GameSession.tscn");
	
	public Game Game { get; internal set; }
	
	private UiPresenter _uiPresenter;
	private LocalPlayer _turnReceiver;
	private Timer _timer;

	public override void _Ready()
	{
		_uiPresenter = GetNode<UiPresenter>("/root/UiPresenter");
		_uiPresenter.CellClicked += OnCellTurn;
		_uiPresenter.WallPlaced += OnWallTurn;
	}

	public override void _Process(float delta)
	{
		Game?.GameManager.GameLoop();
	}

	public async void RequestTurn(LocalPlayer turnReceiver)
	{
		await ToSignal(GetTree().CreateTimer(0.5f), "timeout");
		_uiPresenter.OnInputRequested(Game.GameManager.TurnCheckService
			.FindAvaliableNeighbours(turnReceiver.Pawn.Cell)
			.Select(c => new Tuple<int, int>(c.X, c.Y))
			.ToList());
		_turnReceiver = turnReceiver;
		GD.Print($"Turn requested by {turnReceiver}.");
	}

	private void OnCellTurn(Tuple<int, int> cellCoordinates)
	{
		_turnReceiver?.OnCellTurn(cellCoordinates);
	}
	
	private void OnWallTurn(Tuple<int, int> cellCoordinates, bool isHorizontal)
	{
		_turnReceiver?.OnWallTurn(cellCoordinates, isHorizontal);
	}
	
}
