using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debugger : MonoBehaviour
{

    public float amount;
    public PlayerController Player;

    public HealthBar healthBar;

    public void decreasePlayerHealth() {
        // Player.takeDamage(amount);
        Player.takeDamage(amount);
    }

    public void killPlayer(){
        Player.takeDamage(Player.maxHealth);
    }

    // public void healPlayer(){
    //     Player.OnHeal(amount);
    // }

    public void decreaseHealthbar(){
        healthBar.decreaseHealth(amount);
    }

}
