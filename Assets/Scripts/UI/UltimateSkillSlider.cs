using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UltimateSkillSlider : MonoBehaviour
{
    public Slider slider;
    public int ulti_points=7;
    public bool usingUlt=false;
    public KeyCode key;
    public KeyCode tmpkey;
    private bool setnewCooldown=false;


    [Header("Skill Q")]
    private SkillSlider skill_Q;
    public Slider skill_Q_slider;
    private float skill_Q_cooldown;
    private int skill_Q_useCount;
    [Header("Skill E")]
    private SkillSlider skill_E;
    public Slider skill_E_slider;
    private float skill_E_cooldown;
    private int skill_E_useCount;


    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue=ulti_points;
        slider.value=0;

        skill_E=skill_E_slider.GetComponent<SkillSlider>();
        skill_Q=skill_Q_slider.GetComponent<SkillSlider>();


        skill_E_cooldown=skill_E.cooldown;
        skill_Q_cooldown=skill_Q.cooldown;
        Debug.Log(skill_E_cooldown+" "+skill_Q_cooldown);
    }

    // Update is called once per frame
    void Update()
    {

        if(!usingUlt){
            //! Change when click tmpkey to when hit enemy in the future
            if(Input.GetKeyDown(tmpkey) && slider.value!=slider.maxValue){
                slider.value+=1;
            } 

            if(Input.GetKeyDown(key) && slider.value==slider.maxValue){
                usingUlt=true;
                skill_E_useCount=skill_E.getUseSkillCount();
                skill_Q_useCount=skill_Q.getUseSkillCount();
                Debug.Log("E :- "+skill_E_useCount);
                Debug.Log("Q :- "+skill_Q_useCount);

            }

        }
        else{
            if(slider.value!=0){
                if(!setnewCooldown){
                    skill_E.setCooldown(1f);
                    skill_Q.setCooldown(1f);
                    setnewCooldown=true;
                    Debug.Log("set new");
                }

                if(skill_E.getUseSkillCount()!=skill_E_useCount){
       
                    slider.value=slider.value-(skill_E.ulti_points)*(skill_E.getUseSkillCount()-skill_E_useCount);
                    skill_E_useCount=skill_E.getUseSkillCount();
                    if(slider.value<0) slider.value=0; 
                }
                if(skill_Q.getUseSkillCount()!=skill_Q_useCount){
              

                    slider.value=slider.value-(skill_Q.ulti_points)*(skill_Q.getUseSkillCount()-skill_Q_useCount);

                    skill_Q_useCount=skill_Q.getUseSkillCount();
                    if(slider.value<0) slider.value=0; 
                }
            }
            else{
                skill_E.setCooldown(skill_E_cooldown);
                skill_Q.setCooldown(skill_Q_cooldown);

                usingUlt=false;
                setnewCooldown=false;
            }
        }


        
    }
}
