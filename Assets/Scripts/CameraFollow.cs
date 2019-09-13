using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followTarget;
    public Vector3 offset;
    public float lookAngle;
    public float rotSpeed;

    Vector3 relativeDistance;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = followTarget.position + offset;
        transform.rotation = Quaternion.Euler(lookAngle, followTarget.eulerAngles.y, 0);

        relativeDistance = transform.position - followTarget.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = followTarget.position + relativeDistance;
        transform.RotateAround(followTarget.position, Vector3.up, Input.GetAxis("Horizontal") * rotSpeed);

        relativeDistance = transform.position - followTarget.position;
    }
}
