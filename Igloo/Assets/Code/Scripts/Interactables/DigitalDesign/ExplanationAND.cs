using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplanationAND : Interactable
{
    public bool isPressed;

    void Start()
    {
        promptMessage = "[E] ������ AND Gate ���� ����";
        isPressed = false;
    }

    void Update()
    {

    }

    protected override void Interact()
    {
        isPressed = true;
        promptMessage = "�Է��� ��� True�� �� ����� True�� �ȴ�.";
    }
}
