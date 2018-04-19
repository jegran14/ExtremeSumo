using UnityEngine;
using System.Collections;

public class Flotacion : MonoBehaviour {
    public float waterLevel = -10f;
    public float floatHeight = 2f;
    public float bounceDamp = 0.05f;

    private float forceFactor;
    private Vector3 actionPoint;
    private Vector3 uplift;
    public Vector3 buoyancyCentreOffset;

    void Update()
    {
        actionPoint = transform.position + transform.TransformDirection(buoyancyCentreOffset);
        forceFactor = 1f - ((actionPoint.y - waterLevel) / floatHeight);

        if (forceFactor > 0f)
        {
            uplift = -Physics.gravity * (forceFactor - GetComponent<Rigidbody>().velocity.y * bounceDamp);
            GetComponent<Rigidbody>().AddForceAtPosition(uplift, actionPoint);
        }
    }
}
