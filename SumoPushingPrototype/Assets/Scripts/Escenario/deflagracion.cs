using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deflagracion : MonoBehaviour
{
    public float active=5;
    public float sleep = 5;

    private float timer;
    private float Eternalsleep;
    
    private ParticleSystem particle;
    private ParticleSystem.EmissionModule emissionModule;

    // Use this for initialization
    void Start()
    {
        timer = 0;
        Eternalsleep = sleep;

        particle = GetComponent<ParticleSystem>();
        emissionModule = particle.emission;
        emissionModule.rateOverTimeMultiplier = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (active < timer)
        {
            emissionModule.rateOverTimeMultiplier = 0;
            StartCoroutine("Sleeping");
            timer += Time.deltaTime;
        }
        else {
            timer += Time.deltaTime;
        }
        
    }

    IEnumerator Sleeping()
    {
        if (sleep >0 )
        {
            sleep -=Time.deltaTime;

        }
        else
        {
            emissionModule.rateOverTimeMultiplier = 70;
            sleep = Eternalsleep;
            timer = 0;
            yield return null;
        }
    }
}
