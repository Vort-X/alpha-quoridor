using System;
using Godot;

namespace Quoridor.Game
{
	public class UiPresenter : Node
	{
	
		public GameStates GameStates { get; private set; }
		public InputStates InputStates { get; private set; }

		public event Action WallPlaced;
		public event Action WallStartDragging;
		public event Action WallStopDragging;
			
		public override void _Ready()
		{
			GameStates = GameStates.Waiting;
			InputStates = InputStates.NoInputRequested;

			WallPlaced += () => WallStopDragging?.Invoke();
		}

	
		public override void _Process(float delta)
		{
			if (Input.IsActionJustPressed("input_request"))
			{
				GD.Print("input_request");
				GameStates = GameStates switch
				{
					GameStates.Waiting => GameStates.InputRequested,
					GameStates.InputRequested => GameStates.Waiting
				};
				InputStates = GameStates switch
				{
					GameStates.Waiting => InputStates.NoInputRequested,
					GameStates.InputRequested => InputStates.SelectingCell
				};
			}
		}

		public void OnWallPlaced(Tuple<int, int> cornerCoordinates)
		{
			if (InputStates != InputStates.DraggingWall)
				return;
			
			var (x, y) = cornerCoordinates;
			GD.Print($"Corner: X:{x}, Y:{y}");
			WallPlaced?.Invoke();
			InputStates = InputStates.SelectingCell;
		}

		public void OnCellClicked(Tuple<int, int> cellCoordinates)
		{
			if (InputStates != InputStates.SelectingCell) 
				return;
			
			var (x, y) = cellCoordinates;
			GD.Print($"Cell: X:{x}, Y:{y}");
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
