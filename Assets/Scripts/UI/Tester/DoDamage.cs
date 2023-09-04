using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoDamage : MonoBehaviour
{

    public float amount;
    public PlayerController Player;

    public HealthBar healthBar;

    public void decreasePlayerHealth(){
        // Player.takeDamage(amount);
        Player.SpecialtakeDamage(amount);
    }

    public void decreaseHealthbar(){
        healthBar.decreaseHealth(amount);
    }



}
