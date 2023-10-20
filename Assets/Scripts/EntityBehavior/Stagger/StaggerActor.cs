using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StaggerActor : MonoBehaviour
{
    [Header("Constant")]
    public int MaxStaggerLevel;

    private int Level;

    [Header("Events")]
    public UnityEvent OnStagger;

    public void AddStaggerLevel(int level) {
        Level += level;

        if (Level >= MaxStaggerLevel) {
            Level = 0;
            OnStagger?.Invoke();
        }
    }
}
