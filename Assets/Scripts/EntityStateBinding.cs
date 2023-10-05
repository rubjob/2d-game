using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class EntityStateBinding
{
    public BindingState StateBinding;
    public BaseEntityState EntityState;
    public string AnimationTriggerName;
    public InputActionReference InputBinding;
}
