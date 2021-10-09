using System;
using Godot;

namespace Quoridor.Game
{
	public class UiPresenter : Node
	{
		public InputStates InputStates { get; private set; }

		public event Action WallPlaced;
		public event Action CellClicked;
		public event Action WallStartDragging;
		public event Action WallStopDragging;
			
		public override void _Ready()
		{
			InputStates = InputStates.NoInputRequested;

			WallPlaced += () => WallStopDragging?.Invoke();
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
			WallPlaced?.Invoke();
			InputStates = InputStates.NoInputRequested;
		}

		public void OnCellClicked(Tuple<int, int> cellCoordinates)
		{
			if (InputStates != InputStates.SelectingCell) 
				return;
			
			var (x, y) = cellCoordinates;
			GD.Print($"Cell: X:{x}, Y:{y}");
			CellClicked?.Invoke();
			InputStates = InputStates.NoInputRequested;
		}

		public void OnWallButtonClicked(Action wallCreation)
		{
			if (InputStates != InputStates.SelectingCell) 
				return;
			
			wallCreation.Invoke();
			WallStartDragging?.Invoke();
			
			InputStates = InputStates.DraggingWall;
		}

		public void OnWallDeleted()
		{
			WallStopDragging?.Invoke();
			InputStates = InputStates.SelectingCell;
		}
	}
}
