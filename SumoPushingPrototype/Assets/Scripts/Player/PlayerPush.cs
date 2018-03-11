using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPush : MonoBehaviour {
    public float pushSpeed = 10f;
    public float backSpeed = 5f;
    public float pushForece = 200f;
    public float pushRange = 1f;

    [HideInInspector]
    public IPushState currentState;

    private Rigidbody rb;
    private LayerMask layer;


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        currentState = new IddleState(rb, this);
        layer = LayerMask.GetMask("Pushable");
    }
	
	// Update is called once per frame
	void Update () {
        currentState.UpdateState();
	}

    private void FixedUpdate()
    {
        currentState.FixedUpdateState();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(currentState is PushingState && other.tag == "Pushable")
        {
            Debug.Log("Yoh");
            bool hit = false;
            Collider[] colliders = Physics.OverlapSphere(transform.position, 1f);

            for (int i = 0; i < colliders.Length; i++)
            {
                Rigidbody target = colliders[i].GetComponent<Rigidbody>();

                if (!target)
                    continue;

                target.AddForce(transform.forward * pushForece, ForceMode.Impulse);
                hit = true;
            }

            if(hit)
            {
                PushingState state = currentState as PushingState;
                state.FinishPushing();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (currentState is PushingState && other.tag == "Pushable")
        {
            Debug.Log("Yoh");
            bool hit = false;
            Collider[] colliders = Physics.OverlapSphere(transform.position, 1f, layer);

            for (int i = 0; i < colliders.Length; i++)
            {
                Rigidbody target = colliders[i].GetComponent<Rigidbody>();

                if (!target)
                    continue;

                target.AddForce(transform.forward * pushForece, ForceMode.Impulse);
                hit = true;
            }

            if (hit)
            {
                PushingState state = currentState as PushingState;
                state.FinishPushing();
            }
        }
    }
}
