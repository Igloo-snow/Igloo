using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController cc;
    public Transform cameraTransform;
    [SerializeField] private float walkSpeed = 6f;
    [SerializeField] private float jumpSpeed = 7.5f;
    [SerializeField] private float fallingSpeed = 10f; 
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    float currentVelocityY;

    Vector3 direction;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    void Update()
    {


        if (DialogueManager.GetInstance().isPlaying)
        {
            return;
        }
        
        if (cc.isGrounded)
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float veritcalInput = Input.GetAxisRaw("Vertical");
            direction = new Vector3(horizontalInput, 0f, veritcalInput).normalized;

            if(direction != Vector3.zero)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
                float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
                direction = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            }

            if(Input.GetKeyDown(KeyCode.Space)) {
                direction.y = jumpSpeed;
            }

        }
        direction.y += Physics.gravity.y * Time.deltaTime;
        cc.Move(direction * Time.deltaTime * walkSpeed);

    }
}
