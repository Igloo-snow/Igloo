using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DItem : MonoBehaviour
{
    public Transform originalPosition; // 아이템의 원래 위치를 설정할 변수
    private Transform initialParent;

    void Start()
    {
        initialParent = transform.parent;
    }

    public void ReturnToOriginalPosition()
    {
        if (originalPosition != null)
        {
            transform.position = originalPosition.position;
            transform.rotation = originalPosition.rotation;
        }
        // 부모 설정 해제
        transform.SetParent(null);
    }
}