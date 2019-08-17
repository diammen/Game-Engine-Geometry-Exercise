using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followTarget;
    public Vector3 offset;
    public float lookAngle;
    public float rotSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        offset = Quaternion.AngleAxis(Input.GetAxis("Horizontal") * rotSpeed, Vector3.up) * offset;
        transform.position = followTarget.position + offset;
        transform.rotation = Quaternion.Euler(lookAngle, followTarget.eulerAngles.y, 0);
    }
}
