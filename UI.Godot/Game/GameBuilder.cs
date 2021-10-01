using Godot;
using Quoridor.UI.UI_Buttons;

namespace Quoridor.Game
{
	public class GameBuilder: Node
	{
		private Game _game;

		public override void _Ready()
		{
			_game =  GetNode<Game>("/root/Game");
		}
		
		public void ConnectCell(Cell cell)
		{
			cell.CellClicked += _game.OnCellClicked;
		}

		public void ConnectCorner(Corner corner)
		{
			corner.CornerClicked += _game.OnWallPlaced;
			
			_game.WallStartDragging += corner.OnWallStartDragging;
			_game.WallStopDragging += corner.OnWallStopDragging;
		}

		public void ConnectButton(ButtonWallBase buttonWall)
		{
			buttonWall.ButtonWallClicked += _game.OnWallButtonClicked;

			_game.WallPlaced += buttonWall.OnWallPlaced;
		}

		public void ConnectInteractiveWall(InteractiveWall draggedWall)
		{
			draggedWall.Clickable.OnMouseClickRight += () =>
			{
				_game.OnWallDeleted();
				draggedWall.QueueFree();
			};
		}
	}
}
