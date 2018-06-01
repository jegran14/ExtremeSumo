using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMovement : CharactersBase {

    private UnityEngine.AI.NavMeshAgent nav;
    private AIDirector director;
    private Transform thePlayer;

   // private GameObject thePlayer;
    private Vector3 position;
    private float distance;
    private Animator anim;

    private bool pushing;
    private int pushCounter;
    private float pushingTimer;
    private Vector3 pushDirection;

    RaycastHit hit;


    //Push parameters
    public float pushForce = 10f;
    public int numberOfPushes = 2;
    public float pushingTime = 1f;
    public float pushRadius = 1f;

    //Collider Layer
    private int pushableLayer;




    // Use this for initialization
    void Start() {
        director = FindObjectOfType<AIDirector>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        nav.enabled = true;
        anim = GetComponentInChildren<Animator>();

        pushableLayer = LayerMask.GetMask("Pushable");

        pushing = false;
        pushCounter = 0;
        pushingTimer = 0;
    }

    private void OnEnable()
    {
        if(nav != null)
            nav.enabled = true;

        pushing = false;
        pushCounter = 0;
        pushingTimer = 0;
    }

    // Update is called once per frame
    void Update() {

        
        position = gameObject.transform.position;
        distance = Mathf.Infinity;

        for (int i = 0; i < director.targets.Length; i++)
        {
            if(director.targets[i] != this.transform && director.targets[i].gameObject.activeInHierarchy)
            {
                Vector3 diference = (position - director.targets[i].position);
                float currentDistance = diference.sqrMagnitude;
                if (currentDistance < distance)
                {
                    thePlayer = director.targets[i];
                    distance = currentDistance;
                }
            }
        }

        if (distance > 2f)
        {
            anim.SetBool("Walking", true);
        }
        else
        {
            Push();
        }

        if (!Physics.Raycast(transform.position,
            transform.TransformDirection(Vector3.down),
            out hit, Mathf.Infinity, LayerMask.GetMask("Ground")) || !IsTargetInArena())
        {
            nav.enabled = false;
            anim.SetBool("Walking", false);
        }
        else
        {
            anim.SetBool("Walking", true);
            nav.enabled = true;
        }
        if (nav.isActiveAndEnabled)
        {
            nav.SetDestination(thePlayer.transform.position);
        }
    }

    private void FixedUpdate()
    {
        if (pushing){
           Pushing();
        }
    }

    private bool IsTargetInArena()
    {
        return Vector3.Distance(thePlayer.position, Vector3.zero) < 9.9f;
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
                if (colliders[i].tag == transform.tag) {  continue; }

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

