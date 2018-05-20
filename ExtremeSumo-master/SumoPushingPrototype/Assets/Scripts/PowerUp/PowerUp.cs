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

    public GameObject particles;

    private float index;
    private PlayerController player;
    private Vector3 ballPosition;
    private GameObject ball;
    private float amplitudeX = 0.01f;
    private float amplitudeY = 0.5f;
    private float omegaX = 0.5f;
    private float omegaY = 0.4f;
    private bool positionDone;
    private float y;
    private float x;
    private float z;
    private bool stopped;
    private Animator anim;
    


    private void Start()
    {
        active = true;
        BallGenerator();
        positionDone = false;
        ball.SetActive(true);
        //modoHulk = GetComponent<ModoHulk>();
        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;
        anim = GetComponent<Animator>();
        stopped = false;
        particles.SetActive(true);
    }

    private void Update()
    {
        index += Time.deltaTime;
        
        if (transform.position.y < 0.2f && !positionDone)
        {
            positionDone = true;
        }
        else if (!positionDone)
        {
            y -= 0.01f;
            Debug.Log(y);
            transform.position = new Vector3(x, y, z);
        }
        else
        {
            float yOscilante = Mathf.Abs(amplitudeY * Mathf.Sin(omegaY * index));
            transform.position = new Vector3(x, yOscilante, z);
        }
  
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
                particles.SetActive(false);
                stopped = true;
                anim.SetTrigger("stopped");
                player = other.GetComponent<PlayerController>();
                ball.SetActive(false);
                modoHulk.StartPowerUp(player);
            }
            else if (other.tag.Contains("Player") && ball.name == "Pitufo")
            {
                particles.SetActive(false);
                stopped = true;
                anim.SetTrigger("stopped");
                player = other.GetComponent<PlayerController>();
                ball.SetActive(false);
                modoPitufo.StartPowerUp(player);
            }
        }
    }

    public void BallGenerator()
    {
        if (!stopped)
        {
            if (ball != null)
            {
                ball.SetActive(false);
            }
            ball = balls[Random.Range(0, balls.Length)];
            ball.SetActive(true);
        }
     }
}
