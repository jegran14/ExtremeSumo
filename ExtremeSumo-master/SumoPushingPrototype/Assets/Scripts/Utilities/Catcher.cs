using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;


public class Catcher : MonoBehaviour {
    public ParticleSystem[] particulas;
    void Start()
    {
        for(int i=0; i < particulas.Length; i++)
        {
            particulas[i].gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Pushable" || other.tag.Contains("Player"))
        {
            /*
            float x = Random.Range(-5f, 5f);
            float z = Random.Range(-5f, 5f);

            other.transform.position = new Vector3(x, 3f, z);*/

            //We won't just deactive the player, well disable it using animations and particle effects
            int i = 0;
            for (int j=0; j<particulas.Length; j++)
            {
                if (!particulas[j].gameObject.activeInHierarchy)
                {
                    i = j;
                    break;
                }
            }

            Camera cam = Camera.main;
            float height = 2f * cam.orthographicSize;
            float width = height * cam.aspect;
            Vector3 direccion = Vector3.zero - other.transform.position;


                if (other.transform.position.x < -16f)
                {
                    particulas[i].transform.position = new Vector3(-(width/2), other.transform.position.y, 0);
                    particulas[i].transform.rotation = Quaternion.LookRotation(new Vector3(90,0,0));
                    particulas[i].gameObject.SetActive(true);
                    particulas[i].Play();
                }
                else if (other.transform.position.x > 8f)
                {
                    particulas[i].transform.position = new Vector3(width / 2, other.transform.position.y, 0);
                    particulas[i].transform.rotation = Quaternion.LookRotation(direccion);
                    particulas[i].gameObject.SetActive(true);
                    particulas[i].Play();
                }
                CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
            //particulas[i].transform.position = other.transform.position;

            //particulas[i].transform.position = new Vector3(other.transform.position.x-(other.transform.position.x-(cam.transform.position.x + width/2)), other.transform.position.y - (other.transform.position.y - (cam.transform.position.y +height / 2)), 0);
            //particulas[i].transform.position = other.transform.position - (other.transform.position - new Vector3(width / 2, height / 2, 0f));

            other.gameObject.SetActive(false);
        }
    }
}
