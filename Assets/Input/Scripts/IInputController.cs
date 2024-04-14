using System;
using SurvivalShooter.Inputs;
using UnityEngine;

public interface IInputController
{
    public InputControllerType ControllerType { get; }

    public Vector2 Input { get; }
    public float Horizontal { get; }
    public float HorizontalRaw { get; }
    public float Vertical { get; }
    public float VerticalRaw { get; }
    public Vector2 Direction { get; }

    public Action OnInputStart { get; set; }
    public Action OnInputStop { get; set; }

    public void Register();

    public void Deregister();
}
