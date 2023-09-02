using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxManager : MonoBehaviour {
    [SerializeField] private HitboxTriggerer trigger;
    public HitboxTriggerer Trigger {
        get { return trigger; }
    }

    public void RotateTo(float angle) {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
