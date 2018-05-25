using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCameraMovement : MonoBehaviour {

    public GameObject Arm;
    public float rotateSpeed = 5;
    Vector3 offset;

    void Start() {
        offset = Arm.transform.position - transform.position;
        RotateCamera ();
    }

    void LateUpdate() {
        if (Input.GetMouseButton (1)) {
            RotateCamera ();
        }
    }

    void RotateCamera() {
        float horizontal = Input.GetAxis ("Mouse X") * rotateSpeed;
        float vertical = Input.GetAxis ("Mouse Y") * rotateSpeed;
        Arm.transform.Rotate (vertical, horizontal, 0);

        float desiredAngleY = Arm.transform.eulerAngles.y;
        float desiredAngleX = Arm.transform.eulerAngles.x;

        ChechBoundaries ();

        Quaternion rotation = Quaternion.Euler (desiredAngleX, desiredAngleY, 0);
        transform.position = Arm.transform.position - (rotation * offset);

        transform.LookAt (Arm.transform);
    }

    void ChechBoundaries() {
        float XClamp = Arm.transform.eulerAngles.x;
        if(XClamp > 300) 
            XClamp = Mathf.Clamp (XClamp, 330, 360);

        if (XClamp < 30)
            XClamp = Mathf.Clamp (XClamp, 0, 23);

        Arm.transform.eulerAngles = new Vector3 (XClamp, Arm.transform.eulerAngles.y, Arm.transform.eulerAngles.z);
    }
}
