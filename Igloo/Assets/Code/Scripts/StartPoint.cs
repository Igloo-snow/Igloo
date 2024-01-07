using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    private ActionController actionController;

    [SerializeField] private string startPoint;

    // Start is called before the first frame update
    void Start()
    {
        actionController = FindObjectOfType<ActionController>();

        if(startPoint == actionController.nextSceneName)
        {
            actionController.transform.position = this.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
