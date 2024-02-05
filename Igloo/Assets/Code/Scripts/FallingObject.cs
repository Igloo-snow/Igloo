using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    private Rigidbody rigid;
    private Vector3 originPos;
    private Quaternion originRotation;
    private float radius = 5f;


    private void Start()
    {
        originPos = transform.position;
        originRotation = transform.rotation;
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Collider[] hit = Physics.OverlapSphere(transform.position, radius);
        for(int i = 0; i < hit.Length; i++)
        {
            if (hit[i].CompareTag("Player"))
            {
                Debug.Log("o");
                StartCoroutine(fallCoroutine());
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log("플레이어 enter");
            StartCoroutine(fallCoroutine());
        }
    }

    IEnumerator fallCoroutine()
    {
        yield return new WaitForSeconds(5f);
        rigid.isKinematic = false;
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);

    }

    public void Rearrange()
    {
        gameObject.SetActive(true);
        rigid.isKinematic = true;
        transform.position = originPos;
        transform.rotation = originRotation;
    }
}
