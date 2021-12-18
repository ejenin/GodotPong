using Godot;
using System;

public class PressAnyKey : Label
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var tween = GetNode<Tween>("Tween");
		tween.InterpolateProperty(this, "margin_top", 0, 10, 2,
			Tween.TransitionType.Linear, Tween.EaseType.Out);
		tween.InterpolateProperty(this, "margin_top", 10, 0, 1,
			Tween.TransitionType.Linear, Tween.EaseType.In);
		tween.Start();
	}
}
