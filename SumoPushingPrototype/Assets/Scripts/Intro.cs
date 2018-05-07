using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour {
    public GameObject canvas;

    private float timer;

	// Use this for initialization
	void Start () {
        timer = 0f;
        canvas.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (timer > 25)
        {
            canvas.SetActive(true);
            this.enabled = false;
        }
        timer += Time.deltaTime;
    }
}
