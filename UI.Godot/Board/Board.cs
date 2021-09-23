using Godot;
using System;

	public class Board : Node2D
	{
		[Export] private int _boardWidth = 9;
		[Export] private int _boardHeight = 9;
		[Export] private string _tileName = "New Piskel.png 0";

		private TileMap _cellsTileMap;

		private int _offsetX = 1;
		private int _offsetY = 1;
		private Cell[,] _cells;
	
		public override void _Ready()
		{
			_cellsTileMap = GetNode<TileMap>("CellsTileMap");
			
			_cells = new Cell[_boardHeight, _boardHeight];
			DrawTiles();

		}

		private void DrawTiles()
		{
			var tileId = _cellsTileMap.TileSet.FindTileByName(_tileName);
			var cellScene = ResourceLoader.Load<PackedScene>("res://Cell/Cell.tscn");
			
			
			for (int y = _offsetY; y < _boardHeight + _offsetY; y++)
			{
				for (int x = _offsetX; x < _boardWidth + _offsetX; x++)
				{
					_cellsTileMap.SetCell(x, y, tileId);
					var cell =  cellScene.Instance() as Cell;
					cell.X = x - _offsetX;
					cell.Y = y - _offsetY;
					cell.Position = new Vector2(x*16, y * 16);

					_cells[x-_offsetX, y -_offsetY] = cell;
					AddChild(cell);
				}
				
			}
			
			_cellsTileMap.UpdateBitmaskRegion();
			
		}
	}
