using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoDamage : MonoBehaviour
{

    public float amount;
    public PlayerController Player;

    public HealthBar healthBar;

    public void onClick(){
        Debug.Log(amount);
        Player.takeDamage(amount);
    }

    public void decreseHealthbar(){
        healthBar.decreseHealth(amount);
    }



}
