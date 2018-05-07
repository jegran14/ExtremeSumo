using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nieve : MonoBehaviour {
    public float speed=0.005f;
    public float fallX = -0.1f;
    public float fallY = -1f;
    public float fallZ = -0.2f;
    public float minSize = 2;

    private float x;
    private float timer;
    private float xNieve;
    private float yNieve;
    private float zNieve;

    private ParticleSystem particle;
    private ParticleSystem.VelocityOverLifetimeModule velocityModule;

    // Use this for initialization
    void Start () {
        x = 0;
        timer = 0f;
        xNieve = fallX;
        yNieve = fallY;
        zNieve = fallZ;

        particle = GetComponent<ParticleSystem>();
        velocityModule = particle.velocityOverLifetime;

    }
	
	// Update is called once per frame
	void Update () {

        if (transform.localScale.x > minSize)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;

                if (yNieve < -1)
                {
                    xNieve += 0.006f;
                    yNieve += 0.006f;
                    zNieve += 0.006f;

                    velocityModule.x = xNieve;
                    velocityModule.y = yNieve;
                    velocityModule.z = zNieve;
                }

            }
            else {

                x -= 0.000001f * speed;
                transform.localScale += new Vector3(x, 0, 0);
                minSize = 2;
            }
        }
        else
        {
            if(timer < 20)
            {
                timer += Time.deltaTime;

                if (yNieve > -5)
                {
                    xNieve -= 0.006f;
                    yNieve -= 0.006f;
                    zNieve -= 0.006f;

                    velocityModule.x = xNieve;
                    velocityModule.y = yNieve;
                    velocityModule.z = zNieve;
                }
            }
            else
            {

                x -= 0.000001f * speed;
                transform.localScale -= new Vector3(x, 0, 0);
                minSize = 5;
            }
        }
    }
}
