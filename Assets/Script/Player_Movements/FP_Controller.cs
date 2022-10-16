using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FP_Controller : MonoBehaviour
{

    //Para obtener el componente character controller del Player
    CharacterController characterController;

    //Referencia a otros scripts
    Contenedor_de_Llaves llaveContenedor;

    [Header("Health")]
    public float playerHealth = 2;

    [Header("OPCIONES DE PERSONAJE")]
    //Variables que ayudan al movimiento
    public float walkSpeed = 6f;
    public float runSpeed = 9f;
    public float jumpSpeed = 8f;
    //Esta gravedad se puede cambiar a como queramos
    public float gravity = 20f;

    public bool cansado = false;

    [Header("Crouch")]
    public float restaCrouchSpeed = 2f;
    public bool isCrouching = false;
    private float defaultControllerHeight;


    [Header("Partes de Llave")]
    public int contadorLlaves;
    public string nombreLlave;
    public Image ImagenLLaves;
    public Sprite[] LlavesRecolectadas;

    [Header("Pila")]
    public bool seRecogioPila = false;
    public string nombreItem;
    private GameObject _pila;
    Pila pila;

    //Crear un vector que nos va ayudar a mover el personaje, poniendo todo en 0 para luego camniar valores
    private Vector3 move = Vector3.zero;

    public bool siRango = false;



    void Start()
    {
        characterController = GetComponent<CharacterController>();
        llaveContenedor = FindObjectOfType<Contenedor_de_Llaves>();
        pila = GetComponent<Pila>();
        
        defaultControllerHeight = characterController.height;

    }

    // Update is called once per frame
    void Update()
    {
        if (characterController.isGrounded)
        {
            //Haciendo fuerza al player para moverse con W S A D  y con las flechas
            move = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

            //Para correr
            if (Input.GetKey(KeyCode.LeftShift))
            {
                move = transform.TransformDirection(move) * runSpeed;
            }
            //caminar a velocidad normal
            else
            {
                //Sin esta linea el personaje siempre va hacia adelante sin importar a donde vea
                move = transform.TransformDirection(move) * walkSpeed;
            }

            //Para Saltar
            if (Input.GetKey(KeyCode.Space))
            {
                move.y = jumpSpeed;
            }
            //Crouch

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                Agacharse();
            }

            if (Input.GetKeyUp(KeyCode.LeftControl))
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
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {

            playerHealth--;
            Debug.Log("Ouch " + playerHealth);

            if (playerHealth == 0)
            {
                Object.Destroy(gameObject, .5f);
            }
        }
       


    }

    void Agacharse()
    {
        isCrouching = true;
        characterController.height = defaultControllerHeight - .5f;
        walkSpeed = walkSpeed - restaCrouchSpeed;
        Debug.Log("Se Agacho " + walkSpeed + "Altura del JugadorDetect " + characterController.height);

    }
    void Levantarse()
    {
        isCrouching = false;
        characterController.height = defaultControllerHeight;
        walkSpeed = walkSpeed + restaCrouchSpeed;
        Debug.Log("Se Paro" + walkSpeed + "Altura del JugadorDetect " + characterController.height);


    }

    //Recoleccion de Items
    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag.Contains("Llave"))
        {
            siRango = true;
            nombreLlave = other.gameObject.tag;

        }
        if (other.gameObject.tag == "Pila")
        {
            siRango = true;
            nombreItem = other.gameObject.tag;
            _pila = other.gameObject;
            RecogerPila();
        }
        


    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag.Contains("Llave"))
        {
            siRango = false;
           nombreLlave="NoRango";

        }
        if (other.gameObject.tag == "Pila")
        {
            siRango = false;
            nombreItem = "NoRango";
            
            
        }
       
       
    }

    public void RecogerPila()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (siRango == true && nombreItem.Contains("Pila"))
            {
                _pila.SetActive(false);
                seRecogioPila = true;
                StartCoroutine(ResetPila());
                
            }
            
        }
    }

    public void AbrirPuerta()
    {
        if (contadorLlaves == 4) Debug.Log("Puerta Abierta");

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
    }


}
