using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMovement : MonoBehaviour {

    public GameObject[] players;

    private UnityEngine.AI.NavMeshAgent nav;
    private Transform player;
    private Vector3 position;
    private float distance;
    private Animator anim;

    private bool pushing;
    private int pushCounter;
    private float pushingTimer;
    private Vector3 pushDirection;

    RaycastHit raycast;


    //Push parameters
    public float pushForce = 10f;
    public int numberOfPushes = 2;
    public float pushingTime = 1f;
    public float pushRadius = 1f;

    //Collider Layer
    private int pushableLayer;




    // Use this for initialization
    void Awake() {

        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        nav.enabled = true;
        anim = GetComponentInChildren<Animator>();

        pushableLayer = LayerMask.GetMask("Pushable");

        pushing = false;
        pushCounter = 0;
        pushingTimer = 0;
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            nav.enabled = false;
        }
    }



    // Update is called once per frame
    void Update() {

        
        position = gameObject.transform.position;
        distance = Mathf.Infinity;

        for (int i = 0; i < players.Length; i++)
        {
            var diference = (position - players[i].transform.position);
            var currentDistance = diference.sqrMagnitude;
            if (currentDistance < distance)
            {
                player = players[i].transform;
                distance = currentDistance;
            }
        }

        if (distance > 2f)
        {
            anim.SetBool("Walking", true);
        }
        else
        {
            anim.SetBool("Walking", false);
            Push();
        }
        nav.SetDestination(player.position);
    }

    private void FixedUpdate()
    {
        Debug.Log("Push");
        if (pushing){
           Pushing();
        }
    }
    private void Push()
    {
        pushCounter++;

        if (!pushing)
        {
            pushDirection = transform.forward;
            pushing = true;
            Pushing();
        }
    }

    public void Pushing()
    {
        if (pushingTimer <= pushingTime)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, pushRadius, pushableLayer);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].tag == transform.tag) { Debug.Log("Yoh!"); continue; }

                Rigidbody target = colliders[i].GetComponent<Rigidbody>();

                if (!target) continue;

                target.AddExplosionForce(pushForce * 10, transform.position, pushRadius * 1.5f);
            }

            pushingTimer += Time.deltaTime;
            return;
        }

        pushCounter = 0;
        pushingTimer = 0f;
        pushing = false;
    }
}

