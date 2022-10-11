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

    

    void Start()
    {
        SpawnEnemy();
        enemigo = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(StopWave());
    }

    public void SpawnEnemy()
    {
       

        if(jugadorDetectado == false)
        {
            
        
            tiempoIntervalo = 10f;
            StartCoroutine(InstanceEnemyFalse(tiempoIntervalo));
            
        }

        if(jugadorDetectado == true)
        {
            //StopAllCoroutines();
            tiempoIntervalo = 1f;
            StartCoroutine(InstanceEnemyTrue(tiempoIntervalo));
           
            //Subir velocidad y vida a enemigos.
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


}
