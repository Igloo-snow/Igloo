using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WallController : MonoBehaviour
{
    public Renderer itemRenderer;
    public TMP_InputField inputField;

    void Start()
    {
        itemRenderer.enabled = false;
        inputField.onEndEdit.AddListener(OnInputEndEdit);
    }

    void OnInputEndEdit(string inputText)
    {
        if (inputText == "rmdir room")
        {
            itemRenderer.enabled = true;
        }
    }
}
