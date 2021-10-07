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
	private bool _areCellsHighlighted;

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
		if (_areCellsHighlighted)
			_on_Area2D_mouse_exited();
		_highLight.Visible = false;
	}
	
	private void _on_Area2D_mouse_entered()
	{
		if (!_highLight.Visible) return;
		_areCellsHighlighted = true;
		foreach (var cell in Cells)
		{
			cell.IsHighlighted = true;
		}
	}


	private void _on_Area2D_mouse_exited()
	{
		if (!_highLight.Visible) return;
		_areCellsHighlighted = false;
		foreach (var cell in Cells)
		{
			cell.IsHighlighted = false;
		}
	}
	
}



