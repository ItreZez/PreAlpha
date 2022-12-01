using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public bool jugadorDetectado = false;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float tiempoIntervalo;
     [SerializeField] private float waveTime = 4f; //con este tiempo alcanza a spawnear 2 enemigos por spawn

    //Se tiene que cambiar la variable de intervalo para poderlo perzonalizar más y tener mas control

    private Enemy enemigo;

    
    public float rangoAlerta;
    public LayerMask capaJugador;
    public bool estarAlerta;
    public Transform player;

    public bool Escondido;
    public bool aturdido;

    void Start()
    {
        SpawnEnemy();
        enemigo = GetComponent<Enemy>();
        Escondido = GameObject.Find("Player").GetComponent<FP_Controller>().Escondido;
        //aturdido = GetComponent<AttackSpawn>().aturdido;


    }


    // Update is called once per frame
    void Update()
    {
        estarAlerta = Physics.CheckSphere(transform.position, rangoAlerta, capaJugador);

        Escondido = GameObject.Find("Player").GetComponent<FP_Controller>().Escondido;

        if (estarAlerta == true && Escondido == false && aturdido == false)
        {
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
        }
    }

    public void SpawnEnemy()
    {
       

        if(jugadorDetectado == false)
        {
            
            tiempoIntervalo = 60f * 3f;
            StartCoroutine(InstanceEnemyFalse(tiempoIntervalo));
            
        }
    }
    
    IEnumerator InstanceEnemyFalse(float Intervalo)
    {
        yield return new WaitForSeconds(Intervalo);
        Instantiate(enemyPrefab,transform.position, Quaternion.identity);
        SpawnEnemy();
        
        
    }

     IEnumerator InstanceEnemyTrue(float Intervalo)
    {
        yield return new WaitForSeconds(Intervalo);
        Instantiate(enemyPrefab,transform.position, Quaternion.identity);
        SpawnEnemy();
          
    }

    IEnumerator StopWave()
    {
        if(jugadorDetectado == true)
        {
            yield return new WaitForSeconds(waveTime);
            jugadorDetectado = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position,rangoAlerta);
    }
}
