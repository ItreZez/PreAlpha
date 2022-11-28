using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArañaTutorial : MonoBehaviour
{

    public Transform JugadorDetect;

    [Header("Sphere")]
    public float RangoDeAlerta;
    public LayerMask CapaDelJugador;
    bool estarAlerta;
    public float velocidad = 5f;

    public GameObject luz;

    public Lampara pila;

    public int vida = 3;
    // Start is called before the first frame update

    public bool aturdido;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        EstarAlerta();
        if(vida == 0)
        {
            Destroy(gameObject,1f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, RangoDeAlerta);
    }


    void EstarAlerta()
    {

        estarAlerta = Physics.CheckSphere(transform.position, RangoDeAlerta, CapaDelJugador);
        if (estarAlerta == true && aturdido == false)
        {
            transform.LookAt(new Vector3(JugadorDetect.position.x, transform.position.y, JugadorDetect.position.z));
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(JugadorDetect.position.x, transform.position.y, JugadorDetect.position.z), velocidad * Time.deltaTime);
        }

    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == luz)
        {
            aturdido = true;
            pila.bateria = pila.bateria - 5f;
            StartCoroutine(Despierta());
        }
    }

    IEnumerator Despierta()
    {
        vida = vida - 1;
        yield return new WaitForSeconds(3f);
        aturdido = false;
    }

}

