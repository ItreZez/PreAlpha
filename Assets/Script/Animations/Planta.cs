using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planta : EnemySpawn
{
    public Animator animator;
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Aturdir();
    }

    private void Aturdir()
    {
        if(aturdido == true)
        {
            animator.SetBool("ATURDIDO",true);
        }
        if(aturdido == false)
        {
            animator.SetBool("ATURDIDO",false);
        }
    }
}
