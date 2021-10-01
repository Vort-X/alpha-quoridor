using Godot;
using System;
using Quoridor.UI;

public class InteractiveWall : Node2D
{
	public Draggable Draggable { get; private set; }
	public Clickable Clickable { get; private set; }
	
	public override void _Ready()
	{
		Draggable = GetNode<Draggable>("Draggable");
		Clickable = GetNode<Clickable>("Clickable");
	}

}
