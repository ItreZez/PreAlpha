using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoPrincipal : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 5f;

 

    [Header("Atack/Aturdido")]
    [SerializeField] private bool isAturdidoPrincipal;
    public float tiempoAturdido = 5f;
    private bool attack;

    [Header("Ref a transforms")]
    [SerializeField] private Transform player;

    [Header("Aumento de atributos")]
    [SerializeField] private int cantidadDeLlavesRecogidas;
    
   


    void Start()
    {
        cantidadDeLlavesRecogidas = FindObjectOfType<Lampara>().inventarioPilas.Count;
        
        
       
    }

    // Update is called once per frame
    void Update()
    {
        MovimientoEnemigoPrincipal();
        

    }


    public void MovimientoEnemigoPrincipal()
    {
       Vector3 direccion = player.position - transform.position;
       transform.position += (Vector3)direccion/direccion.magnitude * Time.deltaTime * speed;
       
       


    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Lampara")
        {
            //baja vida
            //enemyVida--;
            //Aturde al enemigo
            speed = 0f;
            isAturdidoPrincipal = true;


            //Empieza funcion para activarlo 
            StartCoroutine(DespiertaPrincipal());


            

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

    IEnumerator DespiertaPrincipal()
    {
        yield return new WaitForSeconds(tiempoAturdido);

        isAturdidoPrincipal = false;
        speed = 2;
    }

    
}

