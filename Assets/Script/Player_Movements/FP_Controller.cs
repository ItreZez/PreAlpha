using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FP_Controller : MonoBehaviour
{

    //Para obtener el componente character controller del Player
    CharacterController characterController;

    //Referencia a otros scripts
    Contenedor_de_Llaves llaveContenedor;

    public EnemigoPrincipal enemigoP;

    Lampara lampara;


    [Header("Health")]
    public float playerHealth = 2;
    public Image panelHurt;
    public bool hit = false;
    public GameObject CanvasHurtI;

    [Header("OPCIONES DE PERSONAJE")]
    //Variables que ayudan al movimiento
    public float walkSpeed = 6f;
    public float runSpeed = 9f;
    public float jumpSpeed = 8f;
    //Esta gravedad se puede cambiar a como queramos
    public float velocidad = 0;
    public float gravity = 20f;
    public float stamina = 100;

    [Header("Crouch")]
    public float restaCrouchSpeed = 2f;
    public bool isCrouching = false;
    private float defaultControllerHeight;


    [Header("Partes de Llave")]
    public int contadorLlaves;
    public string nombreLlave;
    public Image ImagenLLaves;
    public Sprite[] LlavesRecolectadas;
    public GameObject yunque;
    public GameObject UIllave;


    [Header("SFX")]
    [SerializeField] private GameObject YunqueArmarSF; // ya
    [SerializeField] private GameObject RecogerSfx;
    [SerializeField] private GameObject CaminarSfx;
    [SerializeField] private GameObject OuchSfx;
    private bool caminarSfxSi;
    [SerializeField] private GameObject CorrerSfx;

    [SerializeField] private AudioSource CaminarSfxAS;
    [SerializeField] private AudioSource CorrerSfxAS;


    private bool correrSfxSi;



    public GameObject puerta;

    [Header("Pila")]
    public bool seRecogioPila = false;
    public string nombreItem;
    private GameObject _pila;
    public bool inventarioLleno;
    Pila pila;

    //Crear un vector que nos va ayudar a mover el personaje, poniendo todo en 0 para luego camniar valores
    private Vector3 move = Vector3.zero;

    public bool siRango = false;

    //UI Correr
    [SerializeField] private Image CargaRun;
    private float RunMaxima = 100f;

    [Header("Esconderse")]
    public bool Escondido = false;
    public Image ImagenEscondidos;
    public Sprite[] EscondidoSN;

    [Header("MAPA")]
    public bool recogioMapa;
    public GameObject UIMapa;



    void Start()
    {
        CanvasHurt();

        characterController = GetComponent<CharacterController>();
        llaveContenedor = FindObjectOfType<Contenedor_de_Llaves>();
        pila = GetComponent<Pila>();
        enemigoP = GetComponent<EnemigoPrincipal>();
        lampara = GetComponent<Lampara>();


        defaultControllerHeight = characterController.height;

        CargaRun.fillAmount = stamina / RunMaxima;

        recogioMapa = false;
        UIllave.SetActive(false);
        UIMapa.SetActive(false);

        CaminarSfxAS.Stop();
        CorrerSfxAS.Stop();

    }

    // Update is called once per frame
    void Update()
    {

        InstanciarYunque();
        velocidad = 0;
        ChecarInventario();



        CargaRun.fillAmount = stamina / RunMaxima;

        if (characterController.isGrounded)
        {
            //Haciendo fuerza al player para moverse con W S A D  y con las flechas
            move = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

            if (move.sqrMagnitude > 0)

                //Para correr
                if (Input.GetKey(KeyCode.LeftShift) && stamina >= 1)
                {

                    stamina = stamina - .45f;

                    if (stamina > 1)
                    {

                        move = transform.TransformDirection(move) * runSpeed;
                        CargaRun.fillAmount = stamina / RunMaxima;
                        velocidad = runSpeed;
                        SFXMovimiento();
                    }

                }
                //caminar a velocidad normal
                else
                {
                    Stamina();
                    //Sin esta linea el personaje siempre va hacia adelante sin importar a donde vea
                    move = transform.TransformDirection(move) * walkSpeed;
                    CargaRun.fillAmount = stamina / RunMaxima;
                    velocidad = walkSpeed;
                    SFXMovimiento();

                }

            //Para Saltar
            if (Input.GetKey(KeyCode.Space))
            {
                move.y = jumpSpeed;
            }
            //Crouch

            if (Input.GetKeyDown(KeyCode.Q))
            {
                Agacharse();
            }

            if (Input.GetKeyUp(KeyCode.Q))
            {

                Levantarse();


            }


        }

        //Para que la caida sea constante y no dePenda de los frames de cada compu
        move.y -= gravity * Time.deltaTime;

        //Limita al personaje por collisiones
        characterController.Move(move * Time.deltaTime);

        // move.x= Mathf.Lerp(move.x,0f,Time.deltaTime *20f);
        //move.y= Mathf.Lerp(move.y,0f,Time.deltaTime *20f);

        LLavesRecolectadasSprite();

        CanvasHurt();

        escondidoSN();
    }



    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy" && Escondido == false)
        {

            if (playerHealth > 0 && hit == false)
            {
                hit = true;
                StartCoroutine(DanoPlayer());

            }

            if (playerHealth == 0)
            {
                Object.Destroy(gameObject, .5f);
            }

        }
    }



    void Agacharse()
    {
        if (isCrouching == false)
        {
            isCrouching = true;
            characterController.height = defaultControllerHeight - 1f;
            walkSpeed = walkSpeed - restaCrouchSpeed;
            Debug.Log("Se Agacho " + walkSpeed + "Altura del JugadorDetect " + characterController.height);
        }

    }
    void Levantarse()
    {
        if (isCrouching == true)
        {
            isCrouching = false;
            characterController.height = defaultControllerHeight;
            walkSpeed = walkSpeed + restaCrouchSpeed;
            Debug.Log("Se Paro" + walkSpeed + "Altura del JugadorDetect " + characterController.height);
        }


    }



    //Recoleccion de Items
    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag.Contains("Llave"))
        {
            siRango = true;
            nombreLlave = other.gameObject.tag;


        }
        if (other.gameObject.tag == "Pila" && inventarioLleno == false)
        {
            siRango = true;
            nombreItem = other.gameObject.tag;
            _pila = other.gameObject;
            RecogerPila();

        }

        if (other.gameObject.tag == "Yunque")
        {
            if (Input.GetKeyDown(KeyCode.E) && contadorLlaves == 4)
            {
                contadorLlaves = 5;
                Debug.Log("Creaste Llave Chingona");

                //Audio Yunque
                Destroy(Instantiate(YunqueArmarSF, transform.position, Quaternion.identity), 1f);


            }
        }

        if (other.gameObject.tag == "Puerta")
        {

            if (Input.GetKeyDown(KeyCode.E) && contadorLlaves == 5)
            {
                puerta.SetActive(false);
                Debug.Log("Destruisteb sy a apsjla, ae Chingona");
                contadorLlaves = 6;
            }
        }
        if (other.gameObject.tag == "Puerta" && contadorLlaves == 6)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("Win");

        }

        if (other.gameObject.tag == "Escondite")
        {

            Escondido = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag.Contains("Llave"))
        {
            siRango = false;
            nombreLlave = "NoRango";

        }
        if (other.gameObject.tag == "Pila")
        {
            siRango = false;
            nombreItem = "NoRango";
        }

        if (other.gameObject.tag == "Escondite")
        {
            Escondido = false;
        }
    }

    public void RecogerPila()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (siRango == true && nombreItem.Contains("Pila") && lampara.tieneLampara == true)
            {
                _pila.SetActive(false);
                seRecogioPila = true;
                StartCoroutine(ResetPila());
                Destroy(Instantiate(RecogerSfx, transform.position, Quaternion.identity), 1f);

            }

        }
    }

    public void InstanciarYunque()
    {
        if (contadorLlaves == 4)
        {
            yunque.SetActive(true);

        }
    }

    IEnumerator ResetPila()
    {
        yield return new WaitForEndOfFrame();
        seRecogioPila = false;

    }

    void LLavesRecolectadasSprite()
    {
        if (contadorLlaves == 0)
        {
            ImagenLLaves.sprite = LlavesRecolectadas[0];
        }

        if (contadorLlaves == 1)
        {
            ImagenLLaves.sprite = LlavesRecolectadas[1];
        }

        if (contadorLlaves == 2)
        {
            ImagenLLaves.sprite = LlavesRecolectadas[2];
        }

        if (contadorLlaves == 3)
        {
            ImagenLLaves.sprite = LlavesRecolectadas[3];
        }

        if (contadorLlaves == 4)
        {
            ImagenLLaves.sprite = LlavesRecolectadas[4];
        }

        if (contadorLlaves == 5)
        {
            ImagenLLaves.sprite = LlavesRecolectadas[5];
        }
    }

    private void escondidoSN()
    {
        if (Escondido == false)
        {
            ImagenEscondidos.sprite = EscondidoSN[0];
        }
        if (Escondido == true)
        {
            ImagenEscondidos.sprite = EscondidoSN[1];
        }
    }

    public IEnumerator Regenerarse()
    {
        yield return new WaitForSeconds(10);
        playerHealth = 2;

    }

    void CanvasHurt()
    {
        if (playerHealth == 2)
        {
            //panelHurt.enabled = false;
            CanvasHurtI.SetActive(false);
        }
        if (playerHealth == 1)
        {
            //panelHurt.enabled = true;
            CanvasHurtI.SetActive(true);

        }
        if (playerHealth == 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("GameOver");
        }
    }

    public IEnumerator DanoPlayer()
    {
        Destroy(Instantiate(OuchSfx, transform.position, Quaternion.identity), 1f);
        playerHealth = playerHealth - 1;
        Debug.Log("Ouch " + playerHealth);
        yield return new WaitForSeconds(3);
        hit = false;
        if (playerHealth == 1) StartCoroutine(Regenerarse());

    }

    private void Stamina()
    {
        if (stamina < 98.9f)
            stamina = stamina + .3f;

    }

    public void SFXMovimiento()
    {
        GameObject caminar = CaminarSfx;
        GameObject correr = CorrerSfx;

        if (velocidad == 4.5 && caminarSfxSi == false)
        {

            Destroy(Instantiate(caminar, transform.position, Quaternion.identity), .92f);
            caminarSfxSi = true;
            StartCoroutine(cambiarBoolCaminar());
            if (velocidad == 0)
            {
                Destroy(caminar);

            }
        }
        if (velocidad == 7 && correrSfxSi == false)
        {
            Destroy(Instantiate(correr, transform.position, Quaternion.identity),.9f);
            correrSfxSi = true;
            StartCoroutine(cambiarBoolCorrer());
            if (velocidad == 0)
            {
                Destroy(correr);

            }

        }


    }
    IEnumerator cambiarBoolCaminar()
    {
        yield return new WaitForSeconds(1);
        caminarSfxSi = false;

    }
     IEnumerator cambiarBoolCorrer()
    {
        yield return new WaitForSeconds(.9f);
        correrSfxSi = false;

    }

    public void ChecarInventario()
    {
        if(FindObjectOfType<Lampara>().inventarioPilas.Count == 3)
        {
            inventarioLleno = true;

        }
        else
        {
            inventarioLleno = false;
        }

    }






}




