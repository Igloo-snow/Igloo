using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCQuestIconController : MonoBehaviour
{
    public Transform target;
    
    void Update()
    {
        Vector3 relativePos = target.position - transform.position;

        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        rotation *= Quaternion.Euler(0, 180, 0);
        transform.rotation = rotation;
    }
}
