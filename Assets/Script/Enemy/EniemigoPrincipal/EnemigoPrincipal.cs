using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoPrincipal : MonoBehaviour
{
    [Header("Movimiento")]

    public float speed;
    public float speedDefault = 2f;
    public float speed_1_llave = 3f;
    public float speed_2_llave = 4f;
    public float speed_3_llave = 5f;
    public float speed_4_llave = 6f;


    [Header("Atack/Aturdido")]
    [SerializeField] private bool isAturdidoPrincipal;
    public float tiempoAturdido = 5f;
    private bool attack;

    [Header("Ref a transforms")]
    [SerializeField] private Transform player;

    [Header("Aumento de atributos")]
    [SerializeField] private FP_Controller cantidadDeLlavesRecogidas;





    void Start()
    {




    }

    // Update is called once per frame
    void Update()
    {
        MovimientoEnemigoPrincipal();
        AumentoDeAtributos();
       


    }


    public void MovimientoEnemigoPrincipal()
    {
        Vector3 direccion = player.position - transform.position;
        transform.position += (Vector3)direccion / direccion.magnitude * Time.deltaTime * speed;




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
        AumentoDeAtributos();


    }



    public void AumentoDeAtributos()
    {
        if (isAturdidoPrincipal == false)
        {
        if (cantidadDeLlavesRecogidas.contadorLlaves == 0) speed = speedDefault;
        if (cantidadDeLlavesRecogidas.contadorLlaves == 1) speed = speed_1_llave;
        if (cantidadDeLlavesRecogidas.contadorLlaves == 2) speed = speed_2_llave;
        if (cantidadDeLlavesRecogidas.contadorLlaves == 3) speed = speed_3_llave;
        if (cantidadDeLlavesRecogidas.contadorLlaves == 4) speed = speed_4_llave;
        }



    }


}

