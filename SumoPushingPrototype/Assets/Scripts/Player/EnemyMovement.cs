using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    Transform player;
    NavMeshAgent nav;

	// Use this for initialization
	void Awake () {
        player = GameObject.FindGameObjectsWithTag("Player1").transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
