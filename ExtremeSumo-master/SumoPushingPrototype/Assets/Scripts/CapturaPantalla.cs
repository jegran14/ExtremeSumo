using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapturaPantalla : MonoBehaviour {  
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            OnClick();
        }
    }
      
    public void OnClick()
    {
        ScreenCapture.CaptureScreenshot("Capturas/Captura" + System.DateTime.Now.Millisecond + ".png");
    }
}
