using Godot;
using System;

public class Paddle : KinematicBody2D
{
	[Export] public int Speed = 400;

	private Vector2 _viewportSize;
	private Vector2 _size;

	public override void _Ready()
	{
		_viewportSize = GetViewport().Size;
		_size = GetNode<ColorRect>("ColorRect").RectSize;
	}

	public void LerpTo(float from, float y, float dt)
	{
		Position = new Vector2(x: Position.x, y: Mathf.Lerp(from, y, Math.Min(dt, 1f)));
	}

	public void Move(Vector2 velocity)
	{
		Position += velocity * Speed;
	}

	public void Start(Vector2 position)
	{
		Position = new Vector2(position.x - (_size.x / 2), position.y - (_size.y / 2));
	}

	public override void _Process(float delta)
	{
		Position = new Vector2(x: Mathf.Clamp(Position.x, 0, _viewportSize.x),
			y: Mathf.Clamp(Position.y, 0, _viewportSize.y - _size.y));
	}
	public double Height => _size.y;
	public double Width => _size.x;
}
