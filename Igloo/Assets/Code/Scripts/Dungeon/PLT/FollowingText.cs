using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FollowingText : MonoBehaviour
{
    public TextMeshProUGUI text;
    //public Transform target;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.transform.position = Camera.main.WorldToScreenPoint(transform.position);
    }
}
