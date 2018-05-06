using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caida : MonoBehaviour {
    public float speed = 0.5f;
    private Vector3 initPos;
    public static float initY = 5f;

    // Use this for initialization
    void Start()
    {
        initPos = new Vector3(transform.position.x, initY, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = -transform.up * speed * Time.deltaTime;
        transform.Translate(movement);

        if (Mathf.Abs(transform.position.y - initY) > 8)
        {
            transform.position = initPos;
        }
    }
}
