using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    private ActionController actionController;
    [SerializeField]
    [Tooltip("씬 이름과 동일해야함")]
    private string startPoint;

    // Start is called before the first frame update
    void Start()
    {
        actionController = FindObjectOfType<ActionController>();
        if(actionController.nextScene == startPoint)
        {
            actionController.GetComponent<CharacterController>().enabled = false;
            actionController.transform.position = this.transform.position;
            actionController.GetComponent<CharacterController>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
