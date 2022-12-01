using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlanta : MonoBehaviour
{
    public bool jugadorDetectado = false;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float tiempoIntervalo;
    [SerializeField] private float waveTime = 4f; //con este tiempo alcanza a spawnear 2 enemigos por spawn

    //Se tiene que cambiar la variable de intervalo para poderlo perzonalizar más y tener mas control

    private Enemy enemigo;

    public bool hit;


    void Start()
    {
        SpawnEnemy();
        enemigo = GetComponent<Enemy>();
    }

    private void Update()
    {
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {


        if (jugadorDetectado == false)
        {

            tiempoIntervalo = 60f * 3f;
            StartCoroutine(InstanceEnemyFalse(tiempoIntervalo));

        }
    }
    IEnumerator InstanceEnemyFalse(float Intervalo)
    {
        yield return new WaitForSeconds(Intervalo);
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        SpawnEnemy();


    }

    IEnumerator InstanceEnemyTrue(float Intervalo)
    {
        yield return new WaitForSeconds(Intervalo);
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        SpawnEnemy();

    }

    IEnumerator StopWave()
    {
        if (jugadorDetectado == true)
        {
            yield return new WaitForSeconds(waveTime);
            jugadorDetectado = false;
        }
    }
}
