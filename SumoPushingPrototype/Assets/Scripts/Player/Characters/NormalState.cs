using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalState : ICharacterState
{
    private PlayerController thePlayer;
    private Vector3 movementInput;

    public NormalState(PlayerController player)
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
        float horizontal = Input.GetAxis("Horizontal" + thePlayer.player);
        float vertical = Input.GetAxis("Vertical" + thePlayer.player);

        movementInput = new Vector3(horizontal, 0.0f, vertical);
        movementInput = movementInput.normalized;

        if (Input.GetButtonDown("Jump" + thePlayer.player))
        {
            thePlayer.Push();
            return;
        }
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
            //Quaternion rotation = Quaternion.SlerpUnclamped(thePlayer.transform.rotation, Quaternion.LookRotation(movementInput), thePlayer.turnSpeed * Time.deltaTime);
            // thePlayer.Turn(rotation);
            thePlayer.Turn(Quaternion.LookRotation(movementInput));
        }
    }

    private void Falling(bool falling)
    {

    }
}
