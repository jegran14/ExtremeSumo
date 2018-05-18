using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float speed = 12f;
    public float turnSpeed = 180f;

    private Rigidbody rb;
    private Vector3 movementInputValue;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        rb.isKinematic = false;
        movementInputValue = new Vector3();
    }

    private void OnDisable()
    {
        rb.isKinematic = true;
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update () {
        movementInputValue = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
	}

    private void FixedUpdate()
    {
        Move();
        Turn();
    }

    private void Move()
    {
        Vector3 movement = movementInputValue * speed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
    }

    private void Turn()
    {
        float dot = Vector3.Dot(transform.right, movementInputValue);
        float turn = dot * turnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }
}
