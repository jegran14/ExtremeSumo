using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catcher : MonoBehaviour {

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Pushable" || other.tag.Contains("Player"))
        {
            /*
            float x = Random.Range(-5f, 5f);
            float z = Random.Range(-5f, 5f);

            other.transform.position = new Vector3(x, 3f, z);*/

            //We won't just deactive the player, well disable it using animations and particle effects
            other.gameObject.SetActive(false);
        }
    }
}
