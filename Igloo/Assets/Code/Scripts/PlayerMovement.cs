using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController cc;
    public Transform cameraTransform;
    [SerializeField] private float walkSpeed = 6f;
    [SerializeField] private float fallingSpeed = 10f; 
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    float currentVelocityY;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float veritcalInput = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0f, veritcalInput).normalized;

        if (DialogueManager.GetInstance().isPlaying)
        {
            return;
        }

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);

            currentVelocityY += Time.deltaTime * Physics.gravity.y;

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            var velocity = moveDirection * walkSpeed + Vector3.up * currentVelocityY * fallingSpeed;

            cc.Move(velocity * Time.deltaTime);

            if (cc.isGrounded) currentVelocityY = 0;
        }
    }
}
