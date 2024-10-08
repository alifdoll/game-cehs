using Godot;
using System;

public partial class Main : Control
{
	[Export]
	public Node2D tetroidPlace;
	[Export]
	public int targetCount = 5;	
	public int tetroidCount;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		tetroidCount = 0;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		GD.Print(tetroidCount);
		if(tetroidCount >= targetCount)
		{
			GD.Print("You Win!");
		}
	}
}
