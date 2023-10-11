using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    private TextMeshPro textmesh;

    //Create a DamagePopup
    public static DamagePopup Create(Vector3 position,float dmgAmount){

        Transform dmgPopupTransform = Instantiate(GameAssets.instance.prefabDmgPopup,position,Quaternion.identity);

        DamagePopup dmgPopup = dmgPopupTransform.GetComponent<DamagePopup>();
        dmgPopup.Setup(dmgAmount);
    
        return dmgPopup;
    }

    private void Awake()
    {
        textmesh=transform.GetComponent<TextMeshPro>();
    }
    public void Setup(float dmgAmount){
        textmesh.SetText(dmgAmount.ToString());
    }


    private void Update()
    {
        float moveYSpeed = 20f;
        transform.position+=new Vector3(0,moveYSpeed)*Time.deltaTime;
        
    }
}
