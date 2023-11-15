using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SkillSlider : MonoBehaviour
{
    [Header("Dependency")]
    public ActionManager ActionManager;
    public BindingState SkillBinding;
    public Slider slider;
    public float cooldown;
    public int ulti_points;
    private int useSkillCount = 0;
    private void Update()
    {
        
        setCooldown(ActionManager.GetSkillCooldown(SkillBinding));
        float time = Mathf.Clamp(ActionManager.SkillCooldowns[SkillBinding] - Time.time, 0, cooldown);
        slider.value = cooldown - time;
        Debug.Log(slider.value);
    }

    public void setCooldown(float newCooldown){
        cooldown=newCooldown;
        slider.maxValue=cooldown;
        slider.value=cooldown;
    }
}
