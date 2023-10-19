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
    /*private bool onCooldown=false;
    private float useSkillat;
    public KeyCode key;*/

    // Start is called before the first frame update
    void Start()
    {
        /*cooldown = ActionManager.SkillStates[SkillBinding].EntityState.CooldownDuration;
        setCooldown(cooldown);*/
    }

    /// Update is called every frame, if the MonoBehaviour is enabled.
    private void Update()
    {
        cooldown = ActionManager.SkillStates[SkillBinding].EntityState.CooldownDuration;
        slider.maxValue = cooldown;

        float time = Mathf.Clamp(ActionManager.SkillCooldowns[SkillBinding] - Time.time, 0, cooldown);
        slider.value = cooldown - time;

        /*if(Input.GetKeyDown(key) && !onCooldown){
            onCooldown=true;
            useSkillat=Time.time;

            slider.value=0;

            useSkillCount+=1;
        }

        if(onCooldown){
            slider.value=Time.time-useSkillat;
            if(Time.time-useSkillat>=cooldown){
                onCooldown=false;
                
            }
            
        }*/
    }

    public void setCooldown(float newCooldown){
        cooldown=newCooldown;
        slider.maxValue=cooldown;
        slider.value=cooldown;
    }

    public int getUseSkillCount(){
        return useSkillCount;
    }
}
