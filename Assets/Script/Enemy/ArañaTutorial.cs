using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ArañaTutorial : MonoBehaviour
{
    public AI_State currentAIState;//estado actual del enemigo

    public float chaseRange; //rango de persecucion 
    public float attackRange; // rango de ataque 

    public float timeBetweenAttacks = 2f; //tiempo entre ataques

   public AudioSource AS_Walk;

    public float waitAtPoint = 2f; //tiempo de espera en un punto

    private float waitCounter; //contador de espera en puntos 
    private float attackCounter; //cooldown entre ataques  

    private NavMeshAgent nma = null;

    [Header("Transforms para cordenadas")]
    public Transform[] cordenadas;


    [Header("Funciones de Lampara")]
    public bool isAturdido = false;
    public float tiempoAturdido = 3;

    [Header("Ai")]
    public Transform player;
    public NavMeshAgent Enemigo;
    private int destinoCor = 0;

    [Header("Esconderse")]
    public bool EscondidoB;

    private void Awake()
    {
        nma = this.GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        nma = this.GetComponent<NavMeshAgent>();

        waitCounter = waitAtPoint;

        //fp_controller = GetComponent<FP_Controller>();

        EscondidoB = GameObject.Find("Player").GetComponent<FP_Controller>().Escondido;

        IrAlSiguientePunto();

       AS_Walk.Stop();

    }

    private void Update()
    {
        NavMovementsEnemy();
        EscondidoB = GameObject.Find("Player").GetComponent<FP_Controller>().Escondido;

        if(isAturdido == true)
        {
           AS_Walk.Stop();
        }

    }

    void NavMovementsEnemy()
    {
        float _distanceToPlayer = Vector3.Distance(transform.position, GameObject.FindWithTag("Player").transform.position);

        switch (currentAIState)
        {
            case AI_State.IDLE://-------------------------------------------------IDLE
                if (waitCounter > 0)
                {
                    waitCounter -= Time.deltaTime;

                    AS_Walk.Stop();
                }
                else
                {
                    currentAIState = AI_State.PATROLLING;
                    IrAlSiguientePunto();

                    AS_Walk.Play();

                }

                if (EscondidoB == false && _distanceToPlayer <= chaseRange)
                {
                    currentAIState = AI_State.CHASING;

                    AS_Walk.Play();
                }

                break;

            case AI_State.PATROLLING:

                if (nma.remainingDistance <= .2f)
                {
                    currentAIState = AI_State.IDLE;

                    waitCounter = waitAtPoint;

                    AS_Walk.Stop();

                }
                if (EscondidoB == false && _distanceToPlayer <= chaseRange)
                {
                    currentAIState = AI_State.CHASING;

                    AS_Walk.Play();

                }

                break;

            case AI_State.CHASING:

                nma.SetDestination(GameObject.FindWithTag("Player").transform.position);

                if (_distanceToPlayer > chaseRange)
                {
                    currentAIState = AI_State.IDLE;
                    waitCounter = waitAtPoint;
                    nma.velocity = Vector3.zero;
                    nma.SetDestination(transform.position);

                    AS_Walk.Stop();

                }
                break;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Lampara" && FindObjectOfType<Lampara>().tieneLampara == true)
        {
            nma.speed = 0;
            isAturdido = true;

            StartCoroutine(Despierta());

        }
    }

    IEnumerator Despierta()
    {
        yield return new WaitForSeconds(tiempoAturdido);

        isAturdido = false;
        currentAIState = AI_State.PATROLLING;
        nma.speed = 5;

    }

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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}

