using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mapa : MonoBehaviour
{
    
    public GameObject player;

  private void OnTriggerStay(Collider other)
    {
        if(other.gameObject == player && Input.GetKey(KeyCode.E))
        {        

            Debug.Log("Recogio Mapa");
            FindObjectOfType<FP_Controller>().recogioMapa = true;
            Destroy(gameObject);   

        }
    }
}
