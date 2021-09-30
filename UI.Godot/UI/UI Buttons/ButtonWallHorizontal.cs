using Godot;
using System;
using Quoridor.UI;
using Quoridor.UI.UI_Buttons;


public class ButtonWallHorizontal : ButtonWallBase
{
	
	protected Clickable Clickable;

	public override void _Ready()
	{
		base._Ready();
		WallRotationDegrees = 0f;
		Clickable = GetNode<Clickable>("Clickable");
		Clickable.OnMouseClickLeft += ButtonWallOnStartDrag;
	}

}
