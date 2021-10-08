using Godot;
using System;
using Quoridor.Board;
using Quoridor.Model;
using Quoridor.Model.Abstract;
using Quoridor.Model.TurnFactories;

public class GameSession : Node
{
	private static PackedScene _gameSessionScene = ResourceLoader.Load<PackedScene>("res://Game/GameSession.tscn");
	
	public Game Game { get; internal set; }

	public override void _Ready()
	{
		
	}
	
}
