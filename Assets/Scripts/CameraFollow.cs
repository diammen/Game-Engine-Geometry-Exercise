using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followTarget;
    public float offsetHeight;
    public float lookAngle;
    public float rotSpeed;
    public float distance;

    Vector3 relativeDistance;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, followTarget.rotation, 1);
        transform.eulerAngles = new Vector3(lookAngle, transform.eulerAngles.y, transform.eulerAngles.z);
        transform.position = followTarget.position - (transform.forward * distance) + Vector3.up * offsetHeight;

        relativeDistance = transform.position - followTarget.position;
    }
}
