using System;
using Godot;

namespace Quoridor.Game
{
	public class UiPresenter : Node
	{
		private InputStates InputStates { get;  set; }
		private bool isWallHorizontal;

		public event Action<Tuple<int, int>, bool> WallPlaced;
		public event Action<Tuple<int, int>> CellClicked;
		public event Action WallStartDragging;
		public event Action WallStopDragging;
			
		public override void _Ready()
		{
			InputStates = InputStates.NoInputRequested;
		}

		public void OnInputRequested()
		{
			InputStates = InputStates.SelectingCell;
		}
		

		public void OnWallPlaced(Tuple<int, int> cornerCoordinates)
		{
			if (InputStates != InputStates.DraggingWall)
				return;
			
			var (x, y) = cornerCoordinates;
			GD.Print($"Corner: X:{x}, Y:{y}");
			
			WallStopDragging?.Invoke();
			WallPlaced?.Invoke(cornerCoordinates, isWallHorizontal);
			
			InputStates = InputStates.NoInputRequested;
		}

		public void OnCellClicked(Tuple<int, int> cellCoordinates)
		{
			if (InputStates != InputStates.SelectingCell) 
				return;
			
			var (x, y) = cellCoordinates;
			GD.Print($"Cell: X:{x}, Y:{y}");
			
			CellClicked?.Invoke(cellCoordinates);
			
			InputStates = InputStates.NoInputRequested;
		}

		public void OnWallButtonClicked(Action wallCreation, bool isHorizontal)
		{
			if (InputStates != InputStates.SelectingCell) 
				return;
			
			wallCreation.Invoke();
			WallStartDragging?.Invoke();

			isWallHorizontal = isHorizontal;
			InputStates = InputStates.DraggingWall;
		}

		public void OnWallDeleted()
		{
			WallStopDragging?.Invoke();
			InputStates = InputStates.SelectingCell;
		}
	}
}
