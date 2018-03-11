using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushingState : IPushState{
    private Rigidbody rb;
    private PlayerPush pusher;
    private float distanceMoved;
    

    public PushingState(Rigidbody rb, PlayerPush pusher)
    {
        this.rb = rb;
        this.pusher = pusher;
        distanceMoved = 0f;
    }

    public void FixedUpdateState()
    {
        Push();
    }

    public void UpdateState()
    {
        //Does noting on the update state
    }

    private void Push()
    {
        distanceMoved += pusher.pushSpeed * Time.deltaTime;
        Vector3 movement = pusher.transform.forward * pusher.pushSpeed * Time.deltaTime;
        pusher.transform.position = pusher.transform.position + movement;

        if (distanceMoved >= pusher.pushRange)
            FinishPushing();
        
    }

    public void FinishPushing()
    {
        pusher.currentState = new AfterPushing(rb, pusher, distanceMoved);
    }
}
