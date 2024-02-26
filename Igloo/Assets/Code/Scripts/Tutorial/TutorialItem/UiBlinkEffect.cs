using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiBlinkEffect : MonoBehaviour
{
    private float time;
    [SerializeField] GameObject targetObject;

    private void Start()
    {
        if(targetObject.GetComponent<Image>() != null)
        {
            
        }
    }

    private void Update()
    {
        if (time < 0.5f)
        {
            GetComponent<TMP_Text>().color = new Color(1, 1, 1, 1 - time);
        }
        else
        {
            GetComponent<TMP_Text>().color = new Color(1, 1, 1,time);
            if(time > 1f)
            {
                time = 0;
            }
        }
        time += Time.deltaTime;
    }

}
