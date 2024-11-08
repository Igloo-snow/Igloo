using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController cc;
    public Transform cameraTransform;
    public CinemachineFreeLook freeLook;
    private Animator anim;
    private float slowFator = 1f;
    [SerializeField]
    private float speed = 6f;
    [SerializeField]
    private float walkSpeed = 6f;
    [SerializeField]
    private float runSpeed = 10f;
    [SerializeField]
    private float jumpSpeed = 4f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    Vector3 direction;

    public bool isAttacked = false;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        anim = GetComponentInChildren<Animator>();
        freeLook = FindAnyObjectByType<CinemachineFreeLook>();
    }

    void Update()
    {
        if (isAttacked)
        {
            return;
        }

        if (DialogueManager.GetInstance().isPlaying)
        {
            return;
        }

        if (UiManager.CameraStop)
        {
            freeLook.m_XAxis.m_MaxSpeed = 0.01f;
            freeLook.m_YAxis.m_MaxSpeed = 0.01f;
            return;
        }
        else
        {
            freeLook.m_XAxis.m_MaxSpeed = 300;
            freeLook.m_YAxis.m_MaxSpeed = 2;
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
                anim.SetBool("IsWalking", true);
            }
            else
            {
                anim.SetBool("IsWalking", false);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                direction.y = jumpSpeed;
                anim.SetTrigger("Jump");
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("IsWalking", false);
                anim.SetBool("IsRunning", true);
                speed = runSpeed;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                anim.SetBool("IsRunning", false);
                speed = walkSpeed;
            }
        }

        direction.y += Physics.gravity.y * Time.deltaTime;
        cc.Move(direction.normalized * speed * slowFator * Time.deltaTime);

    }

    public void ChangeSpeed(float value)
    {
        slowFator = value;
        anim.speed = value;
    }

    public void Attacked(Transform obstacle)
    {
        isAttacked = true;
        anim.CrossFade("JumpBack", 0.2f);
        direction = -(obstacle.position - transform.position);
        direction.y = jumpSpeed;
        cc.Move(direction.normalized * speed * Time.deltaTime);

        isAttacked = false;
    }

    public void Sit(bool onOff, Transform t, Vector3 offset)
    {
        anim.SetBool("IsSitting", onOff);
        gameObject.transform.position = t.position + offset;
        gameObject.transform.rotation = t.rotation;
    }
}
