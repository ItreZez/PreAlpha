using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PlantaMov : MonoBehaviour
{

    public float rangoAlerta;
    public LayerMask capaJugador;
    public bool estarAlerta;
    public Transform player;

    public bool Escondido;
    public bool hit;


    [Header("Funciones de Lampara")]
    public bool isAturdido = false;
    public float tiempoAturdido = 3;


    private void Start()
    {
        Escondido = GameObject.Find("Player").GetComponent<FP_Controller>().Escondido;
        
    }

    private void Update()
    {
        estarAlerta = Physics.CheckSphere(transform.position, rangoAlerta, capaJugador);
        Escondido = GameObject.Find("Player").GetComponent<FP_Controller>().Escondido;


        if (estarAlerta == true && Escondido == false && isAturdido == false)
        {
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Lampara" && FindObjectOfType<Lampara>().tieneLampara == true)
        {
            isAturdido = true;
            StartCoroutine(Despierta());
        }

        if (other.gameObject.tag == "Player")
        {
            if (hit == false && isAturdido == false)
            {
                StartCoroutine(FindObjectOfType<FP_Controller>().DanoPlayer());
                hit = true;
                Invoke("attackReset", 5f);

            }
        }
    }

    void attackReset()
    {
        hit = false;
    }

    IEnumerator Despierta()
    {
        yield return new WaitForSeconds(tiempoAturdido);
        isAturdido = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, rangoAlerta);
    }
}
