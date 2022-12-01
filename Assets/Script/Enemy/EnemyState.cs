using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum AI_State
{
    IDLE, PATROLLING, CHASING, ATTACKING
}
public class EnemyState : MonoBehaviour
{

    public AI_State currentAIState;//estado actual del enemigo

    public float chaseRange; //rango de persecucion 
    public float attackRange ; // rango de ataque 

    public float timeBetweenAttacks = 2f; //tiempo entre ataques


    public float waitAtPoint = 2f; //tiempo de espera en un punto
    private float waitCounter; //contador de espera en puntos 
    private float attackCounter; //cooldown entre ataques  

    public NavMeshAgent nma = null;

    //RandomBounds
    private Bounds bndFloor;
    //[SerializeField] private GameObject floor;
    [SerializeField] private GameObject pole;
    private Vector3 moveto;
   

    [Header("Funciones de Lampara")]
    public bool isAturdido = false;
    public float tiempoAturdido = 3;

    [Header("Esconderse")]
    public bool EscondidoB;

    public bool movimiento;

    public bool Ataque;

    [Header("Sfx")]
    public AudioSource AS_Walk;



    public void Awake()
    {
        nma = this.GetComponent<NavMeshAgent>();
        Ataque = false;


    }

    public void Start()
    {
        nma = this.GetComponent<NavMeshAgent>();
        bndFloor = GameObject.Find("Terreno").GetComponent<MeshRenderer>().bounds;

        waitCounter = waitAtPoint;

        //fp_controller = GetComponent<FP_Controller>();

        EscondidoB = GameObject.Find("Player").GetComponent<FP_Controller>().Escondido;

        AS_Walk.Stop();

    }

    private void Update()
    {
        NavMovementsEnemy();
        EscondidoB = GameObject.Find("Player").GetComponent<FP_Controller>().Escondido;

        if (isAturdido == true)
        {
            AS_Walk.Stop();
        }

    }

    public void NavMovementsEnemy()
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
                    //SetRandomDestination();
                    SetRandomDestination();
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

                if (EscondidoB == false && _distanceToPlayer <= attackRange)
                {
                    currentAIState = AI_State.ATTACKING;
                    nma.velocity = Vector3.zero;
                    nma.isStopped = true;
                    attackCounter = timeBetweenAttacks;
                    AS_Walk.Stop();

                }

                if (_distanceToPlayer > chaseRange)
                {
                    currentAIState = AI_State.IDLE;
                    waitCounter = waitAtPoint;
                    nma.velocity = Vector3.zero;
                    nma.SetDestination(transform.position);
                    AS_Walk.Stop();

                }
                break;

            case AI_State.ATTACKING:
            
                transform.LookAt(GameObject.FindWithTag("Player").transform.position, Vector3.up);
                transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

                attackCounter -= Time.deltaTime;

                if (attackCounter <= 0)
                {
                    if (_distanceToPlayer < attackRange)
                    {
                        attackCounter = timeBetweenAttacks;
                        AS_Walk.Stop();

                    }
                    else
                    {
                        currentAIState = AI_State.IDLE;
                        waitCounter = waitAtPoint;
                        nma.isStopped = false;
                        AS_Walk.Stop();

                    }
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && isAturdido == false)
        {
            Ataque = true;
            StartCoroutine(Ataco());
        }
    }

    

    IEnumerator Ataco()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        Ataque = false;

    }
    IEnumerator Despierta()
    {
        yield return new WaitForSeconds(tiempoAturdido);

        isAturdido = false;
        currentAIState = AI_State.PATROLLING;
        nma.speed = 5;

    }

    private void SetRandomDestination()
    {
        float rx = Random.Range(bndFloor.min.x, bndFloor.max.x);
        //float rx = Random.Range(60, -60);
        float rz = Random.Range(bndFloor.min.z, bndFloor.max.z);
        //float rz = Random.Range(60, -60);

        
        moveto = new Vector3(rx, this.transform.position.y, rz);
        //RandomDestinationPole
        nma.SetDestination(moveto);

        pole.transform.position = new Vector3(moveto.x, pole.transform.position.y, moveto.z);

        Invoke("CheckPointOnPath", 15f);
       

    }

    void CheckPointOnPath()
    {
      
        if (nma.pathEndPosition != moveto)
        {
            //el punto no esta en el navMash
            SetRandomDestination();

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
      
 
}
