﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModoHulk : MonoBehaviour {

    PlayerController player;
    public float time;
    public float force;
    public Vector3 scale;
    // Use this for initialization
    public void StartPowerUp (PlayerController player) {
        //Cambiar condiciones
        Debug.Log("ModoHulkActivo");
        this.player = player;
        player.transform.localScale += scale;
        player.pushForce += force;
        StartCoroutine("CuentaAtras");
        //CuentaAtras();

    }
	
	// Update is called once per frame
	void Update () {
        
		
	}
    IEnumerator CuentaAtras()
    {
        yield return new WaitForSeconds (time);
        Debug.Log("Reducir");
        player.transform.localScale -= scale;
        player.pushForce -= force;
        //Devolver a condiciones normales
    }
}