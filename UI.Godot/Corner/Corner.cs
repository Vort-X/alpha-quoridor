using Godot;
using System;
using System.Collections.Generic;

using Quoridor.UI;

public class Corner : Node2D
{
	private Clickable _clickable;
	private Quoridor.Game.Game _game;
	
	public int X { get; set; }
	public int Y { get; set; }

	public List<Cell> Cells = new List<Cell>();

	public override void _Ready()
	{
		_clickable = GetNode<Clickable>("Clickable");
		_game = GetNode<Quoridor.Game.Game>("/root/Game");

		_clickable.OnMouseClickLeft += () => _game.OnWallPlaced(new Tuple<int, int>(X, Y));
	}
	
}
