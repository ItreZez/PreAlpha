 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccionEnter : MonoBehaviour
{
    private EnemySpawn enemySpawn;
    public GameObject enemigoPrefab;

    [Header("Spawns")]
    public GameObject SpawnDD;
    public GameObject SpawnDU;
    public GameObject SpawnID;
    public GameObject SpawnIU;

    [Header("Esconderse")]
    public bool EscondidoB;

    public ShakeCamara shakecamara;
    public bool JugadorShake = false;

    private void Awake() 
    {
        
    }

    private void Start()
    {
        enemySpawn = GetComponent<EnemySpawn>();
        EscondidoB = GameObject.Find("Player").GetComponent<FP_Controller>().Escondido;

    }

    private void Update()
    {
        EscondidoB = GameObject.Find("Player").GetComponent<FP_Controller>().Escondido;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(EscondidoB == false && other.gameObject.tag == ("Player"))
        {

            StartCoroutine(shakecamara.Shake());

            Debug.Log("jugador detectado - activar spawn " );
            Instantiate(enemigoPrefab,SpawnDD.transform.position, Quaternion.identity);
            Instantiate(enemigoPrefab,SpawnDU.transform.position, Quaternion.identity);
            Instantiate(enemigoPrefab,SpawnID.transform.position, Quaternion.identity);
            Instantiate(enemigoPrefab,SpawnIU.transform.position, Quaternion.identity);
            
        }
    }
    
}
