using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccionEnter : MonoBehaviour
{
    private bool spawn;

    private void Awake() 
    {
        spawn = GetComponent<EnemySpawn>().jugadorDetectado;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == ("Player"))
        {

            
            Debug.Log("jugador detectado - activar spawns" + spawn);
            spawn = true;
           //Debug.Log(spawn.jugadorDetectado);
        }
    }
}
