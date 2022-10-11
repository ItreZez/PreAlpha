using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovEnemy : MonoBehaviour
{

    public float velocidadDeRotacion = 20f;

    AccionEnter AccionEnterparar;


    void Update()
    {

        //rotacion
        this.transform.Rotate(new Vector3(0, velocidadDeRotacion, 0) * Time.deltaTime);

    }

    private void Start()
    {
        
    }

  
}
