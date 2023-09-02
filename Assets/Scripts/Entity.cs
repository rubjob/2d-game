using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public float maxHealth = 100f;

    private float health;
    public float Health {
        get { return health; }
        set { health = value; Debug.Log(health); }
    }

    private void Start() {
        health = maxHealth;
    }
}
