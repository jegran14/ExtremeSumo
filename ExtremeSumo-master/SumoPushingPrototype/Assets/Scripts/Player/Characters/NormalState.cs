using System;
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
        float horizontal = Input.GetAxis("Horizontal" + thePlayer.inputNumber);
        float vertical = Input.GetAxis("Vertical" + thePlayer.inputNumber);

        movementInput = new Vector3(horizontal, 0.0f, vertical);
        movementInput = movementInput.normalized;

        if (Input.GetButtonDown("Jump" + thePlayer.inputNumber) && pushCounter < thePlayer.numberOfPushes)
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
            Collider[] colliders = Physics.OverlapBox(thePlayer.transform.position, new Vector3(2.5f, 1f, 1.5f), thePlayer.transform.rotation, thePlayer.pushableLayer);
            // Collider[] colliders = Physics.OverlapSphere(thePlayer.transform.position, thePlayer.pushRadius, thePlayer.pushableLayer);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].tag == thePlayer.tag) { continue; }
                
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

                Rigidbody target = colliders[i].GetComponent<Rigidbody>();
                

                if (!target) continue;

                thePlayer.particles.transform.position = new Vector3(target.transform.position.x, (target.transform.position.y+0.5f), target.transform.position.z);
                if (!thePlayer.particles.isPlaying)
                {   
                    thePlayer.particles.Play();
                }
                
                Debug.Log("Yeh");
                target.AddExplosionForce(thePlayer.pushForce, thePlayer.transform.position, thePlayer.pushRadius * 1.5f, 0f);
            }

            pushingTimer += Time.deltaTime;
            return;
        }

        pushCounter = 0;
        pushingTimer = 0f;
        pushing = false;
    }
}
