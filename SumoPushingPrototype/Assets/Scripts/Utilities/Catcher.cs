using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catcher : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Pushable")
        {
            float x = Random.Range(-5f, 5f);
            float z = Random.Range(-5f, 5f);

            other.transform.position = new Vector3(x, 3f, z);
        }
    }
}
