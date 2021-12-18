using Godot;
using System;

public class MainMenu : Node
{
	private Timer _startTimer;
	private AutoPilotBackground _autoPilot;

	[Export] public float BlurAmount = 2.5f;
	
	public override void _Ready()
	{
		_startTimer = GetNode<Timer>("StartTimer");
		_autoPilot = GetNode<AutoPilotBackground>("AutoPilotBackground");
		
		var blurLayer = GetNode<ColorRect>("BlurLayer");
		blurLayer.Material.Set("shader_param/blur_amount", BlurAmount);
	}

	public override void _Process(float delta)
	{
		if (Input.IsActionJustPressed("ui_accept"))
		{
			_startTimer.Start();
			_autoPilot.LerpToStart();

			var blurLayer = GetNode<ColorRect>("BlurLayer");
			var tween = GetNode<Tween>("BlurLayer/Tween");
			tween.InterpolateProperty(blurLayer.Material, "shader_param/blur_amount", 
				BlurAmount, 0f, 1, 
				Tween.TransitionType.Linear, Tween.EaseType.Out);
			tween.Start();
		}
	}
	
	private void _on_Timer_timeout()
	{
		GetTree().ChangeScene("res://Screens/Gameplay.tscn");
	}

}
