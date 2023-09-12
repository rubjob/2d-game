using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public TextMeshProUGUI textHealth;

    public void setMaxHealth(float maxHealth){
        slider.maxValue=maxHealth;
        slider.value=maxHealth;

        fill.color=gradient.Evaluate(1f);
        updateText();
    }

    // public void decreaseHealth(float amount){
    //     slider.value-=amount;
    //     fill.color=gradient.Evaluate(slider.normalizedValue);
    //     updateText();
    // }

    // public void increaseHealth(float amount){
    //     slider.value+=amount;
    //     fill.color=gradient.Evaluate(slider.normalizedValue);
    //     updateText();
    // }

    public void setHealth(float health){
        slider.value=health;
        fill.color=gradient.Evaluate(slider.normalizedValue);
        updateText();
    }

    public void updateText(){
        textHealth.text=slider.value.ToString()+"/"+slider.maxValue.ToString();
    }

  

     
}
