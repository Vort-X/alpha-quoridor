using Godot;
using System;

public class Draggable : Node2D
{
	public bool IsDragging { get; set; }

	public override void _Ready()
	{
		
	}
	
	public override void _Process(float delta)
	{
		if (IsDragging)
		{
			var mousePosition = GetViewport().GetMousePosition();
			var parent = GetParent<Node2D>();
			parent.Position = new Vector2(mousePosition.x, mousePosition.y);
		}
	}
}
