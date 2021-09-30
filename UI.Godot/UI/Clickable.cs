using Godot;

namespace Quoridor.UI
{
	public delegate void MouseClickEvent();
	
	public class Clickable : Area2D
	{
		public event MouseClickEvent OnMouseClickLeft;
		public event MouseClickEvent OnMouseClickRight;
		public event MouseClickEvent OnStartDrag;
		public event MouseClickEvent OnEndDrag;
		
		public override void _Ready()
		{
		
		}

		private void _on_Clickable_input_event(object viewport, object @event, int shape_idx)
		{
			if (!(@event is InputEventMouseButton mouseEvent)) return;
			
			var mouseButton = (ButtonList) mouseEvent.ButtonIndex;

			switch (mouseButton)
			{
				case ButtonList.Left when mouseEvent.Pressed:
					OnStartDrag?.Invoke();
					break;
				case ButtonList.Left:
					OnEndDrag?.Invoke();
					OnMouseClickLeft?.Invoke();
					break;                                                                                                                                                
				case ButtonList.Right when mouseEvent.Pressed:
					OnMouseClickRight?.Invoke();
					break;
			}
		}
	}
}



