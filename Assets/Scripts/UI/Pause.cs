using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Pause : MonoBehaviour
{
    private bool isPaused=false;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            onPause();
        }
    }
    public void onPause(){
        if(!isPaused){
            Time.timeScale = 0;
            isPaused=true;
        }
        else{
            Time.timeScale = 1;
            isPaused=false;
        }
    }
}
