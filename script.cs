using Godot;
using System;
using System.Collections.Generic;
using System.IO;

public partial class script : TileMap
{

  

    PackedScene crate_obj = ResourceLoader.Load<PackedScene>("res://crate.tscn");
    PackedScene crate;
 




    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public override void _Input(InputEvent @event)
    {

        var current_position = GetGlobalMousePosition();
        if (Input.IsActionJustPressed("click"))
        {
            crate = GD.Load<PackedScene>("res://crate.tscn");
            var obj = crate.Instantiate<Node2D>();
            obj.Position = current_position;
            AddChild(obj);
        }
    }

   
}
