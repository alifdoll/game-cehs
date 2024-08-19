using Godot;
using System;

public partial class CraneHook : Node2D
{
    [Export]
    public RigidBody2D hook;
    [Export]
    public float forceValue = 100;
    private Vector2 force;
    private RigidBody2D crate;
    private PinJoint2D grabber;
    private bool isCrateAttached;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        force = Vector2.Zero;
        isCrateAttached = false;
        grabber = new PinJoint2D();
        AddChild(grabber);
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

        if (Input.IsActionPressed("click"))
        {
            if (isCrateAttached)
            {
                GD.Print("ASD");
                grabber.NodeA = null;
                grabber.NodeB = null;
                isCrateAttached = false;
            }


        }
    }

    private void _on_area_2d_body_entered(Node2D body)
    {
        if (body.IsInGroup("Blocks") && body is RigidBody2D block && !isCrateAttached)
        {
            grabber.NodeA = hook.GetPath();
            grabber.NodeB = block.GetPath();
            grabber.Position = Vector2.Zero;
            grabber.Reparent(block);
            block.GlobalPosition = hook.GlobalPosition;
            block.GravityScale = 1;
            isCrateAttached = true;
        }
    }
}
