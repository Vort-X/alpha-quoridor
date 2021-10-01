using Godot;
using System;
using System.Collections.Generic;
using Quoridor.Game;
using Quoridor.UI;

public class Corner : Node2D
{
	private Clickable _clickable;
	private Sprite _highLight;

	public int X { get; set; }
	public int Y { get; set; }

	public List<Cell> Cells = new List<Cell>();

	public event Action<Tuple<int, int>> CornerClicked; 

	public override void _Ready()
	{
		_clickable = GetNode<Clickable>("Clickable");
		_highLight = GetNode<Sprite>("HighLight");

		_highLight.Visible = false;
		_clickable.OnMouseClickLeft += () => CornerClicked?.Invoke(new Tuple<int, int>(X, Y));
	}

	public void OnWallStartDragging()
	{
		_highLight.Visible = true;
	}
	
	public void OnWallStopDragging()
	{
		_highLight.Visible = false;
	}
	
}
