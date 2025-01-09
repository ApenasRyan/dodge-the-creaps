using Godot;
using Vector2 = Godot.Vector2;

public partial class Player : Area2D
{
	[Export]
	public int Speed { get; set; } = 400; //player speed (pixels/sec).
	
	public Vector2 ScreenSize; //size of the game windows.

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var velocity = Godot.Vector2.Zero;

		if (Input.IsActionPressed("move_right")) velocity.X += 1;

		if (Input.IsActionPressed("move_left")) velocity.X -= 1;

		if (Input.IsActionPressed("move_down")) velocity.Y += 1;

		if (Input.IsActionPressed("move_up")) velocity.Y -= 1;

		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

		if (velocity.Length() > 0)
		{
			velocity = velocity.Normalized() * Speed;
			animatedSprite2D.Play();
		}
		else
		{
			animatedSprite2D.Stop();
		}

		Position += velocity * (float)delta;
		Position = new Vector2( x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
								y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y));

		if (velocity.X != 0)
		{
			animatedSprite2D.Animation = "walk";
			animatedSprite2D.FlipV = false;
			animatedSprite2D.FlipH = velocity.X < 0;
		}
		else
		{
			animatedSprite2D.Animation = "up";
			animatedSprite2D.FlipV = velocity.Y > 0;
		}

		if (velocity.X < 0)
		{
			animatedSprite2D.FlipH = true;
		}
		else
		{
			animatedSprite2D.FlipH = false;
		}

		Hide();
	}
}
