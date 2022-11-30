using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpawn : MonoBehaviour
{
    public bool hit;
    public EnemySpawn spawn;
    private void Start()
    {
        spawn = GetComponent<EnemySpawn>();
        
    }
    private void OnTriggerEnter(Collider other)
    {
        print("ENTRO");
        print(other.gameObject.tag);

        if (other.gameObject.tag == "Player")
        {
            if (hit == false && spawn.aturdido == false)
            {
                StartCoroutine(FindObjectOfType<FP_Controller>().DanoPlayer());
                hit = true;
                Invoke("attackReset", 5f);

            }
        }

        if (other.gameObject.tag == "Lampara")
        {
            print("NOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO");

            StartCoroutine(aturdidoReset());


        }
    }

    void attackReset()
    {
        hit = false;
    }

    IEnumerator aturdidoReset()
    {
        spawn.aturdido = true;
        yield return new WaitForSeconds(3);
        print("111111111111111111111111111");
        spawn.aturdido = false;
    }
}
