using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiBlinkEffect : MonoBehaviour
{
    private float time;
    [SerializeField] GameObject targetObject;

    private void Start()
    {
        //targetObject.
    }

    private void Update()
    {
        if (time < 0.5f)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1 - time);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1,time);
            if(time > 1f)
            {
                time = 0;
            }
        }
        time += Time.deltaTime;
    }

}
