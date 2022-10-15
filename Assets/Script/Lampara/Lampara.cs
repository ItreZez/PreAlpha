using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lampara : MonoBehaviour
{

    [Header("On/Off")]
    public bool isOn;


    [Header("Porcentaje de pila")]
    public float bateria = 100f;
    public bool siBateria;

    [Header("Uso de Pila")]
    public float usoBateria = 0.01f;
    public bool puedeRecargar;

    [Header("UI")]
    [SerializeField] private Slider CargaSlider;
    [SerializeField] private Image RecargasSlider;


    public List<int> inventarioPilas = new List<int>();
    GameObject _lampara;
    Pila pila;
    FP_Controller player;
    private EnemigoPrincipal enemigoP;





    void Start()
    {
        _lampara = GameObject.Find("Lampara");
        pila = GetComponent<Pila>();
        player = GetComponent<FP_Controller>();
        CargaSlider.value = bateria;
       

    }

    // Update is called once per frame
    void Update()
    {
        ApagarLampara();
        RecargarPila();
        AgregarPilaInventario();





    }

    private void FixedUpdate()
    {
        StartCoroutine(RestaBateria());
    }

    public void ApagarLampara()
    {
        
        if (Input.GetMouseButtonDown(1))
        {


            if (isOn == true)
            {
                isOn = false;
            }
            else
            {
                if (siBateria == true)
                isOn = true;
            }


            Debug.Log("Lampara esta prendida:" + isOn);

        }

        if (isOn == false)
        {
            _lampara.SetActive(false);
        }
        else
        {
            _lampara.SetActive(true);
        }

    }



    IEnumerator RestaBateria()
    {
        if (isOn == true)
        {
            yield return new WaitForSeconds(0.1f);
            bateria = bateria - usoBateria;
            CargaSlider.value = bateria;

        }
        StopCoroutine(RestaBateria());

        if (bateria <= 0.1f)
        {
            isOn = false;
            siBateria = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && bateria >=6)
        {
            bateria = bateria - 5f;
            CargaSlider.value = bateria;
        }

        if(other.gameObject.tag == "EnemigoPrincipal" && bateria >=6)
        {
            bateria = bateria - 5f;
            CargaSlider.value = bateria;
        }
    }


    public void RecargarPila()
    {
        if (bateria <= 1f)
        {
            puedeRecargar = true;
        }
        else
        {
            puedeRecargar = false;
        }


        if (Input.GetKeyDown(KeyCode.R) && puedeRecargar == true)
        {

            if(inventarioPilas.Count > 0)
            {
                bateria = bateria + 100f;
                inventarioPilas.Remove(1);
                siBateria = true;
                CargaSlider.value = bateria;
            }
        }

    }

    public void AgregarPilaInventario()
    {
        if (player.seRecogioPila == true)
        {
            inventarioPilas.Add(1);
            Debug.Log(inventarioPilas.Count);
            
            

        }
    }







}
