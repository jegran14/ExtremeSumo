using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMovement : MonoBehaviour {

    public GameObject [] players;

    private UnityEngine.AI.NavMeshAgent nav;
    private Transform player;
    private Vector3 position;
    private float distance;
    private Animator anim;


	// Use this for initialization
	void Awake () {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

        position = gameObject.transform.position;
        distance = Mathf.Infinity;

	    for (int i = 0; i < players.Length; i++)
        {
            var diference = (position - players[i].transform.position);
            var currentDistance = diference.sqrMagnitude;
            if(currentDistance < distance)
            {
                player = players[i].transform;
                distance = currentDistance;
            }
        }
        
        if(distance > 0.9)
        {
            anim.SetBool("Walking", true);
        }
        else
        {
            anim.SetBool("Walking", false);

        }
        nav.SetDestination(player.position);
	}
}
