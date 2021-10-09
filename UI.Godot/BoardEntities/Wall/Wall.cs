using Godot;
using System;

public class Wall : Node2D
{

	private static PackedScene _wallScene = ResourceLoader.Load<PackedScene>("res://BoardEntities/Wall/Wall.tscn");

	public static Wall CreateHorizontalWall()
	{
		var wall = _wallScene.Instance<Wall>();
		wall.RotationDegrees = 0;
		return wall;
	}
	
	public static Wall CreateVerticalWall()
	{
		var wall = _wallScene.Instance<Wall>();
		wall.RotationDegrees = 90;
		return wall;
	}
	
	public override void _Ready()
	{
		
	}
}
