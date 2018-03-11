using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterPushing : IPushState {
    private Rigidbody rb;
    private PlayerPush pusher;
    private float distanceMoved;
    private float distanceToMove;

    public AfterPushing(Rigidbody rb, PlayerPush pusher, float distanceToMove)
    {
        this.rb = rb;
        this.pusher = pusher;
        this.distanceToMove = distanceToMove;
        this.distanceMoved = 0f;
    }

    public void FixedUpdateState()
    {
        distanceMoved += pusher.backSpeed * Time.deltaTime;
        Vector3 movement = -pusher.transform.forward * pusher.backSpeed * Time.deltaTime;
        pusher.transform.position = pusher.transform.position + movement;

        if (distanceMoved >= distanceToMove)
            FinishMovement();
    }

    public void UpdateState()
    {
        //Does nothing on the update
    }

    private void FinishMovement()
    {
        pusher.currentState = new IddleState(rb, pusher);
    }
}
