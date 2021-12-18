using Godot;
using System;
using Pong;

public class Gameplay : Node
{
	private static Random _random = new Random();
	
	private Paddle _player;
	private Ball _ball;
	private Paddle _enemy;
	private HUD _hud;

	private int _playerScore;
	private int _enemyScore;
	
	public override void _Ready()
	{
		_player = GetNode<Paddle>("PlayerPaddle");
		_enemy = GetNode<Paddle>("EnemyPaddle");
		_ball = GetNode<Ball>("Ball");
		_hud = GetNode<HUD>("HUD");

		SetPositionsToStart();
		StartGame();
	}

	public void StartGame()
	{
		_playerScore = 0;
		_enemyScore = 0;

		SetPositionsToStart();
	}

	public override void _Process(float delta)
	{
		var playerVelocity = new Vector2();

		if (Input.IsActionPressed("ui_down"))
		{
			playerVelocity.y += 1;
		}

		if (Input.IsActionPressed("ui_up"))
		{
			playerVelocity.y -= 1;
		}

		if (Input.IsActionPressed("ui_cancel"))
		{
			GetTree().ChangeScene("res://Screens/MainMenu.tscn");
		}

		_player.Move(playerVelocity * delta);
	}
	
	private void _on_Ball_OutOfScreen(Side side)
	{
		if (side == Side.Left)
		{
			_enemyScore++;
		}
		else
		{
			_playerScore++;
		}

		_hud.UpdateScores(_playerScore, _enemyScore);
		SetPositionsToStart();
	}

	private void SetPositionsToStart()
	{
		var startPosition = GetNode<Position2D>("PlayerStartPosition");
		_player.Start(startPosition.Position);

		var ballStartPosition = GetNode<Position2D>("BallStartPosition");
		var pos = new Vector2(ballStartPosition.Position.x + 50, ballStartPosition.Position.y);
		_ball.Start(pos, GetRandomDirection());

		var enemyPosition = GetNode<Position2D>("EnemyStartPosition");
		_enemy.Start(enemyPosition.Position);
	}

	public static Vector2 GetRandomDirection()
	{
		var angle = ToRad(22.5d + _random.Next(0, 44) + _random.NextDouble());
		var dx = (float)(Math.Cos(angle) * (_random.NextDouble() > 0.5 ? 1f : -1f));
		var dy = (float)(Math.Sin(angle) * (_random.NextDouble() > 0.5 ? 1f : -1f));
		return new Vector2(dx, dy);
	}
	
	private static double ToRad(double angle) => Math.PI * angle / 180d;
	
	private void _on_Ball_Bounce()
	{
		var player = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
		player.Play();
	}
}
