using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class UltimateSkillSlider : MonoBehaviour
{
    [Header("Depedency")]
    public Player.Ultimate UltimateState;
    public Slider slider;
    public Image img;

    private Color defaultColor;

    void Start()
    {
        defaultColor = img.color;
        slider.maxValue = UltimateState.MaxUltimatePoint;
    }

    void Update()
    {
        img.color = (UltimateState.IsUsingUltimate) ? Color.red : defaultColor;

        slider.value = UltimateState.CurrentUltimatePoint;
    }
}
