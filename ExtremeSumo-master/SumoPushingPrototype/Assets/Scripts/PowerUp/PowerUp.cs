using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {
    private float force;
    private PlayerController player;
    public GameObject ball;
    public ModoHulk modoHulk;
    public ModoPitufo modoPitufo;

    private void Start()
    {
        
        modoHulk = GetComponent<ModoHulk>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag.Contains("Player") && ball.name == "Hulk")
        {
            player = other.GetComponent<PlayerController>();
            ball.SetActive(false);
            modoHulk.StartPowerUp(player); 
        }
        else if (other.tag.Contains("Player") && ball.name == "Pitufo")
        {
            player = other.GetComponent<PlayerController>();
            ball.SetActive(false);
            modoPitufo.StartPowerUp(player);
        }
    }

}
