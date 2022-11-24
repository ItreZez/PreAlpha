using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Misiones : MonoBehaviour
{

    [Header("Numeros")]
    [SerializeField] private int NumeroLlaves;

    [Header("Misiones")]
    [SerializeField] private TextMeshProUGUI Mision1;
    [SerializeField] private TextMeshProUGUI Mision2;
    [SerializeField] private TextMeshProUGUI Mision3;

    [Header("Colores")]
    [SerializeField] private Color ColorMisionCumplida;



    // Start is called before the first frame update
    void Start()
    {
        NumeroLlaves = FindObjectOfType<FP_Controller>().contadorLlaves;

        Mision1.color = Color.white;
        Mision2.color = Color.white;
        Mision3.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        NumeroLlaves = FindObjectOfType<FP_Controller>().contadorLlaves;
        MisionesCumplidas();
    }

    void MisionesCumplidas()
    {
        if(NumeroLlaves == 4)
        {
            Mision1.color = ColorMisionCumplida;
        }
        if (NumeroLlaves == 5)
        {
            Mision2.color = ColorMisionCumplida;
        }
        if (NumeroLlaves == 6)
        {
            Mision3.color = ColorMisionCumplida;
        }
    }
}
