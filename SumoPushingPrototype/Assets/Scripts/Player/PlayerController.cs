using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float movementSpeed = 10f;
    public float turnSpeed = 300f;

    [HideInInspector] public NormalState normalState;

    private Rigidbody rb;
    private ICharacterState currentState;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();

        normalState = new NormalState(this, rb);
        currentState = normalState;
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
        rb.MoveRotation(rb.rotation * rot);
    }
}
