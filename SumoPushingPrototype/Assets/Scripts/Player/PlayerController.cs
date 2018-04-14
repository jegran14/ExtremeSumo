using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    //Movement paramenters
    public float movementSpeed = 10f;
    public float turnSpeed = 300f;
    //Character States
    [HideInInspector] public NormalState normalState;
    private ICharacterState currentState; //The current state the character is on
    //Collision layers
    private int pushableLayer;
    private int groundLayer;
    //Character components
    private Rigidbody rb;
    private Animator anim;
    

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();

        normalState = new NormalState(this, rb);
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
    }

    public void Turn(Quaternion rot)
    {
        //Quaternion rotation = Quaternion.LerpUnclamped(rot, rb.rotation, turnSpeed*Time.deltaTime);
        rb.MoveRotation(rot);
    }
}
