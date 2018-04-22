using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyMovement : MonoBehaviour {

    GameObject [] players;
    NavMeshAgent nav;
    Transform player;
    Vector3 position;
    float distance;


	// Use this for initialization
	void Awake () {
        players = GameObject.FindGameObjectsWithTag("Player1");

        nav = GetComponent<NavMeshAgent>();
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

        nav.SetDestination(player.position);
	}
}
