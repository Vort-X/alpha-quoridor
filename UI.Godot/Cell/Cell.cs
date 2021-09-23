using Godot;
using System;

public class Cell : Node2D
{
	public int X { get; set; }
	public int Y { get; set; }
	
	public override void _Ready()
	{
		
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

	private void _on_Area2D_input_event(object viewport, object @event, int shape_idx)
	{
		if (@event is InputEventMouseButton mouseEvent) 
		{
			if (mouseEvent.Pressed)
			{
				GD.Print($"X:{X}, Y:{Y} pressed");
			}
		}
	}
}



