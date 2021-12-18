using Godot;
using System;
using Pong;

public class Ball : KinematicBody2D
{
	[Export] public int Speed = 400;
	[Export] public int BounceThreshold = 5;
	
	[Signal]
	public delegate void OutOfScreen(Side side);

	[Signal]
	public delegate void Bounce();

	private Vector2 _viewportSize;
	private Vector2 _size;

	public Vector2 _velocity;

	private int _bounceCounter;
	
	public bool Frozen { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_viewportSize = GetViewport().Size;
		_size = GetNode<ColorRect>("ColorRect").RectSize;
	}
	
	private void _on_VisibilityNotifier2D_screen_exited()
	{
		var side = Position.x > (_viewportSize.x / 2) ? Side.Right : Side.Left;
		EmitSignal("OutOfScreen", side);
	}

	public override void _PhysicsProcess(float delta)
	{
		if (Frozen)
		{
			return;
		}
		
		if ((Position.y - _size.y / 2) <= 0 || (Position.y + _size.y / 2) >= _viewportSize.y)
		{
			BounceY();
		}
		var velocity = _velocity * Speed * delta;
		var collisionInfo = MoveAndCollide(velocity);
		
		if (collisionInfo != null)
		{
			BounceX();
		}
	}

	public double Height => _size.y;
	
	public void Start(Vector2 position, Vector2 velocity)
	{
		Position = new Vector2(position.x - (_size.x / 2), position.y - (_size.y / 2));
		_velocity = velocity;
		_bounceCounter = 0;
	}

	public float GetVelocityX() => _velocity.x;

	private void BounceX()
	{
		EmitSignal("Bounce");
		_velocity.x *= -1;
		IncreaseBounce();
	}
	
	private void BounceY()
	{
		EmitSignal("Bounce");
		_velocity.y *= -1;
		IncreaseBounce();
	}

	private void IncreaseBounce()
	{
		_bounceCounter++;
		if (_bounceCounter > BounceThreshold)
		{
			_velocity *= 1.15f;
		}
	}
}
