using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nubes : MonoBehaviour {
    public float speed = 0.5f;
    private Vector3 initPos;
    public static float initX = 200f;

	// Use this for initialization
	void Start () {
        initPos = new Vector3(initX, transform.position.y, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 movement = transform.right * speed * Time.deltaTime;
        transform.Translate(movement);

        if (Mathf.Abs(transform.position.x - initX) > 300)
        {
            transform.position = initPos;
        }
    }
}
