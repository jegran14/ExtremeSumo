using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalState : ICharacterState
{
    private PlayerController thePlayer;
    private Vector3 movementInput;

    public NormalState(PlayerController player, Rigidbody rb)
    {
        thePlayer = player;
    }

    public void FixedUpdateState()
    {
        Move();
        Turn();
    }

    public void UpdateState()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        movementInput = new Vector3(horizontal, 0.0f, vertical);
        movementInput = movementInput.normalized;
    }

    private void Move()
    {
        Vector3 movement = movementInput * thePlayer.movementSpeed * Time.deltaTime;
        thePlayer.Move(movement);

        
    }

    private void Turn()
    {
        if(movementInput != Vector3.zero)
        {
            Quaternion rotation = Quaternion.SlerpUnclamped(thePlayer.transform.rotation, Quaternion.LookRotation(movementInput), thePlayer.turnSpeed * Time.deltaTime);
            thePlayer.Turn(rotation);
        }
    }

    private void Falling(bool falling)
    {

    }
}
