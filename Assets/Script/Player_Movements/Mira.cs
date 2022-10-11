using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mira : MonoBehaviour
{
    //Variables
    public float Sensitivity = 100f;
    public Transform player;
    float xRotation = 0f;




    void Start()
    {
        //Sirve para mantener el cursor del mouse en el centro
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        
        float mouseX = Input.GetAxis ( "Mouse X") * Sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis ( "Mouse Y") * Sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation,-90,75f);

        transform.localRotation= Quaternion.Euler(xRotation,0f,0f);
        player.Rotate(Vector3.up * mouseX);
        
    }
}
