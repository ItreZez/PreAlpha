using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float velocidad = 5f;

    [Header("Sphere")]
    public float RangoDeAlerta;
    public LayerMask CapaDelJugador;
    bool estarAlerta;

    public Transform JugadorDetect;

    [Header("Enemy Health")]
    public float enemyVida = 3f;

    [Header("Funciones de Lampara")]
    public bool isAturdido = false;
    public float tiempoAturdido = 3;

    [Header("Ai")]
    public Transform player; 
    public NavMeshAgent Enemigo;
    private int destinoCor = 0;

    public bool attack = false;

    [Header("Transforms para cordenadas")]
    public Transform[] cordenadas;


    Lampara lampara;

    private void Start()
    {
        Enemigo = GetComponent<NavMeshAgent>();
        lampara = GetComponent<Lampara>();
        
       // Esto hace que nunca para y baje la velocidad entre cordenadas
        Enemigo.autoBraking = false;

        IrAlSiguientePunto();
        
        
    }

    private void Update() 
    {

        if(estarAlerta == false){

        
         //agent.destination = player.position; 
         //Escoge el siguiente destino cuando llega a uno
         if (!Enemigo.pathPending && Enemigo.remainingDistance < 0.5f)
            {
                IrAlSiguientePunto();
            
            }

        }

        EstarAlerta();


    }

    private void OnTriggerEnter(Collider other) 
    {
        
        if (other.gameObject.tag == "Lampara"  )
        {   
            //baja vida
            //enemyVida--;
            //Aturde al enemigo
            Enemigo.speed = 0;
            isAturdido = true;
            

            //Empieza funcion para activarlo 
            StartCoroutine(Despierta());

            
            if (enemyVida == 0){
            //Para destruir enemigo
            Object.Destroy(gameObject,.5f); 

            }

            Debug.Log("vida enemigo " + enemyVida);
        }

        if (other.gameObject.tag == "Rango_Player")
        {
            attack = true;
        }

       
        
    }

    private void OnTriggerExit(Collider other)
    {
      attack = false;  
    }




    //Funcion para Activar enemigo
    IEnumerator Despierta()
    {
        yield return new WaitForSeconds(tiempoAturdido);

        isAturdido = false;
        Enemigo.speed = 5;
    }


     //Funcion para que viaje entre cordenadas
    void IrAlSiguientePunto()
    {
        // si ya no tienen cordenadas se para
         if (cordenadas.Length == 0)
            return;

         // Hace que el Enemigo se diriga a la nueva cordenada
        Enemigo.destination = cordenadas[destinoCor].position;

        // Escoge la siguiente cordenada
        // Cicla todo si es necesario
         destinoCor = (destinoCor + 1) % cordenadas.Length;

    }

    void EstarAlerta() 
    { 
        
        estarAlerta = Physics.CheckSphere(transform.position, RangoDeAlerta, CapaDelJugador);
        if (estarAlerta == true)
        {
            transform.LookAt(new Vector3(JugadorDetect.position.x, transform.position.y, JugadorDetect.position.z));
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(JugadorDetect.position.x, transform.position.y, JugadorDetect.position.z), velocidad * Time.deltaTime);
        }
        
    }

private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, RangoDeAlerta);
    }
}
