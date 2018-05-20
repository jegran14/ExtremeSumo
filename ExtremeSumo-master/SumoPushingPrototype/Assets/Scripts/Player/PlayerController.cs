using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharactersBase {
    //Player Number
    [Header("Propiedades relacionadas con el jugador")][Space]
    [Tooltip("Numero de jugador")]
    public int player;
   /* [Tooltip("Numero de input asignado")]
    public int inputNumber;*/

    [Space][Header("Propiedades relacionadas con el movimiento del personaje")][Space]
    //Movement paramenters
    [Range(1, 20)]
    [Tooltip("Velocidad de movimiento del personaje")]
    public float movementSpeed = 10f;
    [Tooltip("Velocidad de rotacion del personaje")]
    public float turnSpeed = 20f;
    [Tooltip("Fuerza de rozamiento aplicada al movimiento y las fuerzas")]
    [Range(0.3f, 3f)]
    public float dragForce = 10f;

    [Space][Header("Propiedades relacionadas con el empuje del personaje")][Space]
    //Push parameters
    [Tooltip("Fuerza con la que el personaje se empuja a si mismo y a otros personajes")]
    [Range(5f, 30f)]
    public float pushForce = 10f;
    [Tooltip("Numero de empujes maximo que el personaje puede hacer dentro de una ventana de tiempo")]
    public int numberOfPushes = 2;
    [Tooltip("Tiempo durante el que el personaje esta empujando a otros")]
    public float pushingTime = 1f;
    [Tooltip("Radio del area de empuje")]
    public float pushRadius = 1f;
    private Vector3 appliedForces;

    //Character States
    [HideInInspector] public NormalState normalState;
    private ICharacterState currentState; //The current state the character is on

    //Collision layers
    [HideInInspector]
    public int pushableLayer;
    [HideInInspector]
    public int groundLayer;

    //Character components
    [HideInInspector]
    public Rigidbody rb;
    private Animator anim;
    public ParticleSystem particles;
    

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        //particles = GetComponentInChildren<ParticleSystem>();

        normalState = new NormalState(this);
        currentState = normalState;

        pushableLayer = LayerMask.GetMask("Pushable");
        groundLayer = LayerMask.GetMask("Ground");

        appliedForces = new Vector3(0f, 0f, 0f);
    }
	
	// Update is called once per frame
	void Update () {
        if (inputNumber > 0)
            currentState.UpdateState();
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdateState();
    }

    public void Move(Vector3 movement)
    {
        rb.MovePosition(rb.position + movement);
        //Vector3 newMove;

        if (movement != Vector3.zero)
        {     
            anim.SetBool("Walking", true);
            /*newMove = new Vector3(movement.x, rb.velocity.y, movement.z);
            rb.velocity = newMove + appliedForces;
            //particles.Play();*/
        }     
        else
        {
            anim.SetBool("Walking", false);
           /* newMove = rb.velocity - (new Vector3(rb.velocity.x, 0f, rb.velocity.z) + appliedForces) * Time.deltaTime * dragForce;
            rb.velocity = newMove;*/
        }

        //appliedForces -= appliedForces * Time.deltaTime * dragForce;
    }

    public void Turn(Quaternion rot)
    {
        //Quaternion rotation = Quaternion.LerpUnclamped(rot, rb.rotation, turnSpeed*Time.deltaTime);
        rb.MoveRotation(rot);
    }

    public void ToNormal()
    {
        currentState = normalState;
    }

    public void Pushed(Vector3 direction, float force)
    {
        
       Vector3 dir;
        if (direction != Vector3.zero)
            dir = direction;
        else
            dir = transform.forward;

        rb.AddForce(transform.forward * force, ForceMode.Impulse);

        //appliedForces = direction * force;
    }
}
