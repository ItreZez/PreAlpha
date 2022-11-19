using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextosUI : MonoBehaviour
{

    [Header("Textos")]
    [SerializeField] private GameObject TextoMov1;
    [SerializeField] private GameObject TexRecargar;
    [SerializeField] private GameObject TexMov2;
    [SerializeField] private GameObject TexAtaq1;
    [SerializeField] private GameObject TexAtaq2;
    [SerializeField] private GameObject TexAtaq3;
    [SerializeField] private GameObject TexFaro;
    [SerializeField] private GameObject TexEsc;

    [Header("Bools")]
    [SerializeField] private bool textUsoActivado;
    [SerializeField] private bool textoPuedeRecargar;

    //tags----------------------------------------------------------
    //

    //Tex_Recargar
    //Tex_Mov2
    //Tex_Mov1
    //Tex_Ataq1
    //Tex_Ataq2
    //Tex_Ataq3
    //Tex_Faro
    //Tex_Esc


    private void Start()
    {
        //Moviemiento
        TextoMov1.SetActive(true);

        //Lampara
        textUsoActivado = FindObjectOfType<Lampara>().TextoUsoActivado;
        textoPuedeRecargar = FindObjectOfType<Lampara>().TextoPuedeRecargar;

    }

    private void Update()
    {

        //Lampara
        textUsoActivado = FindObjectOfType<Lampara>().TextoUsoActivado;
        textoPuedeRecargar = FindObjectOfType<Lampara>().TextoPuedeRecargar;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Movimeinto
        if(other.tag == "Tex_Mov2")
        {
            TexMov2.SetActive(true);
        }

        //Lampara
        if (other.tag == "Tex_Recargar" && textUsoActivado == true && textoPuedeRecargar == true)
        {
            TexRecargar.SetActive(true);
        }

        //ataques
        if (other.tag == "Tex_Ataq1")
        {
            TexAtaq1.SetActive(true);
        }
        if (other.tag == "Tex_Ataq2")
        {
            TexAtaq2.SetActive(true);
        }
        if (other.tag == "Tex_Ataq3")
        {
            TexAtaq3.SetActive(true);
        }

        //Faro
        if(other.tag == "Tex_Faro")
        {
            TexFaro.SetActive(true);
        }
        if (other.tag == "Tex_Esc")
        {
            TexEsc.SetActive(true);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        //Moviemiento 
        if(other.tag == "Tex_Mov1")
        {
            TextoMov1.SetActive(false);
        }
        if (other.tag == "Tex_Mov2")
        {
            TexMov2.SetActive(false);
        }

        //Lampara
        if (other.tag == "Tex_Recargar" || Input.GetKeyDown(KeyCode.R))
        {
            TexRecargar.SetActive(false);
        }

        //ataques
        if (other.tag == "Tex_Ataq1")
        {
            TexAtaq1.SetActive(false);
        }
        if (other.tag == "Tex_Ataq2")
        {
            TexAtaq2.SetActive(false);
        }
        if (other.tag == "Tex_Ataq3")
        {
            TexAtaq3.SetActive(false);
        }

        //Faro
        if (other.tag == "Tex_Faro")
        {
            TexFaro.SetActive(false);
        }
        if (other.tag == "Tex_Esc")
        {
            TexEsc.SetActive(false);
        }
    }

}
