using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rigid;

    [SerializeField] private float walkSpeed;
    [SerializeField] private float lookSensitivity;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlayerRotation();

    }

    private void PlayerMove()
    {
        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");

        Vector3 moveH = transform.right * hAxis;
        Vector3 moveV = transform.forward * vAxis;

        Vector3 velocity = (moveH + moveV).normalized * walkSpeed;
        rigid.MovePosition(transform.position + velocity * Time.deltaTime);
    }

    private void PlayerRotation()
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
        rigid.MoveRotation(rigid.rotation * Quaternion.Euler(_characterRotationY));
    }
}
