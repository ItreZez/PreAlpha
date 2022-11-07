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

    public float timepoDeEsperaAI = 15f;
    public bool pausa = false;
    [SerializeField] private Transform enemigoFaro;


    [Header("Distancia")]

    public bool atacarSinPausas = false;


    [Header("Atack/Aturdido")]
    [SerializeField] private bool isAturdidoPrincipal;
    public float tiempoAturdido = 5f;
    
    [SerializeField] private FP_Controller playerScript;
    public GameObject Player;

    [Header("Ref a transforms")]
    [SerializeField] private Transform player;

    [Header("Aumento de atributos")]
    [SerializeField] private FP_Controller cantidadDeLlavesRecogidas;

    [Header("Esconderse")]
    public bool EscondidoB;





    void Start()
    {
        StartCoroutine(Comportamiento());
        EscondidoB = GameObject.Find("Player").GetComponent<FP_Controller>().Escondido;





    }

    // Update is called once per frame
    void Update()
    {
        MovimientoEnemigoPrincipal();
        AumentoDeAtributos();
        EscondidoB = GameObject.Find("Player").GetComponent<FP_Controller>().Escondido;










    }


    public void MovimientoEnemigoPrincipal()
    {
        Vector3 direccion = player.position - transform.position;
        Vector3 direccionArbol = enemigoFaro.position - transform.position;

        if (EscondidoB == false)
        {

            transform.position += (Vector3)direccion / direccion.magnitude * Time.deltaTime * speed;
        }
        else
        {
            transform.position += (Vector3)direccionArbol / direccionArbol.magnitude * Time.deltaTime * speed;
        }


    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Lampara"  && FindObjectOfType<Lampara>().tieneLampara == true)
        {
            //baja vida
            //enemyVida--;
            //Aturde al enemigo
            speed = 0f;
            isAturdidoPrincipal = true;


            //Empieza funcion para activarlo 
            StartCoroutine(DespiertaPrincipal());
        }
        if (other.gameObject.name == "Player")
        {
            if (playerScript.playerHealth > 0 && playerScript.hit == false)
            {
                playerScript.hit = true;
                StartCoroutine(playerScript.DanoPlayer());
                EsperarEntreAtaques();

            }

            if (playerScript.playerHealth == 0)
            {
                Object.Destroy(Player, .5f);
            }

        }



    }

    /*private void OnTriggerExit(Collider other)
    {
        attack = false;
    }
    */

    IEnumerator DespiertaPrincipal()
    {
        yield return new WaitForSeconds(tiempoAturdido);
        isAturdidoPrincipal = false;
        AumentoDeAtributos();


    }



    public void AumentoDeAtributos()
    {
        if (isAturdidoPrincipal == false && pausa == false)
        {
            if (cantidadDeLlavesRecogidas.contadorLlaves == 0) speed = speedDefault;
            if (cantidadDeLlavesRecogidas.contadorLlaves == 1) speed = speed_1_llave;
            if (cantidadDeLlavesRecogidas.contadorLlaves == 2) speed = speed_2_llave;
            if (cantidadDeLlavesRecogidas.contadorLlaves == 3) speed = speed_3_llave;
            if (cantidadDeLlavesRecogidas.contadorLlaves == 4) speed = speed_4_llave;

        }



    }

    void EsperarEntreAtaques()
    {
        speed = 0;
        DespiertaPrincipal();
    }

    IEnumerator Comportamiento()
    {

        yield return new WaitForSeconds(timepoDeEsperaAI);
        pausa = true;
        speed = 0f;
        yield return new WaitForSeconds(3);
        pausa = false;
        StartCoroutine(Comportamiento());
        AumentoDeAtributos();





    }



}

