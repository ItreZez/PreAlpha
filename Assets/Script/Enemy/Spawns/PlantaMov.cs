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

    [Header("Sfx")]
    [SerializeField] private AudioSource Sfx_Idle;
    [SerializeField] private GameObject Sfx_Attak;
    [SerializeField] private GameObject Sfx_Stun;


    private void Start()
    {
        Escondido = GameObject.Find("Player").GetComponent<FP_Controller>().Escondido;
        Sfx_Idle.Play();
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
            Destroy(Instantiate(Sfx_Stun, transform.position, Quaternion.identity), 1f);

            //animator.SetBool("ATURDIDO", true);
            StartCoroutine(Despierta());
        }

        if (other.gameObject.tag == "Player")
        {
            if (hit == false && isAturdido == false)
            {
                hit = true;
                Destroy(Instantiate(Sfx_Attak, transform.position, Quaternion.identity), 1f);

                StartCoroutine(FindObjectOfType<FP_Controller>().DanoPlayer());

                Invoke("attackReset", 5f);

            }
        }
    }

    void attackReset()
    {
        hit = false;
        Sfx_Idle.Play();

    }

    IEnumerator Despierta()
    {
        yield return new WaitForSeconds(tiempoAturdido);
        //animator.SetBool("ATURDIDO", false);
        isAturdido = false;
        Sfx_Idle.Play();

    }


    private void Stunt()
    {

        if (isAturdido == true)
        {
            Sfx_Idle.Stop();
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
            Sfx_Idle.Stop();
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
