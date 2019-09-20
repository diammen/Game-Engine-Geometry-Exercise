using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 lookTarget;
    public LayerMask lookTargetMask;
    public LayerMask wallMask;
    public float moveSpeed;
    public float rotSpeed;
    public float lookTurnDuration;
    public float handTurnDuration;
    public float handRaycastDistance;

    private Animator anim;
    private Rigidbody rb;
    [SerializeField]
    private Vector3 force;
    private Vector3 currentVelocity;
    [SerializeField]
    private Transform shoulderHeight;
    private float speed;
    private float distance;
    private float lookTurnElapsed;
    private float leftHandTurnElapsed;
    private float rightHandTurnElapsed;
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("jump");
        }
    }
    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            x /= 2;
            y /= 2;
        }

        var rotateDir = Quaternion.Euler(0, x * rotSpeed, 0);
        rb.MoveRotation(transform.rotation * rotateDir);

        Vector3 moveDir = new Vector3(0, 0, y) * (moveSpeed * 100);
        currentVelocity = rb.velocity;

        anim.SetFloat("dirX", x * (moveSpeed * 100) / (moveSpeed * 100));
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
        RaycastHit rightHit;
        if (Physics.Raycast(shoulderHeight.position, transform.right, out rightHit, handRaycastDistance, wallMask))
        {
            if (rightHandTurnElapsed < handTurnDuration)
                rightHandTurnElapsed += Time.deltaTime;

            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, (rightHandTurnElapsed / handTurnDuration) >= 1 ? 1 : (rightHandTurnElapsed / handTurnDuration));
            anim.SetIKRotationWeight(AvatarIKGoal.RightHand, (rightHandTurnElapsed / handTurnDuration) >= 1 ? 1 : (rightHandTurnElapsed / handTurnDuration));

            anim.SetIKPosition(AvatarIKGoal.RightHand, rightHit.point);
            anim.SetIKRotation(AvatarIKGoal.RightHand, Quaternion.LookRotation(rightHit.point + Vector3.up - shoulderHeight.position));
        }
        else
        {
            if (rightHandTurnElapsed > 0)
                rightHandTurnElapsed -= Time.deltaTime;
            if (rightHandTurnElapsed < 0)
                rightHandTurnElapsed = 0;

            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, (rightHandTurnElapsed / handTurnDuration) >= 1 ? 1 : (rightHandTurnElapsed / handTurnDuration));
            anim.SetIKRotationWeight(AvatarIKGoal.RightHand, (rightHandTurnElapsed / handTurnDuration) >= 1 ? 1 : (rightHandTurnElapsed / handTurnDuration));
        }

        RaycastHit leftHit;
        if (Physics.Raycast(shoulderHeight.position, -transform.right, out leftHit, handRaycastDistance, wallMask))
        {
            if (leftHandTurnElapsed < handTurnDuration)
                leftHandTurnElapsed += Time.deltaTime;

            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, (leftHandTurnElapsed / handTurnDuration) >= 1 ? 1 : (leftHandTurnElapsed / handTurnDuration));
            anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, (leftHandTurnElapsed / handTurnDuration) >= 1 ? 1 : (leftHandTurnElapsed / handTurnDuration));

            anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHit.point);
            anim.SetIKRotation(AvatarIKGoal.LeftHand, Quaternion.LookRotation(leftHit.point + Vector3.up - shoulderHeight.position));
        }
        else
        {
            if (leftHandTurnElapsed > 0)
                leftHandTurnElapsed -= Time.deltaTime;
            if (leftHandTurnElapsed < 0)
                leftHandTurnElapsed = 0;

            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, (leftHandTurnElapsed / handTurnDuration) >= 1 ? 1 : (leftHandTurnElapsed / handTurnDuration));
            anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, (leftHandTurnElapsed / handTurnDuration) >= 1 ? 1 : (leftHandTurnElapsed / handTurnDuration));
            anim.SetIKPosition(AvatarIKGoal.LeftHand, lookTarget);
            anim.SetIKRotation(AvatarIKGoal.LeftHand, Quaternion.LookRotation(lookTarget + Vector3.up - shoulderHeight.position));
        }
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