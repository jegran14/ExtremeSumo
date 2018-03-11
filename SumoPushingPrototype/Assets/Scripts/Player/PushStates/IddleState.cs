using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IddleState : IPushState{
    private Rigidbody rb;
    private PlayerPush pusher;

    public IddleState(Rigidbody rb, PlayerPush pusher)
    {
        this.rb = rb;
        this.pusher = pusher;
    }

    public void FixedUpdateState()
    {
        //Does nothing in the fixed update calls
    }

    public void UpdateState()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Push();
    }

    private void Push()
    {
        pusher.currentState = new PushingState(rb, pusher);
    }
}
