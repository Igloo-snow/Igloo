using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DItem : MonoBehaviour
{
    public Transform originalPosition; // �������� ���� ��ġ�� ������ ����
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
        // �θ� ���� ����
        transform.SetParent(null);
    }
}