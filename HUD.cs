using Godot;
using System;

public class HUD : CanvasLayer
{
	private Label _playerScoreLabel;
	private Label _enemyScoreLabel;
	
	public override void _Ready()
	{
		_playerScoreLabel = GetNode<Label>("PlayerScore");
		_enemyScoreLabel = GetNode<Label>("EnemyScore");
	}

	public void UpdateScores(int player, int enemy)
	{
		_playerScoreLabel.Text = player.ToString();
		_enemyScoreLabel.Text = enemy.ToString();
	}
}
