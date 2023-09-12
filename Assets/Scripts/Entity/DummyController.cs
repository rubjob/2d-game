using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : BaseEntity
{
    private void Start()
    {
        Setup();
    }

    protected override void OnPerformingAction() { }

}
