using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    private TextMeshPro textmesh;

    public static DamagePopup Create(Vector3 position,int dmgAmount){

        Transform dmgPopupTransform = Instantiate(GameAssets.instance.prefabDmgPopup,position,Quaternion.identity);

        DamagePopup dmgPopup = dmgPopupTransform.GetComponent<DamagePopup>();
        dmgPopup.Setup(dmgAmount);
    
        return dmgPopup;
    }

    private void Awake()
    {
        textmesh=transform.GetComponent<TextMeshPro>();
    }
    public void Setup(int dmgAmount){
        textmesh.SetText(dmgAmount.ToString());
    }
}
