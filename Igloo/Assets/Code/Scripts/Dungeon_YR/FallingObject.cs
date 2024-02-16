using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    private Rigidbody rigid;
    private Vector3 originPos;
    private Quaternion originRotation;


    private void Start()
    {
        originPos = transform.position;
        originRotation = transform.rotation;
        rigid = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(FallCoroutine());
        }
    }

    IEnumerator FallCoroutine()
    {
        yield return new WaitForSeconds(5f);
        rigid.isKinematic = false;
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }

    public void Rearrange()
    {
        StopAllCoroutines();
        gameObject.SetActive(true);
        rigid.isKinematic = true;
        transform.position = originPos;
        transform.rotation = originRotation;
    }
}
