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

    public Animator animator;

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

        Attack();
        Stunt();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Lampara" && FindObjectOfType<Lampara>().tieneLampara == true)
        {
            isAturdido = true;
            //animator.SetBool("ATURDIDO", true);
            StartCoroutine(Despierta());
        }

        if (other.gameObject.tag == "Player")
        {
            if (hit == false && isAturdido == false)
            {
                animator.SetBool("ATTACK", true);
                StartCoroutine(FindObjectOfType<FP_Controller>().DanoPlayer());
                hit = true;
                Invoke("attackReset", 5f);

            }
        }
    }

    void attackReset()
    {
        hit = false;
       // animator.SetBool("ATTACK", false);
    }

    IEnumerator Despierta()
    {
        yield return new WaitForSeconds(tiempoAturdido);
        //animator.SetBool("ATURDIDO", false);
        isAturdido = false;
    }


    private void Stunt()
    {

        if (isAturdido == true)
        {
            animator.SetBool("ATURDIDO", true);
        }
        if (isAturdido == false)
        {
            animator.SetBool("ATURDIDO", false);
        }
    }

    private void Attack()
    {
        if (hit == true)
        {
            animator.SetBool("ATTACK", true);
        }
        if (hit == false)
        {
            animator.SetBool("ATTACK", false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, rangoAlerta);
    }
}
