using Godot;
using Quoridor.UI.UI_Buttons;

namespace Quoridor.Game
{
	public class UiPresenterBuilder: Node
	{
		private UiPresenter _uiPresenter;

		public override void _Ready()
		{
			_uiPresenter =  GetNode<UiPresenter>("/root/UiPresenter");
		}
		
		public void ConnectCell(Cell cell)
		{
			cell.CellClicked += _uiPresenter.OnCellClicked;
		}

		public void ConnectCorner(Corner corner)
		{
			corner.CornerClicked += _uiPresenter.OnWallPlaced;
			
			_uiPresenter.WallStartDragging += corner.OnWallStartDragging;
			_uiPresenter.WallStopDragging += corner.OnWallStopDragging;
		}

		public void ConnectButton(ButtonWallBase buttonWall)
		{
			buttonWall.ButtonWallClicked += _uiPresenter.OnWallButtonClicked;

			_uiPresenter.WallPlaced += buttonWall.OnWallPlaced;
		}

		public void ConnectInteractiveWall(InteractiveWall draggedWall)
		{
			draggedWall.Clickable.OnMouseClickRight += () =>
			{
				_uiPresenter.OnWallDeleted();
				draggedWall.QueueFree();
			};
		}
	}
}
