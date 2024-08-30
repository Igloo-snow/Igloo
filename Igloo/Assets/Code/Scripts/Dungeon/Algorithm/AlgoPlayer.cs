using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlgoPlayer : MonoBehaviour
{
    private float currentSpeed;
    public float moveSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpSpeed = 4f;
    public float rotationSpeed = 700f;
    private Rigidbody rb;
    private Animator anim;
    private CinemachineVirtualCamera virtualCamera;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>(); // ������ CinemachineVirtualCamera ã��
    }

    private void FixedUpdate()
    {
        Move();

        if(Input.GetKey(KeyCode.R))
        {
            currentSpeed = jumpSpeed;
            anim.SetTrigger("Jump");
        }
        else if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentSpeed = runSpeed;
        }
        else
        {
            currentSpeed = moveSpeed;
        }

        UpdateAnimatorSpeed();
    }

    private void Move()
    {
        if (virtualCamera == null) return;

        Transform cameraTransform = virtualCamera.transform;
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 inputDirection = new Vector3(horizontal, 0, vertical).normalized;

        Vector3 moveDirection = cameraForward * inputDirection.z + cameraRight * inputDirection.x;

        Vector3 targetPosition = rb.position + moveDirection * currentSpeed * Time.fixedDeltaTime;

        rb.MovePosition(targetPosition);

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            Quaternion newRotation = Quaternion.RotateTowards(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(newRotation);
        }
    }

    private void UpdateAnimatorSpeed()
    {
        float speed = currentSpeed / runSpeed; // runSpeed�� ������ 0~1�� ����ȭ
        speed = Mathf.Clamp01(speed); // 0�� 1 ���̷� ����
        anim.SetFloat("Blend", speed);
        Debug.Log(rb.velocity.magnitude);
    }
}
