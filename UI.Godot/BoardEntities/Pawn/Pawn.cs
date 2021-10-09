using Godot;
using System;

public class Pawn : Node2D
{
	private static PackedScene _pawnScene = ResourceLoader.Load<PackedScene>("res://BoardEntities/Pawn/Pawn.tscn");

	public static Pawn CreateAndAddBlackPawn(Node parent)
	{
		var pawn = _pawnScene.Instance<Pawn>();
		parent.AddChild(pawn);
		pawn._sprite.Frame = 0;
		return pawn;
	}
	
	public static Pawn CreateAndAddWhitePawn(Node parent)
	{
		var pawn = _pawnScene.Instance<Pawn>();
		parent.AddChild(pawn);
		pawn._sprite.Frame = 1;
		return pawn;
	}

	private Sprite _sprite;
	
	public override void _Ready()
	{
		_sprite = GetNode<Sprite>("Sprite");
	}
	
}
