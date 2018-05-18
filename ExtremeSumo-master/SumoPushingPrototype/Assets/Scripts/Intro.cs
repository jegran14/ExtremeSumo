using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Intro : MonoBehaviour {
    public GameObject canvas;

    private float timer;
    public RawImage logoImage;
    public Text presentaText;
    public Text tituloText;
    public Text autorText;

    // Use this for initialization
    void Start () {
        timer = 0f;
        canvas.SetActive(false);
        logoImage.color = new Color(1, 1, 1, 0f);
        presentaText.color = new Color(1, 1, 1, 0f);
        tituloText.color = new Color(1, 1, 1, 0f);
        autorText.color = new Color(1, 1, 1, 0f);
    }
	
	// Update is called once per frame
	void Update () {
        if (timer < 1)
        {
            StartCoroutine("Logo");
        }

         if (timer > 26.5)
        {
            canvas.SetActive(true);
            this.enabled = false;
        }
        timer += Time.deltaTime;
    }

    IEnumerator Logo()
    {
        for (float f = 0f ; f <=1; f += 0.01f)
        {
            logoImage.color = new Color(1, 1, 1, f);
            yield return null;
        }


       float wait= 0;
       while (wait < 2)
        {
            wait += Time.deltaTime;
            logoImage.color = new Color(1, 1, 1, 1);
            yield return null;
        }
        for (float f = 1f; f >= 0; f -= 0.01f)
        {
            logoImage.color = new Color(1, 1, 1, f);
            yield return null;
        }


        wait = 0;
        while (wait < 1)
        {
            wait += Time.deltaTime;
            presentaText.color = new Color(1, 1, 1, 0);
            yield return null;
        }

        for (float f = 0f; f <= 1; f += 0.01f)
        {
            presentaText.color = new Color(1, 1, 1, f);
            yield return null;
        }


        wait = 0;
        while (wait < 1)
        {
            wait += Time.deltaTime;
            presentaText.color = new Color(1, 1, 1, 1);
            yield return null;
        }
        for (float f = 1f; f >= 0; f -= 0.01f)
        {
            presentaText.color = new Color(1, 1, 1, f);
            yield return null;
        }


        wait = 0;
        presentaText.text = "UNA PRODUCCIÓN DE";
        while (wait < 0.25)
        {
            wait += Time.deltaTime;
            presentaText.color = new Color(1, 1, 1, 0);
            yield return null;
        }
        for (float f = 0f; f <= 1; f += 0.01f)
        {
            presentaText.color = new Color(1, 1, 1, f);
            yield return null;
        }

        for (float f = 0f; f <= 1; f += 0.01f)
        {
            autorText.color = new Color(1, 1, 1, f);
            yield return null;
        }

        for (float f = 1f; f >= 0; f -= 0.01f)
        {
            autorText.color = new Color(1, 1, 1, f);
            yield return null;
        }


        wait = 0;
        while (wait < 0.15)
        {
            wait += Time.deltaTime;
            yield return null;
        }

        autorText.text = "Carlos Hernández";
        for (float f = 0f; f <= 1; f += 0.01f)
        {
            autorText.color = new Color(1, 1, 1, f);
            yield return null;
        }

        for (float f = 1f; f >= 0; f -= 0.01f)
        {
            autorText.color = new Color(1, 1, 1, f);
            yield return null;
        }


        wait = 0;
        while (wait < 0.15)
        {
            wait += Time.deltaTime;
            yield return null;
        }

        autorText.text = "Daniel Fernández";
        for (float f = 0f; f <= 1; f += 0.01f)
        {
            autorText.color = new Color(1, 1, 1, f);
            yield return null;
        }

        for (float f = 1f; f >= 0; f -= 0.01f)
        {
            autorText.color = new Color(1, 1, 1, f);
            presentaText.color = new Color(1, 1, 1, f);
            yield return null;
        }


        wait = 0;
        while (wait < 0.5)
        {
            wait += Time.deltaTime;
            yield return null;
        }

        for (float f = 0f; f <= 1; f += 0.01f)
        {
            tituloText.color = new Color(1, 1, 1, f);
            yield return null;
        }

    }

}
