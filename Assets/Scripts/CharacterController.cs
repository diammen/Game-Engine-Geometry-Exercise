using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Vector3 lookTarget;
    public LayerMask lookTargetMask;
    public float moveSpeed;
    public float rotSpeed;
    public float lookTurnDuration;

    private Animator anim;
    private Rigidbody rb;
    private Vector3 force;
    private Vector3 currentVelocity;
    private float speed;
    private float distance;
    private float lookTurnElapsed;
    private bool isColliding;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var rotateDir = Quaternion.Euler(0, Input.GetAxis("Horizontal") * rotSpeed, 0);
        rb.MoveRotation(transform.rotation * rotateDir);

        Vector3 moveDir = new Vector3(0, 0, (Input.GetAxis("Vertical") < -0.5f ? -0.5f : Input.GetAxis("Vertical"))) * (moveSpeed * 100);
        currentVelocity = rb.velocity;

        anim.SetFloat("dirX", Input.GetAxis("Horizontal") * (moveSpeed * 100) / (moveSpeed * 100));
        anim.SetFloat("dirY", moveDir.z / (moveSpeed * 100));
        anim.SetFloat("speed", moveDir.magnitude / (moveSpeed * 100));
        anim.SetBool("isMovingBack", moveDir.z / (moveSpeed * 100) < 0);

        force = Vector3.ClampMagnitude(transform.TransformDirection(moveDir), moveSpeed * 100) * Time.deltaTime - currentVelocity;
        rb.AddForce(force, ForceMode.Impulse);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (isColliding)
        {
            if (lookTurnElapsed < lookTurnDuration)
                lookTurnElapsed += Time.deltaTime;

            anim.SetLookAtWeight(Mathf.Lerp(0, 1, (lookTurnElapsed / lookTurnDuration) >= 1 ? 1 : (lookTurnElapsed / lookTurnDuration)));
            anim.SetLookAtPosition(lookTarget);
        }
        else
        {
            if (lookTurnElapsed > 0)
                lookTurnElapsed -= Time.deltaTime;

            anim.SetLookAtWeight(Mathf.Lerp(0, 1, (lookTurnElapsed / lookTurnDuration) >= 1 ? 1 : (lookTurnElapsed / lookTurnDuration)));
        }

        //anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        //anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
        //anim.SetIKPosition(AvatarIKGoal.RightHand, lookTarget.position);
        //anim.SetIKRotation(AvatarIKGoal.RightHand, lookTarget.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (lookTargetMask == (lookTargetMask | (1 << other.gameObject.layer)))
        {
            isColliding = true;
            lookTarget = other.gameObject.transform.position;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (lookTargetMask == (lookTargetMask | (1 << other.gameObject.layer)))
        {
            isColliding = true;
            lookTarget = other.gameObject.transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isColliding = false;
        lookTarget = transform.forward;
    }
}