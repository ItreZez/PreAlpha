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



    private void Awake() 
    {
        
    }

    private void Start()
    {
        enemySpawn = GetComponent<EnemySpawn>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == ("Player"))
        {

            
            Debug.Log("jugador detectado - activar spawn " );
            Instantiate(enemigoPrefab,SpawnDD.transform.position, Quaternion.identity);
            Instantiate(enemigoPrefab,SpawnDU.transform.position, Quaternion.identity);
            Instantiate(enemigoPrefab,SpawnID.transform.position, Quaternion.identity);
            Instantiate(enemigoPrefab,SpawnIU.transform.position, Quaternion.identity);
            
        }
    }
    
}
