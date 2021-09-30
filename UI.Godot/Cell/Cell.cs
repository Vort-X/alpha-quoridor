using System;
using Godot;
using Quoridor.Game;
using Quoridor.UI;


public class Cell : Node2D
{
	public int X { get; set; }
	public int Y { get; set; }

	private Clickable _clickable;
	private Quoridor.Game.Game _game;

	public override void _Ready()
	{
		_clickable = GetNode<Clickable>("Clickable");
		_game = GetNode<Quoridor.Game.Game>("/root/Game");
		_clickable.OnMouseClickLeft += () =>
		{
			_game.OnCellClicked(new Tuple<int, int>(X, Y));
		};
	}
}




