using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InformationUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text promptText;

    void Start()
    {
        promptText.text = "";
    }

    public void UpdateText(string promptMessage)
    {
        promptText.text = promptMessage;
    }
}
