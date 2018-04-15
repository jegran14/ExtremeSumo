using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushState : ICharacterState {
    private PlayerController thePlayer;
    private Vector3 movementInput;

    public PushState (PlayerController player)
    {
        thePlayer = player;
    }


    public void FixedUpdateState()
    {
        
    }

    public void UpdateState()
    {
        
    }

    public void Push()
    {
        float vertical = Input.GetAxis("Vertical" + thePlayer.player);
        float Horizontal = Input.GetAxis("Vertical" + thePlayer.player);
        movementInput = new Vector3(Horizontal, 0.2f, vertical);
        Debug.Log("Im pushig");
        thePlayer.Pushed(movementInput.normalized, thePlayer.pushForce);
    }
}
