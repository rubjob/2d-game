using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class UltimateSkillSlider : MonoBehaviour
{
    public Slider slider;
    public int ulti_points=7;
    public bool usingUlt=false;
    public KeyCode key;
    public KeyCode tmpkey;
    private bool setnewValue=false;


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

    private int numTargets=0;

    public GameObject fill;
    private Image img;
    private Color default_color;

    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue=ulti_points;
        slider.value=0;

        skill_E=skill_E_slider.GetComponent<SkillSlider>();
        skill_Q=skill_Q_slider.GetComponent<SkillSlider>();


        skill_E_cooldown=skill_E.cooldown;
        skill_Q_cooldown=skill_Q.cooldown;

        img = fill.GetComponent<Image>();
        default_color = img.color;
    }

    // Update is called once per frame
    void Update()
    {

        if(!usingUlt){
            if(numTargets>0 && slider.value!=slider.maxValue){
                slider.value+=numTargets;
                numTargets=0;
            } 

            if(Input.GetKeyDown(key) && slider.value==slider.maxValue){
                usingUlt=true;
                skill_E_useCount=skill_E.getUseSkillCount();
                skill_Q_useCount=skill_Q.getUseSkillCount();

            }

        }
        else{
            if(slider.value!=0){
                if(!setnewValue){
                    skill_E.setCooldown(1f);
                    skill_Q.setCooldown(1f);
                    setnewValue=true;
                    img.color=Color.red;
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
                setnewValue=false;
                img.color=default_color;
            }
        }
    }

    public void SetNumTargets(int num){
        numTargets=num;
    }
}
