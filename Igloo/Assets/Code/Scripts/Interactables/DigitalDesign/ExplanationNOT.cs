using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplanationNOT : Interactable
{
    public bool isPressed;

    void Start()
    {
        promptMessage = "[E] 눌러서 NOT Gate 설명 보기";
        isPressed = false;
    }

    void Update()
    {

    }

    protected override void Interact()
    {
        isPressed = true;
        promptMessage = "입력이 False일 때 결과가 True가 된다.";
    }
}
