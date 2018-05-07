using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    //Player Number
    public int player;
    public int inputNumber;

    //Movement paramenters
<<<<<<< HEAD
=======
    [Range(1, 20)]
    [Tooltip("Velocidad de movimiento del personaje")]
>>>>>>> 76dd5f64ad8d533618fc283129160029051879a8
    public float movementSpeed = 10f;
    public float turnSpeed = 20f;

    //Push parameters
    public float pushForce = 10f;
    public int numberOfPushes = 2;
    public float pushingTime = 1f;
    public float pushRadius = 1f;

    //Character States
    [HideInInspector] public NormalState normalState;
    private ICharacterState currentState; //The current state the character is on

    //Collision layers
    [HideInInspector]
    public int pushableLayer;
    [HideInInspector]
    public int groundLayer;

    //Character components
    private Rigidbody rb;
    private Animator anim;
    private ParticleSystem particles;
    

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
       // particles = GetComponentInChildren<ParticleSystem>();

        normalState = new NormalState(this);
        currentState = normalState;

        pushableLayer = LayerMask.GetMask("Pushable");
        groundLayer = LayerMask.GetMask("Ground");
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
<<<<<<< HEAD

        Debug.Log(movement);
=======
        //Vector3 newMove;
>>>>>>> 76dd5f64ad8d533618fc283129160029051879a8

        if (movement != Vector3.zero)
        {
            anim.SetBool("Walking", true);
<<<<<<< HEAD
           // particles.Play();
=======
            /*newMove = new Vector3(movement.x, rb.velocity.y, movement.z);
            rb.velocity = newMove + appliedForces;
            //particles.Play();*/
>>>>>>> 76dd5f64ad8d533618fc283129160029051879a8
        }     
        else
        {
            anim.SetBool("Walking", false);
<<<<<<< HEAD
        }
=======
           /* newMove = rb.velocity - (new Vector3(rb.velocity.x, 0f, rb.velocity.z) + appliedForces) * Time.deltaTime * dragForce;
            rb.velocity = newMove;*/
        }

        //appliedForces -= appliedForces * Time.deltaTime * dragForce;
>>>>>>> 76dd5f64ad8d533618fc283129160029051879a8
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
<<<<<<< HEAD
        Vector3 dir;
=======
        
       Vector3 dir;
>>>>>>> 76dd5f64ad8d533618fc283129160029051879a8
        if (direction != Vector3.zero)
            dir = direction;
        else
            dir = transform.forward;

        rb.AddForce(transform.forward * force, ForceMode.Impulse);
<<<<<<< HEAD
=======

        //appliedForces = direction * force;
>>>>>>> 76dd5f64ad8d533618fc283129160029051879a8
    }
}
