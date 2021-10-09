using System;
using Godot;
using Quoridor.Game;

namespace Quoridor.UI.UI_Buttons
{
	
	
	public abstract class ButtonWallBase : Node2D
	{
		private readonly PackedScene _wallScene = ResourceLoader.Load<PackedScene>(
		 "res://BoardEntities/Wall/InteractiveWall.tscn" );
		private InteractiveWall _draggedWall;
		
		protected float WallRotationDegrees;
		protected bool isHorizontal;
		protected UiPresenterBuilder UiPresenterBuilder;

		public event Action<Action, bool> ButtonWallClicked;

		public override void _Ready()
		{
			UiPresenterBuilder = GetNode<UiPresenterBuilder>("/root/UiPresenterBuilder");
			UiPresenterBuilder.ConnectButton(this);
		}

		protected void OnButtonWallClick()
		{
			ButtonWallClicked?.Invoke(CreateInteractiveWall, isHorizontal);
		}

		public void OnWallPlaced()
		{
			_draggedWall?.QueueFree();
			_draggedWall = null;
		}
		
		private void CreateInteractiveWall()
		{
			_draggedWall = _wallScene.Instance<InteractiveWall>();
			GetViewport().AddChild(_draggedWall);

			_draggedWall.RotationDegrees = WallRotationDegrees;
			_draggedWall.Draggable.IsDragging = true;

			UiPresenterBuilder.ConnectInteractiveWall(_draggedWall);
		}
		
	}
}
