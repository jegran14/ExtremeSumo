using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModoPitufo : MonoBehaviour {

    PlayerController player;
    public float time;
    public float force;
    public Vector3 scale;
    private PowerUp power;

    // Use this for initialization
    public void StartPowerUp(PlayerController player)
    {
        //Cambiar condiciones
        Debug.Log("ModoHulkActivo");
        this.player = player;
        player.transform.localScale -= scale;
        player.pushForce -= force;
        StartCoroutine("CuentaAtras");
        //CuentaAtras();
        Debug.Log("Reducir");
    }

    // Update is called once per frame
    void Update()
    {


    }
    IEnumerator CuentaAtras()
    {
        Debug.Log("Reducir");
        yield return new WaitForSeconds(time);
        Debug.Log("Reducir");
        player.transform.localScale += scale;
        player.pushForce += force;
        power.active = false;
        //Devolver a condiciones normales
    }

}

