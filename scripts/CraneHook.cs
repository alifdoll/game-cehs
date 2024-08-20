using Godot;
using System;

public partial class CraneHook : Node2D
{
	[Export]
	public RigidBody2D hook;
	[Export]
	public float forceValue = 100;
	[Export]
	public Main main;
	private Tetroids tetroid;
	private bool isCrateAttached;
	[Signal]
	public delegate void TetroidDroppedEventHandler();
	private PinJoint2D grabber;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		isCrateAttached = false;
		grabber = new PinJoint2D();
		AddChild(grabber);
		grabber.Position = Vector2.Zero;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public override void _PhysicsProcess(double delta)
	{
	}

	public override void _Input(InputEvent @event)
	{
		if (Input.IsActionPressed("drop") && isCrateAttached)
		{
			grabber.NodeB = null;
			isCrateAttached = false;
			EmitSignal(SignalName.TetroidDropped);
			tetroid.isSelected = true;
			main.tetroidCount++;
		}
	}

	private void _on_area_2d_body_entered(Node2D body)
	{
		if (body.IsInGroup("Blocks") && body is Tetroids grabbedTetroid && !isCrateAttached)
		{
			tetroid = grabbedTetroid;
			grabber.CallDeferred("reparent", tetroid);
			grabber.Position = Vector2.Zero;
			grabber.NodeA = tetroid.GetPath();
			grabber.NodeB = hook.GetPath();
			tetroid. GlobalPosition = hook.GlobalPosition;
			tetroid.GravityScale = 1;
			isCrateAttached = true;
		}
	}
}
