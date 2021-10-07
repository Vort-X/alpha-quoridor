using Godot;
using System;

public class PixelLabel : Label
{

	DynamicFontData _fontData = ResourceLoader.Load<DynamicFontData>("res://UI/Main Menu/press-start/prstartk.ttf");
	public override void _Ready()
	{
		
	}

	private void _on_PixelMenuItemLabel_mouse_entered()
	{
		var newFont = CreateFontWithOutlineSize(2);
		AddFontOverride("font", newFont);
	}

	private DynamicFont CreateFontWithOutlineSize(int outlineSize)
	{
		var newFont = new DynamicFont();
		newFont.FontData = _fontData;
		newFont.Size = 10;
		newFont.OutlineSize = outlineSize;
		newFont.OutlineColor = Colors.Black;
		return newFont;
	}

	private void _on_PixelMenuItemLabel_mouse_exited()
	{
		var newFont = CreateFontWithOutlineSize(1);
		AddFontOverride("font", newFont);
	}
}






