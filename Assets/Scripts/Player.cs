using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float inputH;
    private float inputV;
    private bool moviendo;
    private Vector3 puntoDestino;
    private Vector3 ultimoInput;
    private Vector3 puntoInteraccion;
    private Collider2D colliderDelante;
    private Animator anim;
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float radioInteraccion;

    public static Player Instance;

    private bool interactuando;

    public bool Interactuando { get => interactuando; set => this.interactuando = value; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputV==0)
        {
            inputH = Input.GetAxisRaw("Horizontal");
        }
        if (inputH == 0)
        {
            inputV = Input.GetAxisRaw("Vertical");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
             LanzarInteraccion();
        }

        if (!interactuando && !moviendo&&(inputH!=0||inputV!=0))
        {
            anim.SetBool("IsWalk", true);
            anim.SetFloat("InputH",inputH);
            anim.SetFloat("InputV",inputV);
            ultimoInput = new Vector3(inputH,inputV,0);
            puntoDestino = transform.position + ultimoInput;
            puntoInteraccion = puntoDestino;

            colliderDelante = LanzarCheck();
            if (!colliderDelante)
            {
                StartCoroutine(Move());
            }
        }
        else if (inputH == 0 && inputV == 0)
        {
            anim.SetBool("IsWalk", false);
        }

        
    }

    private void LanzarInteraccion()
    {
        colliderDelante = LanzarCheck();
        if (colliderDelante)
        {
            if (colliderDelante.gameObject.CompareTag("NPC"))
            {
                NPC npcScript = colliderDelante.gameObject.GetComponent<NPC>();
                npcScript.Interactuar();
            }
        }
    }

    IEnumerator Move()
    {
        moviendo = true;
        while (transform.position!=puntoDestino)
        {
            transform.position= Vector3.MoveTowards(transform.position, puntoDestino, velocidadMovimiento * Time.deltaTime);
            yield return null;
        }
        puntoInteraccion = transform.position + ultimoInput;
        moviendo = false;
    }

    private Collider2D LanzarCheck()
    {
        return Physics2D.OverlapCircle(puntoInteraccion,radioInteraccion);
    }
}
