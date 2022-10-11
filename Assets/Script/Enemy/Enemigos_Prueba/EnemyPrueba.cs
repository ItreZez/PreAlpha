using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPrueba : MonoBehaviour
{
    [Header("Enemy Movimiento")]
    public float enemyWalkingSpeed  = 5;

    [Header("Funciones de Lampara")]
    public bool isAturdido = false;

    public float tiempoAturdido = 3; 

    [Header("Perseguir JugadorDetect")]
    public float disMax = 4;
    public float disMin = 5f;


    public Transform Player;

   

    private void Update() 
    {
        transform.LookAt(Player);
 
         if (Vector3.Distance(transform.position, Player.position) >= disMin)
         {
 
             transform.position += transform.forward * enemyWalkingSpeed * Time.deltaTime;



        }
    }


    private void OnTriggerEnter(Collider other) 

    {
        if (other.gameObject.tag == "Lampara")
        {
            //Para destruir enemigo
            //DestroyObject(gameObject); 


            //Aturde al enemigo
            enemyWalkingSpeed = 0;
            isAturdido = true;

            //Empieza funcion para activarlo 
            StartCoroutine(Despierta());

            Debug.Log("matalo");
        }
        
    }
    //Funcion para Activar enemigo
    IEnumerator Despierta()
    {
        yield return new WaitForSeconds(tiempoAturdido);

        isAturdido = false;
        enemyWalkingSpeed = 5;
    

    }

}
