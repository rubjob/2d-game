using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private const float MAX_DISAPPEAR_TIMER = 1f;
    private float disappearTimer;
    private float disappearSpeed=3f;
    private Vector3 moveVector;
    //Create a PlayerStatus
    public static PlayerStatus Create(Vector3 position){

        Transform playerStatusTransform = Instantiate(GameAssets.instance.prefabPlayerStatus,position,Quaternion.identity);

        PlayerStatus playerStatus = playerStatusTransform.GetComponent<PlayerStatus>();
        playerStatus.Setup();
    
        return playerStatus;
    }

    public void Setup(){
        moveVector = new Vector3(.2f,.5f)*60f;
    }
}