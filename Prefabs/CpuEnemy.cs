using Godot;
using System;
using Pong;

public class CpuEnemy : Node
{
	[Export] public Side Side;
	[Export] public NodePath BallNode;

	private Ball _ball;
	private Paddle _paddle;

	public override void _Ready()
	{
		_paddle = GetParent<Paddle>();
		_ball = GetNode<Ball>(BallNode);
	}
	
	public bool Frozen { get; set; }

	public override void _Process(float delta)
	{
		if (Frozen)
		{
			return;
		}
		
		switch (Side)
		{
			case Side.Left when _ball.GetVelocityX() > 0f:
				return;
			case Side.Right when _ball.GetVelocityX() < 0f:
				return;
		}

		var paddleCenter = _paddle.Position.y + _paddle.Height / 2;
		var ballCenter = _ball.Position.y + _ball.Height / 2;
		if (ballCenter > paddleCenter)
		{
			_paddle.Move(Vector2.Down * delta);
		}
		else if (ballCenter < paddleCenter)
		{
			_paddle.Move(Vector2.Up * delta);
		}
	}
}
