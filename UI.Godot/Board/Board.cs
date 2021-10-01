using Godot;
using Godot.Collections;
using Quoridor.Game;

namespace Quoridor.Board
{
	public class Board : Node2D
	{
		[Export] private int _boardWidth = 9;
		[Export] private int _boardHeight = 9;
		[Export] private string _tileName = "New Piskel.png 0";
		private int _tileId;

		private GameBuilder _gameBuilder;
		
		private readonly PackedScene _cellScene = ResourceLoader.Load<PackedScene>("res://Cell/Cell.tscn");
		private readonly PackedScene _cornerScene = ResourceLoader.Load<PackedScene>("res://Corner/Corner.tscn");

		private int _offsetX = 1;
		private int _offsetY = 1;
		private Cell[,] _cells;
		private Corner[,] _corners;
		private TileMap _cellsTileMap;

		public override void _Ready()
		{
			_cellsTileMap = GetNode<TileMap>("CellsTileMap");
			_gameBuilder = GetNode<GameBuilder>("/root/GameBuilder");
			
			_tileId = _cellsTileMap.TileSet.FindTileByName(_tileName);
			_cells = new Cell[_boardHeight, _boardHeight];
			_corners = new Corner[_boardHeight-1,_boardHeight-1];
			
			AddCells();
			AddCorners();
		}

		private void AddCells()
		{
			for (int y = _offsetY; y < _boardHeight + _offsetY; y++)
			{
				for (int x = _offsetX; x < _boardWidth + _offsetX; x++)
				{
					var cell = DrawAndReturnCell(x, y);
					
					_cells[x - _offsetX, y - _offsetY] = cell;
					_gameBuilder.ConnectCell(cell);
				}
				
			}
			
			_cellsTileMap.UpdateBitmaskRegion();
			
		}

		private Cell DrawAndReturnCell(int x, int y)
		{
			var cell = _cellScene.Instance() as Cell;
			cell.X = x - _offsetX;
			cell.Y = y - _offsetY;
			cell.Position = new Vector2(x * 16, y * 16);
			
			_cellsTileMap.SetCell(x, y, _tileId);
			AddChild(cell);
			return cell;
		}
		
		private void AddCorners()
		{
			var cornersPerRow = _boardHeight - 1;
			var cornersPerCol = _boardWidth - 1;
			for (int y = 0; y < cornersPerCol; y++)
			{
				for (int x = 0; x < cornersPerRow; x++)
				{
					var corner = DrawAndReturnCorner(x, y);
					
					_corners[x, y] = corner;
					_gameBuilder.ConnectCorner(corner);
				}
			}
		}

		private Corner DrawAndReturnCorner(int x, int y)
		{
			var corner = _cornerScene.Instance() as Corner;
			corner.X = x;
			corner.Y = y;
			corner.Position = _cells[x + 1, y + 1].Position;

			corner.Cells.AddRange(new[]
			{
				_cells[x, y], _cells[x + 1, y], _cells[x, y + 1], _cells[x + 1, y + 1]
			});
			
			AddChild(corner);

			return corner;
		}
	}
}
