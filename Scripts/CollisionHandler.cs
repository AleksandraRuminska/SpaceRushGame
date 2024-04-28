using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField]  ParticleSystem explosion;
    void OnTriggerEnter(Collider other) {
        StartCrashSequence();
    }

    void StartCrashSequence(){
        explosion.Play();
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<PlayerController>().enabled = false;
        Invoke("ReloadLevel", loadDelay);
    }

    void ReloadLevel(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

}
