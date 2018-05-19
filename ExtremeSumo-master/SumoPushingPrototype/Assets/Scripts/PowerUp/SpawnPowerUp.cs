using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPowerUp : MonoBehaviour {

    public GameObject powerUp;
  

    public float maxTime = 25;
    public float minTime = 10;

    //Posiciones
    private float x;
    private float y;
    private float z;
    private Vector3 pos;

    private GameObject powerClone;
    //Tiempo actual
    private float time;

    //Booleano para esperar a fin de powerup
    [HideInInspector]
    public bool finish;

    //Tiempo de spawneo
    private float spawnTime;

	// Use this for initialization
	void Start () {
        SetRandomTime();
        y = 0.5f;
        finish = true;
	}

    // Update is called once per frame
    void FixedUpdate() {
        //Contador
        if (finish) { 
            time += Time.deltaTime;
            if (powerClone != null)
            {
                Destroy(powerClone);
            }
        }

        
        //Chequear que esta en el tiempo adecuado de spawneo.
        if (time >= spawnTime)
        {
            SpawnObject();
        }   
	}

    private void SpawnObject()
    {
        x = Random.Range(-7, 7);
        z = Random.Range(-7, 7);
        pos = new Vector3(x, y, z);

        finish = false;
        time = 0;
        transform.position = pos;
        powerClone = Instantiate(powerUp, transform.position, powerUp.transform.rotation);
        powerClone.SetActive(true);
        
    }

    private void SetRandomTime()
    {
        time = 0;
        spawnTime = Random.Range(minTime, maxTime);
    }
}
