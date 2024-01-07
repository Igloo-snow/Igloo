using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    public static ActionController instance;

    private CapsuleCollider collider;

    [SerializeField]private bool encountered = false; 
    public bool changingScene = false;
    public string nextSceneName;

    // Start is called before the first frame update
    void Start()
    {
        
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            collider = GetComponent<CapsuleCollider>();
        }
        else
        {
            Destroy(gameObject);
        }

    }
    private void Awake()
    {
        encountered = false;
        nextSceneName = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (encountered && nextSceneName != null && Input.GetKeyDown(KeyCode.E))
        {
            changingScene = true;
        }
        else
        {
            changingScene = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.transform.CompareTag("Transfer"))
        {
            encountered = true;
            nextSceneName = collision.gameObject.name;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        encountered = false;
    }
}
