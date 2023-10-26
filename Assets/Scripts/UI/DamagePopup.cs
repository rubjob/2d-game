using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    private TextMeshPro textmesh;
    private float disappearTimer=0.5f;
    private Color textColor;
    private float disappearSpeed=3f;

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
    }
    public void Setup(float dmgAmount,bool isCritHit){
        textmesh.SetText(dmgAmount.ToString());
        if(isCritHit){
            textmesh.fontSize=25;
            // textColor.UtilsClass.GetColorFromString
        }
        else{
            textmesh.fontSize=15;
        }
        textColor=textmesh.color;


    }


    private void Update()
    {
        float moveYSpeed = 10f;
        transform.position+=new Vector3(0,moveYSpeed)*Time.deltaTime;
        
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
