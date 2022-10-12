using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoPrincipal : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 5f;
    private Vector3 direccion;
    [SerializeField] Rigidbody rb;

    [Header("Atack/Aturdido")]
    [SerializeField] private bool isAturdidoPrincipal;
    public float tiempoAturdido = 5f;
    private bool attack;

    [Header("Ref a transforms")]
    [SerializeField] private Transform player;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MovimientoEnemigoPrincipal();

    }


    public void MovimientoEnemigoPrincipal()
    {
        direccion = player.position - transform.position;
        rb.MovePosition((Vector3) transform.position + (direccion * speed * Time.deltaTime));
       


    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Lampara")
        {
            //baja vida
            //enemyVida--;
            //Aturde al enemigo
            speed = 0f;
            isAturdidoPrincipal = true;


            //Empieza funcion para activarlo 
            StartCoroutine(DespiertaPrincipal());


            

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

    IEnumerator DespiertaPrincipal()
    {
        yield return new WaitForSeconds(tiempoAturdido);

        isAturdidoPrincipal = false;
        speed = 5;
    }

}

