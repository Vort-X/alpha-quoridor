using Godot;
using System;
using Quoridor.UI;
using Quoridor.UI.UI_Buttons;

public class ButtonWallVertical : ButtonWallBase
{
	
	protected Clickable Clickable;
	
	public override void _Ready()
	{
		base._Ready();
		WallRotationDegrees = 90f;
		Clickable = GetNode<Clickable>("Clickable");
		Clickable.OnMouseClickLeft += ButtonWallOnStartDrag;
	}
	
}
