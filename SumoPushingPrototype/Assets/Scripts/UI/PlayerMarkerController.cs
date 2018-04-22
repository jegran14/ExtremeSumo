using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMarkerController : MonoBehaviour {
    public Transform target;
    public Color spriteColor;

    public float marginOffset = 50f;
    public float speedRotation = 10f;

    public float verticalOffset = 2f;

    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        image.color = spriteColor;
        MoveToTarget();
    }

    // Update is called once per frame
    void Update () {
        MoveToTarget();
	}

    private void MoveToTarget()
    {
        Vector3 targetOnScreen = Camera.main.WorldToScreenPoint(new Vector3(target.position.x, target.position.y + verticalOffset, target.position.z + verticalOffset));

        bool rotated = false;

        if (targetOnScreen.x > Screen.width - marginOffset)
        {
            targetOnScreen.x = Screen.width - marginOffset;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(0f, 0f, 90f), Time.deltaTime * speedRotation);
            rotated = true;
        }
        else if(targetOnScreen.x < 0 + marginOffset)
        {
            targetOnScreen.x = 0 + marginOffset;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(0f, 0f, -90f), Time.deltaTime * speedRotation);
            rotated = true;
        }

        if(targetOnScreen.y > Screen.height - marginOffset)
        {
            targetOnScreen.y = Screen.height - marginOffset;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(0f, 0f, 180f), Time.deltaTime * speedRotation);
            rotated = true;
        }
        else if(targetOnScreen.y < 0 + marginOffset)
        {
            targetOnScreen.y = 0 + marginOffset;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.identity, Time.deltaTime * speedRotation);
            rotated = true;
        }

        if (!rotated)
        {
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.identity, Time.deltaTime * speedRotation);
        }

        this.transform.position = targetOnScreen;
    }
}
