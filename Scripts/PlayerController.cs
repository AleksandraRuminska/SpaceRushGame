using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("General Setup Settings")]
    [Tooltip("How fast ship moves up and down")] [SerializeField] float controlSpeed = 30f;
    [SerializeField] float xRange = 10f;
    [SerializeField] float yRange = 7f;

    [Header("Screen position based tuning")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float positionYawFactor = 2f;

    [Header("Player input based tuning")]
    [SerializeField] float controlPitchFactor = -15f;
    [SerializeField] float controlRollFactor = -20f;

    [Header("Laser Settings")]
    [Tooltip("Add all players' lasers")] [SerializeField] GameObject[] lasers;

    float horizontalThrow;
    float verticalThrow;

    void Update()
    {
       ProcessTranslation();
       ProcessRotation();
       ProcessFiring();
    }

    void ProcessTranslation(){
        horizontalThrow = Input.GetAxis("Horizontal");
        verticalThrow = Input.GetAxis("Vertical");
        
        float xOffset = horizontalThrow*Time.deltaTime * controlSpeed;
        float newX = transform.localPosition.x+xOffset;
        float clampedNewX = Mathf.Clamp(newX, -xRange, xRange);

        float yOffset = verticalThrow *Time.deltaTime * controlSpeed;
        float newY = transform.localPosition.y+yOffset;
        float clampedNewY = Mathf.Clamp(newY, -yRange, yRange);

        transform.localPosition = new Vector3(clampedNewX, clampedNewY, transform.localPosition.z);
    }

    void ProcessRotation(){
        float pitch = transform.localPosition.y * positionPitchFactor + verticalThrow* controlPitchFactor;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = horizontalThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessFiring(){
        if (Input.GetButton("Fire1")){
            SetLasersActive(true);
        } else{
            SetLasersActive(false);
        }
    }

    void SetLasersActive(bool isActive){
        foreach (GameObject laser in lasers) {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }

}
