using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    public ModoHulk modoHulk;
    public ModoPitufo modoPitufo;
    public float rotate;
    public GameObject [] balls;
    [HideInInspector]
    public bool active;

    public SpawnPowerUp Spawn;

    private float index;
    private PlayerController player;
    private Vector3 ballPosition;
    private GameObject ball;
    private float amplitudeX = 0.01f;
    private float amplitudeY = 0.5f;
    private float omegaX = 0.5f;
    private float omegaY = 0.4f;


    private void Start()
    {
        active = true;
        ball = BallGenerator();

        ball.SetActive(true);
        modoHulk = GetComponent<ModoHulk>();
        ballPosition = ball.transform.position;
    }

    private void Update()
    {
        index += Time.deltaTime;
        float x = amplitudeX * Mathf.Cos(omegaX * index);
        float y = Mathf.Abs(amplitudeY * Mathf.Sin(omegaY * index));

        ball.transform.localPosition = new Vector3(x, y, 0);
        ball.transform.Rotate(Vector3.up, -rotate* Time.deltaTime);

        if (!active)
        {
            Spawn.finish = true;
        }

     }

private void OnTriggerEnter(Collider other)
    {
        if (ball!=null && ball.activeInHierarchy)
        {
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

    private GameObject BallGenerator()
    {

        ball = balls[Random.Range(0,balls.Length)];
        return ball;
    }
}
