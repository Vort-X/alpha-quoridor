using Godot;
using Quoridor.Game;

namespace Quoridor.UI.UI_Buttons
{
    
    
    public abstract class ButtonWallBase : Node2D
    {
        private readonly PackedScene _wallScene = ResourceLoader.Load<PackedScene>(
            "res://Wall/InteractiveWall.tscn");

        private InteractiveWall _draggedWall;
        
        protected float WallRotationDegrees;
        protected Quoridor.Game.Game _game;

        public override void _Ready()
        {
            _game = GetNode<Quoridor.Game.Game>("/root/Game");
            _game.WallPlaced += () =>
            {
                _draggedWall?.QueueFree();
                _draggedWall = null;
            };
        }

        protected void ButtonWallOnStartDrag()
        {
            _game.OnWallButtonClicked(CreateInteractiveWall);
        }

        private void CreateInteractiveWall()
        {
            _draggedWall = _wallScene.Instance<InteractiveWall>();
            GetViewport().AddChild(_draggedWall);

            _draggedWall.RotationDegrees = WallRotationDegrees;
            _draggedWall.Draggable.IsDragging = true;
            _draggedWall.Clickable.OnMouseClickRight += () =>
            {
                _game.OnWallDeleted();
                _draggedWall.QueueFree();
                _draggedWall = null;
            };
        }
        
    }
}