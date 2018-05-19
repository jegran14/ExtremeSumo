using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivarCanvas : MonoBehaviour {
    public SelectorManager selector;

    public void ApagarCanvas()
    {
        selector.DesactivarCanvas();
    }
}
