using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummyDistanceCheck : MonoBehaviour
{
    public GameObject gameObject;

    void FixedUpdate()
    {
        Debug.Log(Vector3.Distance(gameObject.transform.position, this.transform.position));   
    }
}
