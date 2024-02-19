using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController cc;
    public Transform cameraTransform;
    private Animator anim;
    [SerializeField]
    private float speed = 6f;
    [SerializeField]
    private float jumpSpeed = 4f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    Vector3 direction;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        anim = GetComponentInChildren<Animator>();
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
                anim.SetBool("IsRunning", true);
            }
            else
            {
                anim.SetBool("IsRunning", false);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                direction.y = jumpSpeed;
                anim.SetTrigger("Jump");
            }
        }

        direction.y += Physics.gravity.y * Time.deltaTime;
        cc.Move(direction.normalized * speed * Time.deltaTime);

    }
}
