using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    // private const float MAX_DISAPPEAR_TIMER = 1f;
    private float endTime;
    // private float disappearSpeed=3f;
    // private Vector3 moveVector;
    // public GameObject UI;
    // public RectTransform parent;
    
    //Create a PlayerStatus
    public static PlayerStatus Create(Vector3 position,float cooldown){

        Transform playerStatusTransform = Instantiate(GameAssets.instance.prefabPlayerStatus,position,Quaternion.identity);

        PlayerStatus playerStatus = playerStatusTransform.GetComponent<PlayerStatus>();
        playerStatus.Setup(cooldown);
    
        return playerStatus;
    }

    public void Setup(float cooldown){
       endTime=cooldown+Time.time;
    }


    private void Update()
    {
        if(endTime<=Time.time){
            Destroy(gameObject);
        }
    }
}