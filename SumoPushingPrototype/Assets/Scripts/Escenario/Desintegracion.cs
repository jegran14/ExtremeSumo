using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desintegracion : MonoBehaviour {

    public float velocity=1;
    public float minSize = 0.3f;

    private float x;
    private float y;
    private float z;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        if (transform.localScale.y> minSize)
        {
            x -= 0.1f * Time.deltaTime * velocity;
            y -= 0.1f * Time.deltaTime * velocity;
            z -= 0.1f * Time.deltaTime * velocity;
            transform.localScale += new Vector3(x, y, z);
        }


    }
}
