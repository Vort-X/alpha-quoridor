using Godot;
using System;
using Quoridor.Game;
using Quoridor.Model;

public class PixelLabel : Label
{

	[Export] private MenuOption _menuOption = MenuOption.VsPlayer;

	DynamicFontData _fontData = ResourceLoader.Load<DynamicFontData>("res://UI/Main Menu/press-start/prstartk.ttf");
	public override void _Ready()
	{
		
	}

	private void _on_PixelMenuItemLabel_mouse_entered()
	{
		var newFont = CreateFontWithOutlineSize(2);
		AddFontOverride("font", newFont);
	}

	private DynamicFont CreateFontWithOutlineSize(int outlineSize)
	{
		var newFont = new DynamicFont();
		newFont.FontData = _fontData;
		newFont.Size = 10;
		newFont.OutlineSize = outlineSize;
		newFont.OutlineColor = Colors.Black;
		return newFont;
	}

	private void _on_PixelMenuItemLabel_mouse_exited()
	{
		var newFont = CreateFontWithOutlineSize(1);
		AddFontOverride("font", newFont);
	}
	
	private void _on_PixelMenuItemLabel_gui_input(object @event)
	{
		if (_menuOption is MenuOption.Exit)
		{
			GetTree().Quit();
			return;
		}

		if (!(@event is InputEventMouseButton mouseEvent)) return;
			
		var mouseButton = (ButtonList) mouseEvent.ButtonIndex;


		if (mouseButton == ButtonList.Left && mouseEvent.Pressed)
		{
			var sceneLoader = GetNode<SceneLoader>("/root/SceneLoader");
			var gameSession = GetNode<GameSession>("/root/GameSession");
			var gameUi = GetNode<GameUI>("/root/GameUi");
			var game = _menuOption switch
			{
				MenuOption.VsAi => GameCreator.NewGameVsBot(gameSession),
				MenuOption.VsPlayer => GameCreator.NewGameVsPlayer(gameSession)
			};

			gameSession.Game = game;
			
			_on_PixelMenuItemLabel_mouse_exited();
			sceneLoader.GotoScene(gameUi);
			
		}
	}
}

public enum MenuOption
{
	VsAi,
	VsPlayer,
	Exit
}










