using System;
using Godot;

namespace Quoridor.Game
{
	public class Game : Node
	{
	
		public GameState GameState { get; private set; }
		public InputState InputState { get; private set; }

		public event Action WallPlaced;
			
		public override void _Ready()
		{
			GameState = GameState.Waiting;
			InputState = InputState.NoInputRequested;
		}

	
		public override void _Process(float delta)
		{
			if (Input.IsActionJustPressed("input_request"))
			{
				GD.Print("input_request");
				GameState = GameState switch
				{
					GameState.Waiting => GameState.InputRequested,
					GameState.InputRequested => GameState.Waiting
				};
				InputState = GameState switch
				{
					GameState.Waiting => InputState.NoInputRequested,
					GameState.InputRequested => InputState.SelectingCell
				};
			}
		}

		public void OnWallPlaced(Tuple<int, int> cornerCoordinates)
		{
			if (InputState != InputState.DraggingWall)
				return;
			
			var (x, y) = cornerCoordinates;
			GD.Print($"Corner: X:{x}, Y:{y}");
			WallPlaced?.Invoke();
			InputState = InputState.SelectingCell;
		}

		public void OnCellClicked(Tuple<int, int> cellCoordinates)
		{
			if (InputState != InputState.SelectingCell) 
				return;
			
			var (x, y) = cellCoordinates;
			GD.Print($"Cell: X:{x}, Y:{y}");
		}

		public void OnWallButtonClicked(Action wallCreation)
		{
			if (InputState != InputState.SelectingCell) 
				return;
			
			wallCreation.Invoke();
			InputState = InputState.DraggingWall;
		}

		public void OnWallDeleted()
		{
			InputState = InputState.SelectingCell;
		}
	}
}
