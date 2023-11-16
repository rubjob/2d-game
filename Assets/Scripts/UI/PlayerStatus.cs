using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    // private const float MAX_DISAPPEAR_TIMER = 1f;
    private float endTime;
    private Sprite bgImg;
    private Sprite Img;

    // private float disappearSpeed=3f;
    // private Vector3 moveVector;
    // public GameObject UI;
    
    //Create a PlayerStatus
    public static PlayerStatus Create(Vector3 position,float cooldown, RectTransform parent){

        Transform playerStatusTransform = Instantiate(GameAssets.instance.prefabPlayerStatus,position,Quaternion.identity,parent);

        PlayerStatus playerStatus = playerStatusTransform.GetComponent<PlayerStatus>();
        playerStatus.Setup(cooldown);
    
        return playerStatus;
    }

    private void Awake()
    {
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