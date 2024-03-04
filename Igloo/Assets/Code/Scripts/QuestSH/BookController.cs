using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BookController : MonoBehaviour
{
    public Renderer itemRenderer;
    public TMP_InputField inputField;
    private Rigidbody bookRigidbody;

    void Start()
    {
        bookRigidbody = GetComponent<Rigidbody>();
        bookRigidbody.isKinematic = true;
        itemRenderer.enabled = false;
        inputField.onEndEdit.AddListener(OnInputEndEdit);
    }

    void OnInputEndEdit(string inputText)
    {
        if (inputText == "cat 리눅스교재")
        {
            itemRenderer.enabled = true;
            bookRigidbody.isKinematic = false;
            inputField.text = "";
        }
    }
}
