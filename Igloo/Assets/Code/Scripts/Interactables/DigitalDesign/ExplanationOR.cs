using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplanationOR : Interactable
{
    public bool isPressed;

    void Start()
    {
        promptMessage = "[E] ������ OR Gate ���� ����";
        isPressed = false;
    }

    void Update()
    {

    }

    protected override void Interact()
    {
        isPressed = true;
        promptMessage = "�Է� �� �ϳ��� True���� ����� True�� �ȴ�.";
    }
}
