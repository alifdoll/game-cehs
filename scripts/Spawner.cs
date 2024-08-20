using Godot;
using Godot.Collections;
using System;
using System.Linq;

public partial class Spawner : Node2D
{
	[Export]
	public Area2D areaSpawner;
	[Export]
	public CraneHook craneHook;
	[Export]
	public Spawner otherSpawner;
	private PackedScene[] tetroidsLoader;
	private const string PATH = "components";
	private Random randomizer = new Random();
	private Tetroids randomTetroid;
	private PackedScene tetroidLoader;
	private int[] randomRotation = { 0, 90, 180, 270 };

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		DirAccess directoryAccessor = DirAccess.Open(PATH);
		string[] fileNames = directoryAccessor.GetFiles();
		LoadTetroids(fileNames);
		RandomTentroidGenerator(false);
		craneHook.TetroidDropped += ActivateGravity;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void LoadTetroids(string[] fileNamesInput)
	{
		tetroidsLoader = new PackedScene[fileNamesInput.Length];

		for (int i = 0; i < fileNamesInput.Length; i++)
		{
			string filePath = $"{PATH}/{fileNamesInput[i]}";
			tetroidsLoader[i] = GD.Load<PackedScene>(filePath);
		}
	}

	public void RandomTentroidGenerator(bool isDeffered)
	{
		tetroidLoader = tetroidsLoader[randomizer.Next(0, tetroidsLoader.Length)];
		randomTetroid = tetroidLoader.Instantiate<Tetroids>();
		randomTetroid.GravityScale = 0;
		if (isDeffered)
		{
			CallDeferred("add_child", randomTetroid);
		}
		else
		{
			AddChild(randomTetroid);
		}
		randomTetroid.Position = areaSpawner.Position;
		randomTetroid.RotationDegrees = randomRotation[randomizer.Next(0, randomRotation.Length)];
	}

	private void ActivateGravity()
	{
		randomTetroid.GravityScale = 1;
	}

	private void _on_area_despawner_body_exited(Node2D body)
	{
		if (body.IsInGroup("Blocks") && body is Tetroids tetroid)
		{
			if (tetroid.isSelected == true)
			{
				GD.Print("Game Over");
			}
			else
			{
				tetroid.QueueFree();
				RandomTentroidGenerator(true);
				otherSpawner.RandomTentroidGenerator(true);
			}
		}
	}
}
