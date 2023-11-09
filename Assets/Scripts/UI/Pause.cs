using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Pause : MonoBehaviour
{
    public GameObject Panel;
    private bool isPaused=false;


    private void Awake()
    {
        Panel.SetActive(false);
    }
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
            Panel.SetActive(true);
        }
        else{
            Time.timeScale = 1;
            isPaused=false;
            Panel.SetActive(false);

        }
    }
}
