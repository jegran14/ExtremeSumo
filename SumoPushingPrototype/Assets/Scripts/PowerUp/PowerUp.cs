using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {
    private float force;
    private PlayerController player;
    private Vector3 ballPosition;
    public GameObject ball;
    public ModoHulk modoHulk;
    public ModoPitufo modoPitufo;
    public float rotate;
    public float amplitudeX;
    public float amplitudeY;
    public float omegaX;
    public float omegaY;
    float index;

    private void Start()
    {
        ballPosition = ball.transform.position;
    }
    private void Update()
    {
        index += Time.deltaTime;
        float x = amplitudeX * Mathf.Cos(omegaX * index);
        float y = Mathf.Abs(amplitudeY * Mathf.Sin(omegaY * index));
        ball.transform.localPosition = new Vector3(x, y, 0);

        ball.transform.Rotate(Vector3.up, -rotate * Time.deltaTime);
        Debug.Log(Quaternion.Euler(1f,rotate,1f));
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
