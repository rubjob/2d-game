using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debugger : MonoBehaviour
{

    public float amount;
    public BaseEntity Player;

    public HealthBar healthBar;

    public void decreasePlayerHealth() {
        // Player.takeDamage(amount);
        Player.takeDamage(amount);
    }

    public void healPlayer(){
        Player.heal(amount);
    }

    public void decreaseHealthbar(){
        healthBar.decreaseHealth(amount);
    }

}
