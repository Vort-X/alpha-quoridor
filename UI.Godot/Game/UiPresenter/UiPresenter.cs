using System;
using System.Collections.Generic;
using Godot;

namespace Quoridor.Game
{
	public class UiPresenter : Node
	{
		private InputStates InputStates { get;  set; }
		private bool isWallHorizontal;
		private List<Tuple<int, int>> _highlightedCells;

		public Action<Tuple<int, int>, bool> WallPlaced;
		public Action<Tuple<int, int>> CellClicked;
		public Action WallStartDragging;
		public Action WallStopDragging;
		public Action<List<Tuple<int, int>>> AddHighlightCells; 
		public Action<List<Tuple<int, int>>> RemoveHighlightCells; 

		public override void _Ready()
		{
			InputStates = InputStates.NoInputRequested;
		}

		public void OnInputRequested(List<Tuple<int, int>> highlightedCells)
		{
			InputStates = InputStates.SelectingCell;
			AddHighlightCells?.Invoke(highlightedCells);
			_highlightedCells = highlightedCells;
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
			RemoveHighlightCells?.Invoke(_highlightedCells);
			
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
			RemoveHighlightCells?.Invoke(_highlightedCells);
		}

		public void OnWallDeleted()
		{
			WallStopDragging?.Invoke();
			InputStates = InputStates.SelectingCell;
			AddHighlightCells?.Invoke(_highlightedCells);
		}

		public void UnsubscribeFromAll()
		{
			foreach (Delegate d in WallPlaced.GetInvocationList())
			{
				WallPlaced -= (Action<Tuple<int, int>, bool>) d;
			}
			foreach (Delegate d in CellClicked.GetInvocationList())
			{
				CellClicked -= (Action<Tuple<int, int>>) d;
			}
			foreach (Delegate d in WallStartDragging.GetInvocationList())
			{
				WallStartDragging -= (Action) d;
			}
			foreach (Delegate d in WallStopDragging.GetInvocationList())
			{
				WallStopDragging -= (Action) d;
			}
			foreach (Delegate d in AddHighlightCells.GetInvocationList())
			{
				AddHighlightCells -= (Action<List<Tuple<int, int>>>) d;
			}
			foreach (Delegate d in RemoveHighlightCells.GetInvocationList())
			{
				RemoveHighlightCells -= (Action<List<Tuple<int, int>>>) d;
			}
			InputStates = InputStates.NoInputRequested;
		}
	}
}
