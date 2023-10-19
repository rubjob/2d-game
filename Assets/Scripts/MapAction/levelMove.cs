using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class levelMove : MonoBehaviour
{
   public string sceneToLoad;


   private void OnTriggerEnter2D(Collider2D other){
        print("TriggerEnter");

        if(other.tag == "GameController"){
            print("Switch to");
            SceneManager.LoadScene(sceneToLoad);
        }
   }
}
