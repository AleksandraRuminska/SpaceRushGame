using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    void Awake(){
        int numOfMusPlay = FindObjectsOfType<MusicPlayer>().Length;

        if(numOfMusPlay > 1){
            Destroy(gameObject); 
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }
}
