using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavPoints : MonoBehaviour
{
    [Header("PlayerDetected")]//-----------------------------------------------------------------------------PlayerDetected
    [SerializeField] private float velocidad = 5f;
    [Header("Sphere")]
    [SerializeField] private float RangoDeAlerta;
    [SerializeField] private LayerMask CapaDelJugador;
    bool estarAlerta;



    [Header("RandomPointPatrol")]//------------------------------------------------------------------------RandomPointPatrol
    private NavMeshAgent nma = null;
    private Bounds bndFloor;
    [SerializeField] private GameObject floor;
    [SerializeField] private GameObject pole;
    [SerializeField] private float BoundChangeTime;
    private Vector3 moveto;

    [Header("AI")]//----------------------------------------------------------------------------------------AI
    public Transform player;
    public NavMeshAgent Enemigo;

    [Header("Funciones de Lampara")]//----------------------------------------------------------------------Funciones de Lampara
    public float tiempoAturdido = 3;
    public bool isAturdido = false;
    Lampara lampara;

    [Header("Enemy Health")]//-----------------------------------------------------------------------------Enemy Health
    public float enemyVida = 3f;
    public bool attack = false;



    private void Start()
    {
        //RandomPointPatrol
        nma = this.GetComponent<NavMeshAgent>();
        bndFloor = floor.GetComponent<Renderer>().bounds;
        SetRandomDestination();


        //J
        Enemigo = GetComponent<NavMeshAgent>();
        lampara = GetComponent<Lampara>();

    }

    private void Update()
    {
        //PlayerDetected
        EstarAlerta();
    }
    //RandomPointPatrol
    void SetRandomDestination()
    {
        float rx = Random.Range(bndFloor.min.x, bndFloor.max.x);
        float rz = Random.Range(bndFloor.min.z, bndFloor.max.z);

        moveto = new Vector3(rx, transform.position.y, rz);

        nma.SetDestination(moveto);

        pole.transform.position = new Vector3(moveto.x, this.transform.position.y, moveto.z);

        Invoke("CheckPointOnPath", BoundChangeTime);


    }
    //RandomPointPatrol
    void CheckPointOnPath()
    {
        if (nma.pathEndPosition != moveto)
        {
            //el punto no esta en el navMash
            SetRandomDestination();

        }
    }



    //PlayerDetected
    void EstarAlerta()
    {

        estarAlerta = Physics.CheckSphere(transform.position, RangoDeAlerta, CapaDelJugador);
        if (estarAlerta == true)
        {
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, transform.position.y, player.position.z), velocidad * Time.deltaTime);
        }

    }
    //PlayerDetected
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, RangoDeAlerta);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Lampara")
        {
            //baja vida
            //enemyVida--;
            //Aturde al enemigo
            Enemigo.speed = 0;
            isAturdido = true;


            //Empieza funcion para activarlo 
            StartCoroutine(Despierta());


            if (enemyVida == 0)
            {
                //Para destruir enemigo
                Object.Destroy(gameObject, .5f);

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
}
