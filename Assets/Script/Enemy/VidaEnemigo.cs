using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaEnemigo : MonoBehaviour
{
    public int vida ;
    private EnemyState enemyState;
   
    void Start()
    {
        enemyState = GetComponent<EnemyState>();
        vida = 3;
        
    }

    // Update is called once per frame
    void Update()
    {
        MatarEnemigo();
        
    }

    private void MatarEnemigo()
    {
        if(vida == 0 )
        {
            Destroy(gameObject, 2.3f);
        }
        Debug.Log(vida);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Lampara") vida--;
        
    }
}
