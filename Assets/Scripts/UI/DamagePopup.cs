using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    private TextMeshPro textmesh;
    private const float MAX_DISAPPEAR_TIMER = 1f;
    private float disappearTimer;
    private Color textColor;
    private float disappearSpeed=3f;
    private Color normal;
    private Color heavy;
    private Vector3 moveVector;



    //Create a DamagePopup
    public static DamagePopup Create(Vector3 position,float dmgAmount, bool isCritHit){
        position.x+=1;

        Transform dmgPopupTransform = Instantiate(GameAssets.instance.prefabDmgPopup,position,Quaternion.identity);

        DamagePopup dmgPopup = dmgPopupTransform.GetComponent<DamagePopup>();
        dmgPopup.Setup(dmgAmount,isCritHit);
    
        return dmgPopup;
    }

    private void Awake()
    {
        textmesh=transform.GetComponent<TextMeshPro>();
        normal = new Color(255, 101, 5);
        heavy = new Color(255, 0, 5);
    }
    public void Setup(float dmgAmount,bool isCritHit){
        textmesh.SetText(dmgAmount.ToString());
        if(isCritHit){
            textmesh.fontSize=25;
            textColor = heavy;
        }
        else{
            textmesh.fontSize=8;
            textColor = normal;

        }
        // textColor=textmesh.color;
        textmesh.color=textColor;
        disappearTimer=MAX_DISAPPEAR_TIMER;

        moveVector = new Vector3(.2f,.5f)*60f;
    }


    private void Update()
    {
        transform.position+=moveVector*Time.deltaTime;
        moveVector-=moveVector*8f*Time.deltaTime;

        if(disappearTimer>MAX_DISAPPEAR_TIMER/2){
            float increaseScalaAmount=1f;
            transform.localScale += Vector3.one*increaseScalaAmount*Time.deltaTime;
        }
        else{
            float decreaseScalaAmount=1f;
            transform.localScale -= Vector3.one*decreaseScalaAmount*Time.deltaTime;

        }

        disappearTimer-=Time.deltaTime;
        if(disappearTimer<0){//Start to disappear when disappearTimer<0 with disappearSpeed
            textColor.a-=disappearSpeed*Time.deltaTime;
            textmesh.color=textColor;
            if(textColor.a<0){
                Destroy(gameObject);
            }
        }

    }
}
