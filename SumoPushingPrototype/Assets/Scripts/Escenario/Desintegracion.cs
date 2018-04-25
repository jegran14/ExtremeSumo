using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desintegracion : MonoBehaviour {

    public float velocityEscale = 1;
    public float velocityPosition = 1;
    public float minSize = 10f;

    private float x;
    private float y;
    private float z;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (transform.localScale.x > minSize)
        {
            x -= 0.1f * Time.deltaTime * velocityEscale;

            z -= 0.1f * Time.deltaTime * velocityEscale;
            transform.localScale += new Vector3(x, 0, z);

            if (transform.localPosition.y > -10)
            {
                y -= 0.1f * Time.deltaTime * velocityPosition;
                transform.localPosition += new Vector3(0, y, 0);
            }
        }
    }
}
