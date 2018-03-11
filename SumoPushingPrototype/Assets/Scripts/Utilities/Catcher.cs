using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catcher : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Pushable")
        {
            float x = Random.Range(-10f, 10f);
            float z = Random.Range(-10f, 10f);

            other.transform.position = new Vector3(x, 3f, z);
        }
    }
}
