using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplanationNOT : Interactable
{
    public bool isPressed;

    void Start()
    {
        promptMessage = "[E] ������ NOT Gate ���� ����";
        isPressed = false;
    }

    void Update()
    {

    }

    protected override void Interact()
    {
        isPressed = true;
        promptMessage = "�Է��� False�� �� ����� True�� �ȴ�.";
    }
}
