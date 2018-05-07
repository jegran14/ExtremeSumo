﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalState : ICharacterState
{
    private PlayerController thePlayer;
    private Vector3 movementInput;
    private bool pushing;
    private int pushCounter;
    private float pushingTimer;
    private Vector3 pushDirection;

    public NormalState(PlayerController player)
    {
        thePlayer = player;
        pushing = false;
        pushCounter = 0;
        pushingTimer = 0;
    }

    public void FixedUpdateState()
    {
        Move();
        Turn();

        if (pushing)
            Pushing();
    }

    public void UpdateState()
    {
        float horizontal = Input.GetAxis("Horizontal" + thePlayer.player);
        float vertical = Input.GetAxis("Vertical" + thePlayer.player);

        movementInput = new Vector3(horizontal, 0.0f, vertical);
        movementInput = movementInput.normalized;

        if (Input.GetButtonDown("Jump" + thePlayer.player) && pushCounter < thePlayer.numberOfPushes)
        {
            Push();
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

    private void Push()
    {
        thePlayer.Pushed(movementInput, thePlayer.pushForce);
        pushCounter++;

        if (!pushing)
        {
            if (movementInput != Vector3.zero)
                pushDirection = movementInput;
            else
                pushDirection = thePlayer.transform.forward;

            pushing = true;
            Pushing();
        }
    }

    public void Pushing()
    {
        if (pushingTimer <= thePlayer.pushingTime)
        {
            Collider[] colliders = Physics.OverlapSphere(thePlayer.transform.position, thePlayer.pushRadius, thePlayer.pushableLayer);

            for(int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].tag == thePlayer.tag) { continue; }
<<<<<<< HEAD
                Debug.Log(colliders[i].tag);
=======
                
               /* PlayerController targetPlayer = colliders[i].GetComponent<PlayerController>();

                if (!targetPlayer)
                {
                    Rigidbody rigidbody = colliders[i].GetComponent<Rigidbody>();

                    if (!target) continue;
                    target.AddExplosionForce(thePlayer.pushForce / 1.1f, thePlayer.transform.position, thePlayer.pushRadius * 1.5f, 0f);
                    continue;
                }
                Debug.Log("Yhe");
                Vector3 dir = targetPlayer.transform.position - thePlayer.transform.position;
                dir = dir.normalized;
                targetPlayer.Pushed(dir, thePlayer.pushForce);*/
>>>>>>> 76dd5f64ad8d533618fc283129160029051879a8

                Rigidbody target = colliders[i].GetComponent<Rigidbody>();

                if (!target) continue;
<<<<<<< HEAD

                target.AddExplosionForce(thePlayer.pushForce * 10, thePlayer.transform.position, thePlayer.pushRadius * 1.5f);

=======
                Debug.Log("Yeh");
                target.AddExplosionForce(thePlayer.pushForce, thePlayer.transform.position, thePlayer.pushRadius * 1.5f, 0f);
>>>>>>> 76dd5f64ad8d533618fc283129160029051879a8
            }

            pushingTimer += Time.deltaTime;
            return;
        }

        pushCounter = 0;
        pushingTimer = 0f;
        pushing = false;
    }
}
