using Godot;
using System;
using Quoridor.Model.Abstract;

public class GameUI : Control
{
	public IBoardPresenter BoardPresenter { get; set; }
	
	public override void _Ready()
	{
		
	}
}
