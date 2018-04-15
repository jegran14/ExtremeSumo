using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {
    private float force;
    private PlayerController player;
    public GameObject ball;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag.Contains("Player"))
        {
            player = other.GetComponent<PlayerController>();
            player.pushForce = 50;
            ball.SetActive(false);
            
            StartCoroutine("CountDown");
            
            
        }
    }

    // Update is called once per frame
    void Update () {

	}

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds (5f);
        player.pushForce = 10;
        Destroy(gameObject);
    }
}
