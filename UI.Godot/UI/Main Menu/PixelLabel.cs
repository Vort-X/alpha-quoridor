using Godot;
using System;
using Quoridor.Game;
using Quoridor.Model;

public class PixelLabel : Label
{

	[Export] private GameType _gameType = GameType.VsPlayer;

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
		if (!(@event is InputEventMouseButton mouseEvent)) return;
			
		var mouseButton = (ButtonList) mouseEvent.ButtonIndex;


		if (mouseButton == ButtonList.Left && mouseEvent.Pressed)
		{
			var sceneLoader = GetNode<SceneLoader>("/root/SceneLoader");
			var gameSession = GetNode<GameSession>("/root/GameSession");
			var game = _gameType switch
			{
				GameType.VsAi => GameCreator.NewGameVsBot(gameSession),
				GameType.VsPlayer => GameCreator.NewGameVsPlayer(gameSession)
			};

			gameSession.Game = game;
			
			sceneLoader.GotoScene(GameUI.CreateGameUi());
		}
	}
}

public enum GameType
{
	VsAi,
	VsPlayer
}










