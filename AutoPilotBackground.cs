using Godot;
using System;
using Pong;

public class AutoPilotBackground : Node
{
	private Paddle _cpu1;
	private Paddle _cpu2;
	private Ball _ball;
	
	public override void _Ready()
	{
		_cpu1 = GetNode<Paddle>("Cpu1");
		_cpu2 = GetNode<Paddle>("Cpu2");
		_ball = GetNode<Ball>("Ball");

		ResetPositions();
	}

	public void LerpToStart()
	{
		var height = _cpu1.Height;
		var width = _cpu1.Width;
		var position1 = GetNode<Position2D>("Cpu1Position").Position;
		position1 = new Vector2((float)(position1.x - (width / 2)), (float)(position1.y - (height / 2)));
		var cpu1 = GetNode<CpuEnemy>("Cpu1/CpuEnemy");
		cpu1.Frozen = true;
		var tween1 = GetNode<Tween>("Cpu1/Tween");
		tween1.InterpolateProperty(_cpu1, "position", _cpu1.Position, position1, 1,
			Tween.TransitionType.Linear, Tween.EaseType.Out);
		tween1.Start();
		
		var position2 = GetNode<Position2D>("Cpu2Position").Position;
		position2 = new Vector2((float)(position2.x - (width / 2)), (float)(position2.y - (height / 2)));
		var cpu2 = GetNode<CpuEnemy>("Cpu2/CpuEnemy");
		cpu2.Frozen = true;
		var tween2 = GetNode<Tween>("Cpu1/Tween");
		tween2.InterpolateProperty(_cpu2, "position", _cpu2.Position, position2, 1,
			Tween.TransitionType.Linear, Tween.EaseType.Out);
		tween2.Start();

		var ballPosition = GetNode<Position2D>("BallPosition").Position;
		var tween = GetNode<Tween>("Ball/Tween");
		_ball.Frozen = true;
		tween.InterpolateProperty(_ball, "position", _ball.Position, ballPosition, 1,
			Tween.TransitionType.Linear, Tween.EaseType.Out);
		tween.Start();
	}
	
	private void _on_Ball_OutOfScreen(Side side)
	{
		// Replace with function body.
		ResetPositions();
	}

	private void ResetPositions()
	{
		var startPosition = GetNode<Position2D>("Cpu1Position");
		_cpu1.Start(startPosition.Position);

		var ballStartPosition = GetNode<Position2D>("BallPosition");
		_ball.Start(ballStartPosition.Position, Gameplay.GetRandomDirection());

		var enemyPosition = GetNode<Position2D>("Cpu2Position");
		_cpu2.Start(enemyPosition.Position);
	}
}
