using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Focus {
    None,
    Pointer,
    PointerBidirectional,
    Movement,
}

[Serializable]
public class EntityStateBinding
{
    public BindingState StateBinding;
    public BaseEntityState EntityState;
    public InputActionReference InputBinding;
    public bool StopOnActionStarting;
    public Focus Focus;
}
