using System;
using Godot;
using Quoridor.Game;
using Quoridor.UI;


public class Cell : Node2D
{
	public int X { get; set; }
	public int Y { get; set; }

	private Clickable _clickable;

	private Sprite _hightLight;

	public event Action<Tuple<int, int>> CellClicked;

	public bool IsHighlighted
	{
		set => _hightLight.Visible = value;
	}

	public override void _Ready()
	{
		_clickable = GetNode<Clickable>("Clickable");
		_hightLight = GetNode<Sprite>("Highlight");

		IsHighlighted = false;
		_clickable.OnMouseClickLeft += () =>
		{
			CellClicked?.Invoke(new Tuple<int, int>(X, Y));
		};
	}
	
}




