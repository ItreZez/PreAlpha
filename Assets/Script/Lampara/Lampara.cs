using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Lampara : MonoBehaviour
{

    [Header("On/Off")]
    public bool isOn;
    public bool tieneLampara;


    [Header("Porcentaje de pila")]
    public float bateria = 100f;
    public bool siBateria;

    [Header("Uso de Pila")]
    public float usoBateria = 0.01f;
    public bool puedeRecargar;

    [Header("UI")]
    [SerializeField] private Image CargaLampara;
    private float BateriaMaxima = 100f;
    public Image ImagenPilas;
    public Sprite[] PilasRecolectadas;
    [SerializeField] private GameObject UILamparaCanvas;
    [SerializeField] private GameObject TextoUso;
    public bool TextoUsoActivado;
    public bool TextoPuedeRecargar;

    [Header("SFX")]
    [SerializeField] GameObject LamparaTuOfSfx;
    [SerializeField] GameObject LamparaRSfx;



    public List<int> inventarioPilas = new List<int>();
    GameObject _lampara;
    Pila pila;
    FP_Controller player;
    private EnemigoPrincipal enemigoP;
    private EnemyState enemigoAraña;
    public Transform PlayerTr;


    void Start()
    {
        _lampara = GameObject.Find("Lampara");
        pila = GetComponent<Pila>();
        player = GetComponent<FP_Controller>();
        enemigoAraña = GetComponent<EnemyState>();
        UILamparaCanvas.SetActive(false);

        //CargaSlider.value = bateria;

        CargaLampara.fillAmount = bateria / BateriaMaxima;

        TextoUso.SetActive(false);
        TextoUsoActivado = false;
        TextoPuedeRecargar = false;

    }

    // Update is called once per frame
    void Update()
    {
        ApagarLampara();
        RecargarPila();
        AgregarPilaInventario();
        //CargaSlider.value = bateria;
        CargaLampara.fillAmount = bateria / BateriaMaxima;

        PilasRecolectadasSprite();

        textoUsoL();

    }

    private void FixedUpdate()
    {
        StartCoroutine(RestaBateria());
    }

    public void ApagarLampara()
    {
        if (tieneLampara == true)
        {

            if (Input.GetMouseButtonDown(1))
            {


                if (isOn == true)
                {
                    isOn = false;
                    Destroy(Instantiate(LamparaTuOfSfx, PlayerTr.position, Quaternion.identity), 1f);
                }
                else
                {
                    if (siBateria == true)
                        isOn = true;
                    Destroy(Instantiate(LamparaTuOfSfx, PlayerTr.position, Quaternion.identity), 1f);

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

            UILamparaCanvas.SetActive(true);
        }

    }


    IEnumerator RestaBateria()
    {
        if (isOn == true)
        {
            yield return new WaitForSeconds(0.1f);
            bateria = bateria - usoBateria;

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
        if (other.gameObject.tag == "Enemy" && bateria >= 6 && isOn == true)
        {
            bateria = bateria - 5f;


        }

        if (other.gameObject.tag == "EnemigoPrincipal" && bateria >= 6 && isOn == true)
        {
            bateria = bateria - 5f;
        }

        if (other.gameObject == player.UIMapa && Input.GetKey(KeyCode.E))
        {
            player.UIMapa.SetActive(false);
            player.recogioMapa = true;
        }
        if (other.gameObject.tag == "EnemyTutorial" && bateria >= 6 && isOn == true)
        {
            bateria = bateria - 5f;


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

            if (inventarioPilas.Count > 0)
            {
                bateria = bateria + 100f;
                inventarioPilas.Remove(1);
                siBateria = true;
                Destroy(Instantiate(LamparaRSfx, PlayerTr.position, Quaternion.identity), 1f);

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


    void PilasRecolectadasSprite()
    {
        if (inventarioPilas.Count == 0)
        {
            ImagenPilas.sprite = PilasRecolectadas[0];

        }

        if (inventarioPilas.Count == 1)
        {
            ImagenPilas.sprite = PilasRecolectadas[1];
        }

        if (inventarioPilas.Count == 2)
        {
            ImagenPilas.sprite = PilasRecolectadas[2];
        }
        if (inventarioPilas.Count == 3)
        {
            ImagenPilas.sprite = PilasRecolectadas[3];
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Escondite" && isOn) player.Escondido = false;
    }

    void textoUsoL()
    {
        if (tieneLampara == true && TextoUsoActivado == false)
        {
            TextoUso.SetActive(true);
            TextoUsoActivado = true;
        }
        if (TextoUsoActivado == true && Input.GetMouseButtonDown(1))
        {
            TextoUso.SetActive(false);
            TextoPuedeRecargar = true;
        }
    }
}
