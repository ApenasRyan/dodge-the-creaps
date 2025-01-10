using Godot;
using System;

public partial class Main : Node
{
	[Export]
	public PackedScene EnemyScene { get; set; }

	private int _score;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		NewGame();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void GameOver()
	{
		GetNode<Timer>("EnemyMobTimer").Stop();
		GetNode<Timer>("ScoreTimer").Stop();
	}

	public void NewGame()
	{
		_score = 0;

		var player = GetNode<Player>("Player");
		var startPosition = GetNode<Marker2D>("StartPosition");
		player.Start(startPosition.Position);

		GetNode<Timer>("StartTimer").Start();
	}

	// We also specified this function name in PascalCase in the editor's connection window.
private void OnEnemyTimerTimeout()
{
    // Note: Normally it is best to use explicit types rather than the `var`
    // keyword. However, var is acceptable to use here because the types are
    // obviously Mob and PathFollow2D, since they appear later on the line.

    // Create a new instance of the Mob scene.
    Enemy enemy = EnemyScene.Instantiate<Enemy>();

    // Choose a random location on Path2D.
    var enemySpawnLocation = GetNode<PathFollow2D>("EnemyPath/EnemySpawnLocation");
    enemySpawnLocation.ProgressRatio = GD.Randf();

    // Set the enemy's direction perpendicular to the path direction.
    float direction = enemySpawnLocation.Rotation + Mathf.Pi / 2;

    // Set the enemy's position to a random location.
    enemy.Position = enemySpawnLocation.Position;

    // Add some randomness to the direction.
    direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
    enemy.Rotation = direction;

    // Choose the velocity.
    var velocity = new Vector2((float)GD.RandRange(150.0, 250.0), 0);
    enemy.LinearVelocity = velocity.Rotated(direction);

    // Spawn the enemy by adding it to the Main scene.
    AddChild(enemy);
}
}
