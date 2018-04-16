using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    //Player Number
    public int player;
    public int inputNumber;

    //Movement paramenters
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
        currentState.UpdateState();
	}

    private void FixedUpdate()
    {
        currentState.FixedUpdateState();
    }

    public void Move(Vector3 movement)
    {
        rb.MovePosition(rb.position + movement);

        if (movement != Vector3.zero)
        {
            anim.SetBool("Walking", true);
           // particles.Play();
        }     
        else
        {
            anim.SetBool("Walking", false);
        }
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
    }
}
