using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPilas : MonoBehaviour
{
    [SerializeField] private GameObject Pila;
    [SerializeField] private int xPos;
    [SerializeField] private int zPos;

    [SerializeField] private int zR1;
    [SerializeField] private int zR2;
    [SerializeField] private int xR1;
    [SerializeField] private int xR2;


    private void Start()
    {
        StartCoroutine(TiempoSpawn());
    }
    private void Update()
    {

    }

    public IEnumerator TiempoSpawn()
    {
        while (true)
        {


            xPos = Random.Range(xR1, xR2);
            zPos = Random.Range(zR1, zR2);


            int wait_time = Random.Range(55, 60);
            yield return new WaitForSeconds(wait_time);

            Instantiate(Pila, new Vector3(xPos, 4, zPos), Quaternion.identity);



        }

    }
}
