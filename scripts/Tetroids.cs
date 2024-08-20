using Godot;
using System;

public partial class Tetroids : RigidBody2D
{
	public bool isSelected = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Transform2D transform2d = new Transform2D();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
